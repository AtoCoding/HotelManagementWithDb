using DataAccessLayer.Bases;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class RoomInformationRepository : IRepository<RoomInformation>
    {
        private static RoomInformationRepository _Instance = null!;
        private readonly IRepository<BookingDetail> _BookingDetailsRepository;
        private readonly FuminiHotelManagementContext _Context;

        private RoomInformationRepository()
        {
            _Context = new FuminiHotelManagementContext();
            _BookingDetailsRepository = BookingDetailRepository.GetInstance();
        }

        public static RoomInformationRepository GetInstance() => _Instance ??= new RoomInformationRepository();

        public bool Add(RoomInformation data)
        {
            _Context.RoomInformations.Add(data);

            return _Context.SaveChanges() > 0;
        }

        public int Count()
        {
            return _Context.RoomInformations.Count();
        }

        public bool Delete(int id)
        {
            RoomInformation? roomInformation = _Context.RoomInformations.FirstOrDefault(x => x.RoomId == id);

            bool isExisted = _BookingDetailsRepository.GetAll().Any(x => x.RoomId == id);

            if (isExisted)
            {
                roomInformation!.RoomStatus = 2;
                _Context.RoomInformations.Update(roomInformation);
            }
            else
            {
                _Context.RoomInformations.Remove(roomInformation ?? new());
            }

            return _Context.SaveChanges() > 0;
        }

        public RoomInformation? Get(int id)
        {
            return _Context.RoomInformations.FirstOrDefault(x => x.RoomId == id);
        }

        public int GetNewId()
        {
            return _Context.RoomInformations.OrderByDescending(x => x.RoomId).FirstOrDefault()?.RoomId + 1 ?? 1;
        }

        public List<RoomInformation> GetAll()
        {
            return _Context.RoomInformations.Include(x => x.RoomType).ToList();
        }

        public List<RoomInformation> Search(string? description, string? typeName, int capacity)
        {
            List<RoomInformation> result = GetAll().ToList();

            if (!string.IsNullOrEmpty(description))
            {
                result.RemoveAll(x => !x.RoomDetailDescription!.ToLower().Contains(description.ToLower()));
            }
            if (!string.IsNullOrEmpty(typeName))
            {
                result.RemoveAll(x => !x.RoomType!.RoomTypeName!.ToLower().Contains(typeName.ToLower()));
            }
            if (capacity > 0)
            {
                result.RemoveAll(x => x.RoomMaxCapacity != capacity);
            }

            return result;
        }

        public List<RoomInformation> Search(string? fullName, string? telephone, string? emailAddress)
        {
            return [];
        }

        public bool Update(RoomInformation data)
        {
            return _Context.SaveChanges() > 0;
        }

        public List<RoomInformation> GetList(int id)
        {
            return [];
        }
    }
}
