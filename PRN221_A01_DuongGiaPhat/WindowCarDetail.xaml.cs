using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for WindowCarDetail.xaml
    /// </summary>
    public partial class WindowCarDetail : Window
    {
        Car Car;
        ICarRepository carRepository;
        bool isUpdate = false;
        List<int> statuses = new List<int> { 0, 1 };
        List<CarProducer> producers = new List<CarProducer>();
        public WindowCarDetail(Car car, bool update)
        {
            InitializeComponent();
            carRepository = new CarRepository();
            isUpdate = update;
            Car = car;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (isUpdate)
            {
                txtCarId.IsEnabled = false;
                btnDelete.Visibility = Visibility.Visible;
            }
            else
            {
                txtCarId.IsEnabled = true;
                btnDelete.Visibility = Visibility.Hidden;
                statuses.ForEach(status => cmbSatus.Items.Add(status));
                cmbSatus.SelectedIndex = 0;
                producers = carRepository.GetCarProducers().ToList();
                producers.ForEach(prod => cmbProducerId.Items.Add(prod.ProducerId));
                cmbProducerId.SelectedIndex = 0;
            }
            if (Car != null)
            {
                producers = carRepository.GetCarProducers().ToList();
                txtCarId.Text = Car.CarId;
                txtCarName.Text = Car.CarName;
                txtCarModelYear.Text = Car.CarModelYear.ToString();
                txtColor.Text = Car.Color;
                txtCapacity.Text = Car.Capacity.ToString();
                txtDescription.Text = Car.Description;
                txtRentPrice.Text = Car.RentPrice.ToString();
                statuses.ForEach(status => cmbSatus.Items.Add(status));
                var statusIndex = statuses.FindIndex(status => status == Car.Status);
                cmbSatus.SelectedIndex = statusIndex;
                producers.ForEach(prod => cmbProducerId.Items.Add(prod.ProducerId));
                var producerIdIndex = producers.FindIndex(prod => prod.ProducerId.Equals(Car.ProducerId));
                cmbProducerId.SelectedIndex = producerIdIndex;
            }
        }
        private void GetCarInFo() => Car = new Car
        {
            CarId = txtCarId.Text,
            CarName = txtCarName.Text,
            CarModelYear = int.Parse(txtCarModelYear.Text),
            Color = txtColor.Text,  
            Description = txtDescription.Text,
            ImportDate = DateTime.Now,
            Capacity = int.Parse(txtCapacity.Text),
            RentPrice = decimal.Parse(txtRentPrice.Text),
            Status = statuses.ElementAt(cmbSatus.SelectedIndex),
            ProducerId = producers.ElementAt(cmbProducerId.SelectedIndex).ProducerId
        };

        private void btnClose_Click(object sender, RoutedEventArgs e) => this.Close();

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete?", "Delete Warning", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    carRepository.RemoveProduct(Car);
                    Close();
                    MessageBox.Show("Product " + Car.CarName+ " is deleted!", "Delete Product");
                    WindowCars windowCars = new WindowCars();
                    windowCars.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Car Error!");
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isValid = true;
                if (String.IsNullOrEmpty(txtCarId.Text.Trim()))
                {
                    isValid = false;
                    MessageBox.Show("CarId must be not empty");
                }
                else if (String.IsNullOrEmpty(txtCarName.Text.Trim()))
                {
                    isValid = false;
                    MessageBox.Show("Car name must be not empty");
                }
                else if (!int.TryParse(txtCarModelYear.Text, out int carModelYear))
                {
                    MessageBox.Show("Car Model Year must be a number");
                    isValid = false;
                }
                else if (String.IsNullOrEmpty(txtColor.Text.Trim()))
                {
                    isValid = false;
                    MessageBox.Show("Color must be not empty");
                }
                else if (!int.TryParse(txtCapacity.Text, out int capacity))
                {
                    MessageBox.Show("Car capacity must be a number");
                    isValid = false;
                }
                else if (String.IsNullOrEmpty(txtDescription.Text.Trim()))
                {
                    isValid = false;
                    MessageBox.Show("Description must be not empty");
                }
                else if (!decimal.TryParse(txtCapacity.Text, out decimal rentPrice))
                {
                    MessageBox.Show("Car rent price must be a number");
                    isValid = false;
                }
                if (isValid)
                {
                    GetCarInFo();
                    if (!isUpdate)
                    {
                        carRepository.AddProduct(Car);
                        MessageBox.Show("Car is created successfully!", "Create Car");
                        Close();
                    }
                    else
                    {
                        carRepository.UpdateProduct(Car);
                        MessageBox.Show("Car is updated successfully!", "Update Car");
                        Close();
                    }
                    WindowCars windowCars = new WindowCars();
                    windowCars.Show();
                }
            }
            catch(Exception ex)
            {
                if (!isUpdate) MessageBox.Show(ex.Message, "Creating car error");
                else MessageBox.Show(ex.Message, "Updating car error");
            }
        }
    }
}
