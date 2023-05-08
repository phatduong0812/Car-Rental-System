using DataAccess.Repository;
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
using System.Windows.Shapes;

namespace PRN221_A01_DuongGiaPhat
{
    /// <summary>
    /// Interaction logic for WindowCustomers.xaml
    /// </summary>
    /// 
    public partial class WindowCustomers : Window
    {
        IMemberRepository memberRepository;
        public WindowCustomers()
        {
            InitializeComponent();
            memberRepository = new MemberRepository();
            LoadCustomers();
        }
        public void LoadCustomers()
        {
            customers.ItemsSource = memberRepository.GetCustomers();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            WindowCustomerProfile windowCustomerProfile = new WindowCustomerProfile(null, false);
            windowCustomerProfile.Show();
            this.Close();
        }
    }
}
