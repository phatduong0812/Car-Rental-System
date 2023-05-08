using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetCars();
        Car GetProductByID(string carId);
        void AddProduct(Car product);
        void UpdateProduct(Car product);
        void RemoveProduct(Car product);
        IEnumerable<CarProducer> GetCarProducers();
        IEnumerable<CarRental> GetCarRentals();
        void CreateRentalCar(CarRental car);
    }
}
