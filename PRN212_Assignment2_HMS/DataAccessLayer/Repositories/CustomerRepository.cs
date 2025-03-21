using DataAccessLayer.Bases;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private static CustomerRepository _Instance = null!;
        private readonly FuminiHotelManagementContext _Context;

        private CustomerRepository()
        {
            _Context = new FuminiHotelManagementContext();
        }

        public static CustomerRepository GetInstance() => _Instance ??= new CustomerRepository();

        public Customer? Add(Customer data)
        {
            _Context.Customers.Add(data);

            return _Context.SaveChanges() > 0 ? data : null;
        }

        public int Count()
        {
            return _Context.Customers.Count();
        }

        public bool Delete(int id)
        {
            Customer? customer = _Context.Customers.FirstOrDefault(x => x.CustomerId == id);

            _Context.Customers.Remove(customer ?? new());

            return _Context.SaveChanges() > 0;
        }

        public Customer? Get(int id)
        {
            return _Context.Customers.FirstOrDefault(x => x.CustomerId == id);
        }

        public List<Customer> GetAll()
        {
            return _Context.Customers.ToList();
        }

        public List<Customer> Search(string? description, string? typeName, int capacity)
        {
            return [];
        }

        public List<Customer> Search(string? fullName, string? telephone, string? emailAddress)
        {
            return [];
        }

        public Customer? Update(Customer data)
        {
            _Context.Customers.Update(data);

            return _Context.SaveChanges() > 0 ? data : null;
        }
    }
}
