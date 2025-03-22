namespace DataAccessLayer.Bases
{
    public interface IRepository<T>
    {
        bool Add(T data);
        bool Update(T data);
        bool Delete(int id);
        T? Get(int id);
        List<T> GetList(int id);
        List<T> GetAll();
        int Count();
        List<T> Search(string? description, string? typeName, int capacity);
        List<T> Search(string? fullName, string? telephone, string? emailAddress);
        int GetNewId();
    }
}
