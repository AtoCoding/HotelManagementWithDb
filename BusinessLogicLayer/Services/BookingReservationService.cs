using BusinessLogicLayer.Bases;
using DataAccessLayer.Bases;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services
{
    public class BookingReservationService : Bases.IService<BookingReservation>
    {
        private static BookingReservationService _Instance = null!;
        private readonly BookingReservationRepository _BookingReservationRepository;

        private BookingReservationService()
        {
            _BookingReservationRepository = BookingReservationRepository.GetInstance();
        }

        public static BookingReservationService GetInstance() => _Instance ??= new BookingReservationService();

        public bool Add(BookingReservation data)
        {
            return _BookingReservationRepository.Add(data);
        }

        public int Count()
        {
            return _BookingReservationRepository.Count();
        }

        public bool Delete(int id)
        {
            return _BookingReservationRepository.Delete(id);
        }

        public BookingReservation? Get(int id)
        {
            return _BookingReservationRepository.Get(id);
        }

        public List<BookingReservation> GetAll()
        {
            return _BookingReservationRepository.GetAll();
        }

        public int GetNewId()
        {
            return -1;
        }

        public List<BookingReservation> Search(string? description, string? typeName, int capacity)
        {
            return _BookingReservationRepository.Search(description, typeName, capacity);
        }

        public List<BookingReservation> Search(string? fullName, string? telephone, string? emailAddress)
        {
            return _BookingReservationRepository.Search(fullName, telephone, emailAddress);
        }

        public bool Update(BookingReservation data)
        {
            return _BookingReservationRepository.Update(data);
        }

        public List<BookingReservation> GetList(int id)
        {
            return _BookingReservationRepository.GetList(id);
        }
    }
}
