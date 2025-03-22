using BusinessLogicLayer.Bases;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.Configuration;

namespace BusinessLogicLayer.Services
{
    public class CustomerService : IService<Customer>, IAccount<Customer>
    {
        private static CustomerService _Instance = null!;
        private readonly CustomerRepository _CustomerRepository;

        private CustomerService()
        {
            _CustomerRepository = CustomerRepository.GetInstance();
        }

        public static CustomerService GetInstance() => _Instance ??= new CustomerService();

        public bool Add(Customer data)
        {
            if (data != null)
            {
                return _CustomerRepository.Add(data);
            }

            return false;
        }

        public (bool isAuthen, string role) CheckAuth(string email, string password)
        {
            IConfiguration config = Utils.GetConfiguration();

            var jsonEmail = config["AdminAccount:Email"];
            var jsonPassword = config["AdminAccount:Password"];

            bool check = string.Equals(email, jsonEmail) && string.Equals(password, jsonPassword);

            if (check)
            {
                return (true, "Admin");
            }
            else
            {
                List<Customer> customers = _CustomerRepository.GetAll();
                bool isExisted = customers.FirstOrDefault(x => x.EmailAddress == email && x.Password == password && x.CustomerStatus == 1) != null;
                return isExisted ? (isExisted, "Customer") : (isExisted, "Guest");
            }
        }

        public int Count()
        {
            return _CustomerRepository.Count();
        }

        public bool Delete(int id)
        {
            return _CustomerRepository.Delete(id);
        }

        public Customer? Get(int id)
        {
            return null!;
        }

        public List<Customer> GetAll()
        {
            return _CustomerRepository.GetAll();
        }

        public List<Customer> Search(string? description, string? typeName, int capacity)
        {
            return [];
        }

        public List<Customer> Search(string? fullName, string? telephone, string? emailAddress)
        {
            return _CustomerRepository.Search(fullName, telephone, emailAddress);
        }

        public bool Update(Customer data)
        {
            Customer? customer = _CustomerRepository.Get(data.CustomerId);

            if (customer != null)
            {
                customer.CustomerFullName = data.CustomerFullName;
                customer.Telephone = data.Telephone;
                customer.EmailAddress = data.EmailAddress;
                customer.CustomerBirthday = data.CustomerBirthday;
                customer.CustomerStatus = data.CustomerStatus;
                customer.Password = data.Password;

                return _CustomerRepository.Update(customer);
            }

            return false;
        }

        public int GetNewId()
        {
            return _CustomerRepository.GetNewId();
        }

        public List<Customer> GetList(int id)
        {
            return [];
        }

        public List<Customer> Search(int customerId)
        {
            return [];
        }
    }
}
