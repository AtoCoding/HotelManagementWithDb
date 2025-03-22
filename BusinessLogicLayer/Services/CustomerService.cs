using BusinessLogicLayer.Bases;
using BusinessLogicLayer.Dtos;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.Configuration;

namespace BusinessLogicLayer.Services
{
    public class CustomerService : IService<CustomerDto>, IAccount<CustomerDto>
    {
        private static CustomerService _Instance = null!;
        private readonly CustomerRepository _CustomerRepository;

        private CustomerService()
        {
            _CustomerRepository = CustomerRepository.GetInstance();
        }

        public static CustomerService GetInstance() => _Instance ??= new CustomerService();

        public bool Add(CustomerDto data)
        {
            if (data != null)
            {
                Customer customer = new()
                {
                    CustomerFullName = data.CustomerFullName,
                    Telephone = data.Telephone,
                    EmailAddress = data.EmailAddress,
                    CustomerBirthday = data.CustomerBirthday,
                    CustomerStatus = data.CustomerStatus,
                    Password = data.Password,
                    BookingReservations = []
                };

                return _CustomerRepository.Add(customer);
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

        public CustomerDto? Get(int id)
        {
            return null!;
        }

        public List<CustomerDto> GetAll()
        {
            List<Customer> customers = _CustomerRepository.GetAll();
            List<CustomerDto> customerDtos = new List<CustomerDto>();

            foreach (var customer in customers)
            {
                customerDtos.Add(new CustomerDto()
                {
                    CustomerId = customer.CustomerId,
                    CustomerFullName = customer.CustomerFullName,
                    Telephone = customer.Telephone,
                    EmailAddress = customer.EmailAddress,
                    CustomerBirthday = customer.CustomerBirthday,
                    CustomerStatus = customer.CustomerStatus,
                    CustomerStatusName = "",
                    Password = customer.Password,
                    BookingReservations = customer.BookingReservations,
                });
            }

            ServiceCommon.SetCustomerStatusName(customerDtos);

            return customerDtos;
        }

        public List<CustomerDto> Search(string? description, string? typeName, int capacity)
        {
            return [];
        }

        public List<CustomerDto> Search(string? fullName, string? telephone, string? emailAddress)
        {
            List<Customer> customers = _CustomerRepository.Search(fullName, telephone, emailAddress);
            List<CustomerDto> customerDtos = new List<CustomerDto>();

            foreach (var customer in customers)
            {
                customerDtos.Add(new CustomerDto()
                {
                    CustomerId = customer.CustomerId,
                    CustomerFullName = customer.CustomerFullName,
                    Telephone = customer.Telephone,
                    EmailAddress = customer.EmailAddress,
                    CustomerBirthday = customer.CustomerBirthday,
                    CustomerStatus = customer.CustomerStatus,
                    CustomerStatusName = "",
                    Password = customer.Password,
                    BookingReservations = customer.BookingReservations,
                });
            }

            ServiceCommon.SetCustomerStatusName(customerDtos);

            return customerDtos;
        }

        public bool Update(CustomerDto data)
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

        public List<CustomerDto> GetList(int id)
        {
            return [];
        }
    }
}
