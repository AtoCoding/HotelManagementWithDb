using BusinessLogicLayer.Bases;
using BusinessLogicLayer.Dtos;
using DataAccessLayer.Repositories;

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

        public CustomerDto? Add(CustomerDto data)
        {
            return null!;
        }

        public (bool isAuthen, string role) CheckAuth(string email, string password)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            return _CustomerRepository.Count();
        }

        public bool Delete(int id)
        {
            return false;
        }

        public CustomerDto? Get(int id)
        {
            return null!;
        }

        public List<CustomerDto> GetAll()
        {
            return [];
        }

        public List<CustomerDto> Search(string? description, string? typeName, int capacity)
        {
            return [];
        }

        public List<CustomerDto> Search(string? fullName, string? telephone, string? emailAddress)
        {
            return [];
        }

        public CustomerDto? Update(CustomerDto data)
        {
            return null!;
        }
    }
}
