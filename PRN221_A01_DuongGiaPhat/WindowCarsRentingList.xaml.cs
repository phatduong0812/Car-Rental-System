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
    /// Interaction logic for WindowCarsRentingList.xaml
    /// </summary>
    public partial class WindowCarsRentingList : Window
    {
        ICarRepository carRepository = null;
        public WindowCarsRentingList()
        {
            InitializeComponent();
            carRepository = new CarRepository();
            LoadCars();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e) => this.Close();
        public void LoadCars()
        {
            carRetals.ItemsSource = carRepository.GetCarRentals();
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            WindowCarsRenting windowCarRentals = new WindowCarsRenting();
            windowCarRentals.Show();
            this.Close();
        }
    }
}
