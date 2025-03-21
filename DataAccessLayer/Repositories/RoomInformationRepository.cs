using DataAccessLayer.Bases;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public class RoomInformationRepository : IRepository<RoomInformation>
    {
        private static RoomInformationRepository _Instance = null!;
        private readonly FuminiHotelManagementContext _Context;

        private RoomInformationRepository()
        {
            _Context = new FuminiHotelManagementContext();
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

            _Context.RoomInformations.Remove(roomInformation ?? new());

            return _Context.SaveChanges() > 0;
        }

        public RoomInformation? Get(int id)
        {
            return _Context.RoomInformations.FirstOrDefault(x => x.RoomId == id);
        }

        public int GetNewId()
        {
            return -1;
        }

        public List<RoomInformation> GetAll()
        {
            return _Context.RoomInformations.ToList();
        }

        public List<RoomInformation> Search(string? description, string? typeName, int capacity)
        {
            return [];
        }

        public List<RoomInformation> Search(string? fullName, string? telephone, string? emailAddress)
        {
            return [];
        }

        public bool Update(RoomInformation data)
        {
            _Context.RoomInformations.Update(data);

            return _Context.SaveChanges() > 0;
        }
    }
}
