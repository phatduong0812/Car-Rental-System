using BusinessObject;
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
    /// Interaction logic for WindowStaffs.xaml
    /// </summary>
    public partial class WindowStaffs : Window
    {
        IMemberRepository memberRepository;
        public WindowStaffs()
        {
            memberRepository = new MemberRepository();
            InitializeComponent();
            LoadStaffs();
        }

        public void LoadStaffs()
        {
            staffs.ItemsSource = memberRepository.GetStaffs();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void btn_Load_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadStaffs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Loading staffs error, please try again!");
            }
        }

        private void staffs_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            StaffAccount staff = (StaffAccount)staffs.SelectedItem;
            WindowStaffDetail windowStaffDetail = new WindowStaffDetail(staff, true, true);
            windowStaffDetail.Show();
            this.Close();
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            WindowStaffDetail windowStaffDetail = new WindowStaffDetail(null, true, false);
            windowStaffDetail.Show();
            this.Close();
        }
    }
}
