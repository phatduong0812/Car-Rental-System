using BusinessObject;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for WindowStaffDetail.xaml
    /// </summary>
    public partial class WindowStaffDetail : Window
    {
        IMemberRepository memberRepository;
        private StaffAccount Staff;
        bool isUpdate = false;
        bool isAdmin = false;
        List<int> roles = new List<int> { 1, 2 };
        public WindowStaffDetail(StaffAccount staff, bool admin, bool update)
        {
            InitializeComponent();
            memberRepository = new MemberRepository();
            if(staff != null)
            {
                Staff = staff;
            }
            isAdmin = admin;
            isUpdate = update;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (isUpdate)
            {
                txtStaffId.IsEnabled = false;
                txtEmail.IsEnabled = false;
                if (isAdmin) btnDelete.Visibility = Visibility.Visible;
                else btnDelete.Visibility = Visibility.Hidden;
            }
            else
            {
                txtStaffId.IsEnabled = true;
                btnDelete.Visibility = Visibility.Hidden;
                roles.ForEach(role => cmbRole.Items.Add(role));
                cmbRole.SelectedIndex = 0;
            }
            if(Staff != null)
            {
                txtStaffId.Text = Staff.StaffId.ToString();
                txtFullName.Text = Staff.FullName.ToString();
                txtEmail.Text = Staff.Email.ToString();
                txtPassword.Password = Staff.Password.ToString();
                roles.ForEach(role => cmbRole.Items.Add(role));  
                var index = roles.FindIndex(role => role == Staff.Role);
                cmbRole.SelectedIndex = index;
            }
        }

        private void GetStaffInFo() => Staff = new StaffAccount
        {
            StaffId = txtStaffId.Text,
            Email = txtEmail.Text,
            FullName = txtFullName.Text,
            Password = txtPassword.Password,
            Role = roles.ElementAt(cmbRole.SelectedIndex)
        };

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            var emailRegex = "[a-zA-Z0-9]+@[a-zA-Z0-9]+\\.com";
            bool isValid = true;
            if (String.IsNullOrEmpty(txtStaffId.Text.Trim()))
            {
                isValid = false;
                MessageBox.Show("StaffId must be not empty");
            }
            else if(String.IsNullOrEmpty(txtFullName.Text.Trim()))
            {
                isValid = false;
                MessageBox.Show("Fullname must be not empty");
            }
            else if (String.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                isValid = false;
                MessageBox.Show("Fullname must be not empty");
            }else if(!Regex.Match(txtEmail.Text, emailRegex).Success)
            {
                isValid = false;
                MessageBox.Show("Email is not correct format");
            }if (isValid)
            {
                GetStaffInFo();
                try
                {
                    if (!isUpdate)
                    {
                        memberRepository.AddStaff(Staff);
                        MessageBox.Show("Staff is created successfully!", "Create Staff");
                        Close();
                    }
                    else
                    {
                        memberRepository.UpdateStaff(Staff);
                        MessageBox.Show("Staff is updated successfully!", "Update Staff");
                        Close();
                    }
                    WindowStaffs windowStaffs = new WindowStaffs();
                    windowStaffs.Show();
                }
                catch (Exception ex)
                {
                    if (!isUpdate) MessageBox.Show(ex.Message, "Creating error");
                    else MessageBox.Show("Updating error ");
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete?", "Delete Warning", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    memberRepository.RemoveStaff(Staff);
                    MessageBox.Show("Member is deleted successfully!", "Delete Member");
                    Close();
                    WindowStaffs windowStaffs = new WindowStaffs();
                    windowStaffs.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Deleting error");
                Close();
                WindowStaffs windowStaffs = new WindowStaffs();
                windowStaffs.Show();
            }
        }
    }
}
