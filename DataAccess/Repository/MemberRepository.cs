using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public void AddCustomer(Customer customer) => MemberDAO.Instance.AddCustomer(customer);

        public void AddStaff(StaffAccount staff) => MemberDAO.Instance.AddStaff(staff);

        public Customer GetCustomerByID(string customerId) => MemberDAO.Instance.GetCustomerByID(customerId);

        public IEnumerable<Customer> GetCustomers() => MemberDAO.Instance.GetCustomers();

        public Customer GetLoginCustomer(string email, string password) => MemberDAO.Instance.LoginCustomer(email, password);   

        public StaffAccount GetLoginStaff(string email, string password) => MemberDAO.Instance.LoginStaff(email, password);

        public StaffAccount GetStaffByID(string staffId) => MemberDAO.Instance.GetStaffByID(staffId);

        public IEnumerable<StaffAccount> GetStaffs() => MemberDAO.Instance.GetStaffs();

        public void RemoveCustomer(Customer customer) => MemberDAO.Instance.RemoveCustomer(customer);

        public void RemoveStaff(StaffAccount staff) => MemberDAO.Instance.RemoveStaff(staff);   

        public void UpdateCustomer(Customer customer) => MemberDAO.Instance.UpdateCustomer(customer);

        public void UpdateStaff(StaffAccount staff) => MemberDAO.Instance.UpdateStaff(staff);
    }
}
