using DataAccessLayer.Bases;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public class RoomTypeRepository : IRepository<RoomType>
    {
        private static RoomTypeRepository _Instance = null!;
        private readonly FuminiHotelManagementContext _Context;

        private RoomTypeRepository()
        {
            _Context = new FuminiHotelManagementContext();
        }

        public static RoomTypeRepository GetInstance() => _Instance ??= new RoomTypeRepository();

        public bool Add(RoomType data)
        {
            _Context.RoomTypes.Add(data);
            
            return _Context.SaveChanges() > 0;
        }

        public int Count()
        {
            return _Context.RoomTypes.Count();
        }

        public bool Delete(int id)
        {
            RoomType? roomType = _Context.RoomTypes.FirstOrDefault(x => x.RoomTypeId == id);

            _Context.RoomTypes.Remove(roomType ?? new());

            return _Context.SaveChanges() > 0;
        }

        public RoomType? Get(int id)
        {
            return _Context.RoomTypes.FirstOrDefault(x => x.RoomTypeId == id);
        }

        public List<RoomType> GetAll()
        {
            return _Context.RoomTypes.ToList();
        }

        public int GetNewId()
        {
            return -1;
        }

        public List<RoomType> Search(string? description, string? typeName, int capacity)
        {
            return [];
        }

        public List<RoomType> Search(string? fullName, string? telephone, string? emailAddress)
        {
            return [];
        }

        public bool Update(RoomType data)
        {
            _Context.RoomTypes.Update(data);

            return _Context.SaveChanges() > 0;
        }
    }
}
