using System.Net.Mail;
using DataAccessLayer.Bases;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class BookingReservationRepository : IRepository<BookingReservation>
    {
        private static BookingReservationRepository _Instance = null!;
        private readonly FuminiHotelManagementContext _Context;

        private BookingReservationRepository()
        {
            _Context = new FuminiHotelManagementContext();
        }

        public static BookingReservationRepository GetInstance() => _Instance ??= new BookingReservationRepository();

        public bool Add(BookingReservation data)
        {
            _Context.BookingReservations.Add(data);

            return _Context.SaveChanges() > 0;
        }

        public int Count()
        {
            return _Context.BookingReservations.Count();
        }

        public bool Delete(int id)
        {
            BookingReservation? bookingReservation = _Context.BookingReservations.FirstOrDefault(x => x.BookingReservationId == id);

            _Context.BookingReservations.Remove(bookingReservation ?? new());

            return _Context.SaveChanges() > 0;
        }

        public int GetNewId()
        {
            return _Context.BookingReservations.OrderByDescending(x => x.BookingReservationId).FirstOrDefault()?.BookingReservationId + 1 ?? 1;
        }

        public BookingReservation? Get(int id)
        {
            return _Context.BookingReservations.FirstOrDefault(x => x.BookingReservationId == id);
        }

        public List<BookingReservation> GetAll()
        {
            return _Context.BookingReservations.Include(x => x.Customer)
                                               .Include(x => x.BookingDetails)
                                                    .ThenInclude(x => x.Room)
                                                        .ThenInclude(x => x.RoomType)
                                               .ToList();
        }

        public List<BookingReservation> Search(string? description, string? typeName, int capacity)
        {
            return [];
        }

        public List<BookingReservation> Search(string? fullName, string? telephone, string? emailAddress)
        {
            return [];
        }

        public bool Update(BookingReservation data)
        {
            _Context.BookingReservations.Update(data);

            return _Context.SaveChanges() > 0;
        }

        public List<BookingReservation> GetList(int id)
        {
            return _Context.BookingReservations.Where(x => x.CustomerId == id)
                                               .Include(x => x.BookingDetails)
                                                    .ThenInclude(x => x.Room)
                                                        .ThenInclude(x => x.RoomType)
                                               .ToList();
        }

        public List<BookingReservation> Search(int customerId)
        {
            List<BookingReservation> result = GetAll().ToList();

            if (customerId != 0)
            {
                result.RemoveAll(x => x.CustomerId != customerId);
            } 
            else
            {
                return GetAll().ToList();
            }

            return result;
        }
    }
}
