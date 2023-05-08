using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections;
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
    /// Interaction logic for WindowCarsRenting.xaml
    /// </summary>
    public partial class WindowCarsRenting : Window
    {
        IMemberRepository memberRepository;
        ICarRepository carRepository;
        List<Car> cars = null;
        List<Customer> customers = null;
        CarRental CarRental;
        public WindowCarsRenting()
        {
            InitializeComponent();
            memberRepository = new MemberRepository();
            carRepository = new CarRepository();
            cars = new List<Car>();
            customers = new List<Customer>();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cars = carRepository.GetCars().ToList();
            customers = memberRepository.GetCustomers().ToList();
            cars.ForEach(car => cmbCarId.Items.Add(car.CarId));
            cmbCarId.SelectedIndex = 0;
            customers.ForEach(cus => cmbCustomerId.Items.Add(cus.CustomerId));
            cmbCustomerId.SelectedIndex = 0;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isValid = true;
                if (!decimal.TryParse(txtRentPrice.Text, out decimal rentPrice))
                {
                    MessageBox.Show("Car rent price must be a number");
                    isValid = false;
                }
                else if (DateTime.Compare(dpPickUp.SelectedDate.Value, DateTime.Now) <= 0)
                {
                    isValid = false;
                    MessageBox.Show("Pick up date must now or future");
                }
                else if (DateTime.Compare(dpReturnDate.SelectedDate.Value, dpPickUp.SelectedDate.Value) <= 0)
                {
                    isValid = false;
                    MessageBox.Show("Return date must be after pick up date");
                }
                if (isValid)
                {
                    GetRentalInFo();
                    carRepository.CreateRentalCar(CarRental);
                    MessageBox.Show("Rental is created successfully!", "Create Rental");
                    Close();
                    WindowCarsRentingList windowCarsRentingList = new WindowCarsRentingList();
                    windowCarsRentingList.Show();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Creating car error");
            }
        }

        private void GetRentalInFo() => CarRental = new CarRental
        {
            CarId = cars.ElementAt(cmbCarId.SelectedIndex).CarId,
            CustomerId = customers.ElementAt(cmbCarId.SelectedIndex).CustomerId,
            PickupDate = (DateTime)dpPickUp.SelectedDate,
            ReturnDate = (DateTime)dpReturnDate.SelectedDate,
            RentPrice = decimal.Parse(txtRentPrice.Text),
        };
    }
}
