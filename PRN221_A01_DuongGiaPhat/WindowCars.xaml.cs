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
    /// Interaction logic for WindowCars.xaml
    /// </summary>
    public partial class WindowCars : Window
    {
        ICarRepository carRepository = null;
        public WindowCars()
        {
            InitializeComponent();
            carRepository = new CarRepository();
            LoadCars();
        }

        public void LoadCars()
        {
            listCars.ItemsSource = carRepository.GetCars();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e) => this.Close();

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadCars();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Loading cars error");
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            WindowCarDetail windowCarDetail = new WindowCarDetail(null, false);
            windowCarDetail.Show();
            this.Close();
        }

        private void listCars_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Car car = (Car)listCars.SelectedItem;
            WindowCarDetail windowCarDetail = new WindowCarDetail(car, true);
            windowCarDetail.Show();
            this.Close();
        }
    }
}
