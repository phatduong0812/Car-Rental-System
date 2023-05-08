using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CarDAO
    {
        private static CarDAO instance = null;
        private static readonly object instanceLock = new object();
        private CarDAO() { }
        public static CarDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CarDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Car> GetCars()
        {
            List<Car> cars = null;
            try
            {
                var CarRentalSystemDB = new CarRentalSystemDBContext();
                cars = CarRentalSystemDB.Cars.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return cars;
        }

        public Car GetProductByID(string carId)
        {
            Car car = null;
            try
            {
                var CarRentalSystemDB = new CarRentalSystemDBContext();
                car = CarRentalSystemDB.Cars.SingleOrDefault(carr => carr.CarId == carId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return car;
        }

        public void AddProduct(Car product)
        {
            try
            {
                Car _product = GetProductByID(product.CarId);
                if (_product == null)
                {
                    var CarRentalSystemDB = new CarRentalSystemDBContext();
                    CarRentalSystemDB.Cars.Add(product);
                    CarRentalSystemDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The car is already exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateProduct(Car product)
        {
            try
            {
                Car _product = GetProductByID(product.CarId);
                if (_product != null)
                {
                    var CarRentalSystemDB = new CarRentalSystemDBContext();
                    CarRentalSystemDB.Entry<Car>(product).State = EntityState.Modified;
                    CarRentalSystemDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The product does not exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveProduct(Car product)
        {
            try
            {
                Car _product = GetProductByID(product.CarId);
                if (_product != null)
                {
                    var CarRentalSystemDB = new CarRentalSystemDBContext();
                    CarRentalSystemDB.Cars.Remove(product);
                    CarRentalSystemDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The product does not exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<CarProducer> GetCarProducers()
        {
            List<CarProducer> carProducers = null;
            try
            {
                var CarRentalSystemDB = new CarRentalSystemDBContext();
                carProducers = CarRentalSystemDB.CarProducers.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return carProducers;
        }

        public IEnumerable<CarRental> GetCarRentals()
        {
            List<CarRental> carRentals = null;
            try
            {
                var CarRentalSystemDB = new CarRentalSystemDBContext();
                carRentals = CarRentalSystemDB.CarRentals.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return carRentals;
        }

        public void CreateRentalCar(CarRental car)
        {
            try
            {
                var CarRentalSystemDB = new CarRentalSystemDBContext();
                var canRental = CarRentalSystemDB.CarRentals.SingleOrDefault(_car => _car.CustomerId.Equals(car.CustomerId) && _car.CarId.Equals(car.CarId));
                if (canRental == null)
                {
                    CarRentalSystemDB.CarRentals.Add(car);
                    CarRentalSystemDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The rent is already exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
