using BusinessObject;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PRN221_A01_DuongGiaPhat
{
    /// <summary>
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        IMemberRepository MemberRepository = new MemberRepository();
        List<string> roles = new List<string> { "admin", "customer", "staff" };
        public WindowLogin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            roles.ForEach(role => cmbRole.Items.Add(role));
            cmbRole.SelectedIndex = 0;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            IConfiguration config = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", true, true)
                                        .Build();
            string emailAdmin = config["AdminAccount:Email"];
            string PasswordAdmin = config["AdminAccount:Password"];
            var email = txtEmail.Text;
            var password = txtPassword.Password;
            if (String.IsNullOrEmpty(email))
            {
                MessageBox.Show("Your email is empty", "wrong Information and try again!");

            }
            else if (String.IsNullOrEmpty(password))
            {
                MessageBox.Show("Your password is empty", "wrong Information and try again!");
            }
            else
            {
                try
                {
                    if (roles.ElementAt(cmbRole.SelectedIndex) == "admin")
                    {
                        if(email.Equals(emailAdmin.Trim()) && password.Equals(PasswordAdmin.Trim()))
                        {
                            MainWindow mainWindow = new MainWindow("admin", null, null);
                            this.Close();
                            mainWindow.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Your email or password is wrong", "please try again!");
                        }
                    }
                    else if(roles.ElementAt(cmbRole.SelectedIndex) == "staff")
                    {
                        StaffAccount staff = MemberRepository.GetLoginStaff(email.Trim(), password.Trim());
                        if (staff != null)
                        {
                            MainWindow mainWindow = new MainWindow("staff", staff, null);
                            this.Close();
                            mainWindow.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Your email or password is wrong", "please try again!");
                        }
                    }
                    else if(roles.ElementAt(cmbRole.SelectedIndex) == "customer")
                    {
                        Customer customer= MemberRepository.GetLoginCustomer(email.Trim(), password.Trim());
                        if (customer != null)
                        {
                            MainWindow mainWindow = new MainWindow("customer", null, customer);
                            this.Close();
                            mainWindow.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Your email or password is wrong", "please try again!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Something went wrong!");
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
