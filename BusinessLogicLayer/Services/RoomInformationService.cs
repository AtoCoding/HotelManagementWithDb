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
            return _RoomInformationRepository.Add(data);
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

        public int GetNewId()
        {
            return -1;
        }

        public List<RoomInformation> Search(string? description, string? typeName, int capacity)
        {
            return _RoomInformationRepository.Search(description, typeName, capacity);
        }

        public List<RoomInformation> Search(string? fullName, string? telephone, string? emailAddress)
        {
            return _RoomInformationRepository.Search(fullName, telephone, emailAddress);
        }

        public bool Update(RoomInformation data)
        {
            return _RoomInformationRepository.Update(data); 
        }
    }
}
