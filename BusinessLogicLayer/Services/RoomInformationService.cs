using BusinessLogicLayer.Bases;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services
{
    public class RoomInformationService : IService<RoomInformation>
    {
        private static RoomInformationService _Instance = null!;
        private readonly RoomInformationRepository _RoomInformationRepository;

        private RoomInformationService()
        {
            _RoomInformationRepository = RoomInformationRepository.GetInstance();
        }

        public static RoomInformationService GetInstance() => _Instance ??= new RoomInformationService();

        public bool Add(RoomInformation data)
        {
            if (data != null)
            {
                return _RoomInformationRepository.Add(data);
            }

            return false;
        }

        public int Count()
        {
            return _RoomInformationRepository.Count();
        }

        public bool Delete(int id)
        {
            return _RoomInformationRepository.Delete(id);
        }

        public RoomInformation? Get(int id)
        {
            return _RoomInformationRepository.Get(id);
        }

        public List<RoomInformation> GetAll()
        {
            return _RoomInformationRepository.GetAll();
        }

        public List<RoomInformation> GetList(int id)
        {
            return [];
        }

        public int GetNewId()
        {
            return _RoomInformationRepository.GetNewId();
        }

        public List<RoomInformation> Search(string? description, string? typeName, int capacity)
        {
            return _RoomInformationRepository.Search(description, typeName, capacity);
        }

        public List<RoomInformation> Search(string? fullName, string? telephone, string? emailAddress)
        {
            return [];
        }

        public List<RoomInformation> Search(int customerId)
        {
            return [];
        }

        public bool Update(RoomInformation data)
        {
            RoomInformation? roomInformation = _RoomInformationRepository.Get(data.RoomId);

            if (roomInformation != null)
            {
                roomInformation.RoomNumber = data.RoomNumber;
                roomInformation.RoomDetailDescription = data.RoomDetailDescription;
                roomInformation.RoomMaxCapacity = data.RoomMaxCapacity;
                roomInformation.RoomTypeId = data.RoomTypeId;
                roomInformation.RoomStatus = data.RoomStatus;
                roomInformation.RoomPricePerDay = data.RoomPricePerDay;

                return _RoomInformationRepository.Update(roomInformation);
            }

            return false;
        }
    }
}
