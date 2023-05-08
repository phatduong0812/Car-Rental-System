using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        StaffAccount GetLoginStaff(string email, string password);
        Customer GetLoginCustomer(string email, string password);
        IEnumerable<Customer> GetCustomers();
        IEnumerable<StaffAccount> GetStaffs();
        Customer GetCustomerByID(string customerId);
        StaffAccount GetStaffByID(string staffId);
        void AddCustomer(Customer customer);
        void AddStaff(StaffAccount staff);
        void UpdateCustomer(Customer customer);
        void UpdateStaff(StaffAccount staff);
        void RemoveStaff(StaffAccount staff);
        void RemoveCustomer(Customer customer);
    }
}
