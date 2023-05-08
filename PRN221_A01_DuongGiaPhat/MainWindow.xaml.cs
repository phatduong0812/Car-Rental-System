using BusinessObject;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PRN221_A01_DuongGiaPhat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Customer Customer;
        StaffAccount Staff;
        string role = "admin";
        public MainWindow(string roleUser, StaffAccount staff, Customer customer)
        {
            InitializeComponent();
            if(roleUser == "customer")
            {
                Customer = customer;
                role = roleUser;
            }else if(roleUser == "staff")
            {
                Staff = staff;
                role = roleUser;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(role == "staff")
            {
                btn_Staffs.Visibility = Visibility.Hidden; 
            }else if(role == "customer")
            {
                btn_Customers.Visibility = Visibility.Hidden;
                btn_Cars.Visibility = Visibility.Hidden;
                btn_Staffs.Visibility = Visibility.Hidden;
                btn_Rent.Visibility = Visibility.Hidden;
            }else if(role == "admin")
            {
                btn_Customers.Visibility = Visibility.Hidden;
                btn_Cars.Visibility = Visibility.Hidden;
                btn_Profile.Visibility = Visibility.Hidden;
                btn_Rent.Visibility = Visibility.Hidden;
            }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void btn_Cars_Click(object sender, RoutedEventArgs e)
        {
            WindowCars windowCars = new WindowCars();
            this.Hide();
            windowCars.Show();
            this.Show();
        }

        private void btn_Staffs_Click(object sender, RoutedEventArgs e)
        {
            WindowStaffs windowStaffs = new WindowStaffs();
            this.Hide();
            windowStaffs.Show();
            this.Show();
        }


        private void btn_Profile_Click(object sender, RoutedEventArgs e)
        {
            if(role == "customer")
            {
                WindowCustomerProfile windowCustomerProfile = new WindowCustomerProfile(Customer, true);
                this.Hide();
                windowCustomerProfile.Show();
                this.Show();
            }else if(role == "staff")
            {
                WindowStaffDetail windowStaffProfile = new WindowStaffDetail(Staff, false, true);
                this.Hide();
                windowStaffProfile.Show();
                this.Show();
            }
        }

        private void btn_Customers_Click_1(object sender, RoutedEventArgs e)
        {
            WindowCustomers windowCustomers = new WindowCustomers();
            this.Hide();
            windowCustomers.Show();
            this.Show();
        }

        private void btn_Rent_Click(object sender, RoutedEventArgs e)
        {
            WindowCarsRentingList windowCarsRentingList = new WindowCarsRentingList();
            this.Hide();
            windowCarsRentingList.Show();
            this.Show();
        }
    }
}
