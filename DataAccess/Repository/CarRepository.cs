using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CarRepository : ICarRepository
    {
        public void AddProduct(Car product) => CarDAO.Instance.AddProduct(product);

        public void CreateRentalCar(CarRental car) => CarDAO.Instance.CreateRentalCar(car); 

        public IEnumerable<CarProducer> GetCarProducers() => CarDAO.Instance.GetCarProducers();

        public IEnumerable<CarRental> GetCarRentals() => CarDAO.Instance.GetCarRentals();   

        public IEnumerable<Car> GetCars() => CarDAO.Instance.GetCars(); 

        public Car GetProductByID(string carId) => CarDAO.Instance.GetProductByID(carId);   

        public void RemoveProduct(Car product) => CarDAO.Instance.RemoveProduct(product);

        public void UpdateProduct(Car product) => CarDAO.Instance.UpdateProduct(product);
    }
}
