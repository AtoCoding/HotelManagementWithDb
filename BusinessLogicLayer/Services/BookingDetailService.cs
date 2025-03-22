using BusinessLogicLayer.Bases;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services
{
    public class BookingDetailService : IService<BookingDetail>
    {
        private static BookingDetailService _Instance = null!;
        private readonly BookingDetailRepository _BookingDetailRepository;

        private BookingDetailService()
        {
            _BookingDetailRepository = BookingDetailRepository.GetInstance();
        }

        public static BookingDetailService GetInstance() => _Instance ??= new BookingDetailService();

        public bool Add(BookingDetail data)
        {
            return _BookingDetailRepository.Add(data);
        }

        public int Count()
        {
            return _BookingDetailRepository.Count();
        }

        public bool Delete(int id)
        {
            return _BookingDetailRepository.Delete(id);
        }

        public BookingDetail? Get(int id)
        {
            return _BookingDetailRepository.Get(id);
        }

        public List<BookingDetail> GetAll()
        {
            return _BookingDetailRepository.GetAll();
        }

        public List<BookingDetail> GetList(int id)
        {
            return [];
        }

        public int GetNewId()
        {
            return -1;
        }

        public List<BookingDetail> Search(string? description, string? typeName, int capacity)
        {
            return _BookingDetailRepository.Search(description, typeName, capacity);
        }

        public List<BookingDetail> Search(string? fullName, string? telephone, string? emailAddress)
        {
            return _BookingDetailRepository.Search(fullName, telephone, emailAddress);
        }

        public bool Update(BookingDetail data)
        {
            return _BookingDetailRepository.Update(data);
        }
    }
}
