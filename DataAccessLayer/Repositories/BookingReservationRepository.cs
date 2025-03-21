using DataAccessLayer.Bases;
using DataAccessLayer.Entities;

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

        public BookingReservation? Add(BookingReservation data)
        {
            _Context.BookingReservations.Add(data);

            return _Context.SaveChanges() > 0 ? data : null;
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

        public BookingReservation? Get(int id)
        {
            return _Context.BookingReservations.FirstOrDefault(x => x.BookingReservationId == id);
        }

        public List<BookingReservation> GetAll()
        {
            return _Context.BookingReservations.ToList();
        }

        public List<BookingReservation> Search(string? description, string? typeName, int capacity)
        {
            return [];
        }

        public List<BookingReservation> Search(string? fullName, string? telephone, string? emailAddress)
        {
            return [];
        }

        public BookingReservation? Update(BookingReservation data)
        {
            _Context.BookingReservations.Update(data);

            return _Context.SaveChanges() > 0 ? data : null;
        }
    }
}
