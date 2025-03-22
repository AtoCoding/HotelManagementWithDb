using BusinessLogicLayer.Bases;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services
{
    public class RoomTypeService : IService<RoomType>
    {
        private static RoomTypeService _Instance = null!;
        private readonly RoomTypeRepository _RoomTypeRepository;

        private RoomTypeService()
        {
            _RoomTypeRepository = RoomTypeRepository.GetInstance();
        }

        public static RoomTypeService GetInstance() => _Instance ??= new RoomTypeService();

        public bool Add(RoomType data)
        {
            return _RoomTypeRepository.Add(data);
        }

        public int Count()
        {
            return _RoomTypeRepository.Count();
        }

        public bool Delete(int id)
        {
            return _RoomTypeRepository.Delete(id);
        }

        public RoomType? Get(int id)
        {
            return _RoomTypeRepository.Get(id);
        }

        public List<RoomType> GetAll()
        {
            return _RoomTypeRepository.GetAll();
        }

        public List<RoomType> GetList(int id)
        {
            return [];
        }

        public int GetNewId()
        {
            return -1;
        }

        public List<RoomType> Search(string? description, string? typeName, int capacity)
        {
            return _RoomTypeRepository.Search(description, typeName, capacity);
        }

        public List<RoomType> Search(string? fullName, string? telephone, string? emailAddress)
        {
            return _RoomTypeRepository.Search(fullName, telephone, emailAddress);
        }

        public List<RoomType> Search(int customerId)
        {
            return [];
        }

        public bool Update(RoomType data)
        {
            return _RoomTypeRepository.Update(data);
        }
    }
}
