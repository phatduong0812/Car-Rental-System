using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for WindowCustomerProfile.xaml
    /// </summary>
    public partial class WindowCustomerProfile : Window
    {
        IMemberRepository memberRepository;
        bool isUpdate = false;
        Customer Customer;
        public WindowCustomerProfile(Customer customer, bool update)
        {
            InitializeComponent();
            memberRepository = new MemberRepository();
            Customer = customer;
            isUpdate = update;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (isUpdate)
            {
                txtCustomerId.IsEnabled = false;
                txtEmail.IsEnabled = false;
            }
            else
            {
                txtCustomerId.IsEnabled = true;
            }
            if (Customer != null)
            {
                txtCustomerId.Text = Customer.CustomerId;
                txtFullName.Text = Customer.CustomerName;
                txtEmail.Text = Customer.CustomerEmail;
                txtPassword.Password = Customer.CustomerPassword;
                txtMobile.Text = Customer.Mobile;
                txtIdentity.Text = Customer.IdentityCard;
                txtLicenseNumber.Text = Customer.LicenceNumber;
                dateLicense.SelectedDate = Customer.LicenceDate;
            }
        }

        private void GetCustomerInFo() => Customer = new Customer
        {
            CustomerId = txtCustomerId.Text,
            CustomerEmail = txtEmail.Text,
            CustomerName = txtFullName.Text,
            CustomerPassword = txtPassword.Password,
            Mobile = txtMobile.Text,
            IdentityCard = txtIdentity.Text,    
            LicenceNumber = txtLicenseNumber.Text,
            LicenceDate = dateLicense.SelectedDate
        };

        private void btnClose_Click(object sender, RoutedEventArgs e) => this.Close();

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            var emailRegex = "[a-zA-Z0-9]+@[a-zA-Z0-9]+\\.com";
            bool isValid = true;
            if (String.IsNullOrEmpty(txtCustomerId.Text.Trim()))
            {
                isValid = false;
                MessageBox.Show("CustomerId must be not empty");
            }
            else if (String.IsNullOrEmpty(txtFullName.Text.Trim()))
            {
                isValid = false;
                MessageBox.Show("Customer name must be not empty");
            }
            else if (String.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                isValid = false;
                MessageBox.Show("Customer email must be not empty");
            }
            else if (!Regex.Match(txtEmail.Text, emailRegex).Success)
            {
                isValid = false;
                MessageBox.Show("Email is not correct format");
            }
            else if (String.IsNullOrEmpty(txtPassword.Password.Trim()))
            {
                isValid = false;
                MessageBox.Show("Customer password must be not empty");
            }
            else if (String.IsNullOrEmpty(txtMobile.Text.Trim()))
            {
                isValid = false;
                MessageBox.Show("Customer moible must be not empty");
            }
            else if (String.IsNullOrEmpty(txtIdentity.Text.Trim()))
            {
                isValid = false;
                MessageBox.Show("Customer Identity must be not empty");
            }
            else if (String.IsNullOrEmpty(txtLicenseNumber.Text.Trim()))
            {
                isValid = false;
                MessageBox.Show("Customer license number must be not empty");
            }else if(DateTime.Compare(dateLicense.SelectedDate.Value, DateTime.Now) >= 0)
            {
                isValid = false;
                MessageBox.Show("Customer's license must not be in futer or now");
            }
            if (isValid)
            {
                GetCustomerInFo();
                try
                {
                    if (!isUpdate)
                    {
                        memberRepository.AddCustomer(Customer);
                        MessageBox.Show("Customer is created successfully!", "Create Customer");
                        Close();
                        WindowCustomers windowCustomers = new WindowCustomers();
                        windowCustomers.Show();
                    }
                    else
                    {
                        memberRepository.UpdateCustomer(Customer);
                        MessageBox.Show("Customer is updated successfully!", "Update Customer");
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    if (!isUpdate) MessageBox.Show(ex.Message, "Creating error");
                    else MessageBox.Show("Updating error ");
                }
            }
        }
    }
}
