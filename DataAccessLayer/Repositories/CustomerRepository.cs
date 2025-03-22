using DataAccessLayer.Bases;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

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

        public bool Add(Customer data)
        {
            _Context.Customers.Add(data);

            return _Context.SaveChanges() > 0;
        }

        public int Count()
        {
            return _Context.Customers.Count();
        }

        public bool Delete(int id)
        {
            Customer? customer = _Context.Customers.Include(x => x.BookingReservations).FirstOrDefault(x => x.CustomerId == id);

            if(customer?.BookingReservations.Count == 0)
            {
                _Context.Customers.Remove(customer ?? new());
            } 
            else
            {
                customer!.CustomerStatus = 2;
                _Context.Customers.Update(customer);
            }

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
            List<Customer> result = GetAll().ToList();

            if (!string.IsNullOrEmpty(fullName))
            {
                result.RemoveAll(x => !x.CustomerFullName!.ToLower().Contains(fullName.ToLower()));
            }
            if (!string.IsNullOrEmpty(telephone))
            {
                result.RemoveAll(x => !x.Telephone!.ToLower().Contains(telephone.ToLower()));
            }
            if (!string.IsNullOrEmpty(emailAddress))
            {
                result.RemoveAll(x => !x.EmailAddress!.ToLower().Contains(emailAddress.ToLower()));
            }

            return result;
        }

        public bool Update(Customer data)
        {
            return _Context.SaveChanges() > 0;
        }

        public int GetNewId()
        {
            return _Context.Customers.OrderByDescending(x => x.CustomerId).FirstOrDefault()?.CustomerId + 1 ?? 1;
        }

        public List<Customer> GetList(int id)
        {
            return [];
        }
    }
}
