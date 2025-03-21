using DataAccessLayer.Bases;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public class BookingDetailRepository : IRepository<BookingDetail>
    {
        private static BookingDetailRepository _Instance = null!;
        private readonly FuminiHotelManagementContext _Context;

        private BookingDetailRepository()
        {
            _Context = new FuminiHotelManagementContext();
        }

        public static BookingDetailRepository GetInstance() => _Instance ??= new BookingDetailRepository();

        public bool Add(BookingDetail data)
        {
            _Context.BookingDetails.Add(data);

            return _Context.SaveChanges() > 0;
        }

        public int Count()
        {
            return _Context.BookingDetails.Count();
        }

        public bool Delete(int id)
        {
            BookingDetail? bookingDetail = _Context.BookingDetails.FirstOrDefault(x => x.BookingReservationId == id);

            _Context.BookingDetails.Remove(bookingDetail ?? new());

            return _Context.SaveChanges() > 0;
        }

        public BookingDetail? Get(int id)
        {
            return _Context.BookingDetails.FirstOrDefault(x => x.BookingReservationId == id);
        }

        public List<BookingDetail> GetAll()
        {
            return _Context.BookingDetails.ToList();
        }

        public int GetNewId()
        {
            return -1;
        }

        public List<BookingDetail> Search(string? description, string? typeName, int capacity)
        {
            return [];
        }

        public List<BookingDetail> Search(string? fullName, string? telephone, string? emailAddress)
        {
            return [];
        }

        public bool Update(BookingDetail data)
        {
            _Context.BookingDetails.Update(data);

            return _Context.SaveChanges() > 0;
        }
    }
}
