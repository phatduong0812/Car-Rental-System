using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public StaffAccount LoginStaff(string email, string password)
        {
            StaffAccount staff = null;
            try
            {
                var CarRentalSystemDB = new CarRentalSystemDBContext();
                staff = CarRentalSystemDB.StaffAccounts.SingleOrDefault(member => member.Email == email && member.Password == password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return staff;
        }

        public Customer LoginCustomer(string email, string password)
        {
            Customer customer = null;
            try
            {
                var CarRentalSystemDB = new CarRentalSystemDBContext();
                customer = CarRentalSystemDB.Customers.SingleOrDefault(member => member.CustomerEmail == email && member.CustomerPassword == password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customer;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            List<Customer> customers = null;
            try
            {
                var CarRentalSystemDB = new CarRentalSystemDBContext();
                customers = CarRentalSystemDB.Customers.ToList();
            }
            catch (Exception e)
            {
                Exception exception = new(e.Message);
                throw exception;
            }

            return customers;
        }

        public IEnumerable<StaffAccount> GetStaffs()
        {
            List<StaffAccount> staffs = null;
            try
            {
                var CarRentalSystemDB = new CarRentalSystemDBContext();
                staffs = CarRentalSystemDB.StaffAccounts.ToList();
            }
            catch (Exception e)
            {
                Exception exception = new(e.Message);
                throw exception;
            }

            return staffs;
        }

        public Customer GetCustomerByID(string customerId)
        {
            Customer customer = null;
            try
            {
                var CarRentalSystemDB = new CarRentalSystemDBContext();
                customer = CarRentalSystemDB.Customers.SingleOrDefault(m => m.CustomerId.Equals(customerId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customer;
        }

        public StaffAccount GetStaffByID(string staffId)
        {
            StaffAccount staff = null;
            try
            {
                var CarRentalSystemDB = new CarRentalSystemDBContext();
                staff = CarRentalSystemDB.StaffAccounts.SingleOrDefault(m => m.StaffId.Equals(staffId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return staff;
        }

        private bool CheckIsCustomerEmailExisted(string email)
        {
            bool flag = true;
            var CarRentalSystemDB = new CarRentalSystemDBContext();
            Customer customer = CarRentalSystemDB.Customers.SingleOrDefault(cus => cus.CustomerEmail.Equals(email));
            if (customer != null) flag = true;
            else flag = false;
            return flag;

        }

        private bool CheckIsStaffEmailExisted(string email)
        {
            bool flag = true;
            var CarRentalSystemDB = new CarRentalSystemDBContext();
            StaffAccount staff = CarRentalSystemDB.StaffAccounts.SingleOrDefault(cus => cus.StaffId.Equals(email));
            if (staff != null) flag = true;
            else flag = false;
            return flag;

        }

        public void AddCustomer(Customer customer)
        {
            try
            {
                Customer _customer = GetCustomerByID(customer.CustomerId);
                if (_customer == null)
                {
                    var CarRentalSystemDB = new CarRentalSystemDBContext();
                    if (CheckIsCustomerEmailExisted(customer.CustomerEmail))
                    {
                        throw new Exception("The Email is already exist!");
                    }
                    CarRentalSystemDB.Customers.Add(customer);
                    CarRentalSystemDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The customer is already exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddStaff(StaffAccount staff)
        {
            try
            {
                StaffAccount _staff = GetStaffByID(staff.StaffId);
                if (_staff == null)
                {
                    var CarRentalSystemDB = new CarRentalSystemDBContext();
                    if (CheckIsStaffEmailExisted(staff.Email))
                    {
                        throw new Exception("The Email is already exist!");
                    }
                    CarRentalSystemDB.StaffAccounts.Add(staff);
                    CarRentalSystemDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The Staff is already exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            try
            {
                Customer _customer = GetCustomerByID(customer.CustomerId);
                if (_customer != null)
                {
                    var CarRentalSystemDB = new CarRentalSystemDBContext();
                    CarRentalSystemDB.Entry<Customer>(customer).State = EntityState.Modified;
                    CarRentalSystemDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The customer does not exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateStaff(StaffAccount staff)
        {
            try
            {
                StaffAccount _staff = GetStaffByID(staff.StaffId);
                if (_staff != null)
                {
                    var CarRentalSystemDB = new CarRentalSystemDBContext();
                    CarRentalSystemDB.Entry<StaffAccount>(staff).State = EntityState.Modified;
                    CarRentalSystemDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The staff does not exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveStaff(StaffAccount staff)
        {
            try
            {
                StaffAccount _staff = GetStaffByID(staff.StaffId);
                if (_staff != null)
                {
                    var CarRentalSystemDB = new CarRentalSystemDBContext();
                    CarRentalSystemDB.StaffAccounts.Remove(staff);
                    CarRentalSystemDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The Staff does not exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveCustomer(Customer customer)
        {
            try
            {
                Customer _customer = GetCustomerByID(customer.CustomerId);
                if (_customer != null)
                {
                    var CarRentalSystemDB = new CarRentalSystemDBContext();
                    CarRentalSystemDB.Customers.Remove(customer);
                    CarRentalSystemDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The Staff does not exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
