namespace BusinessLogicLayer.Bases
{
    public interface IService<T>
    {
        bool Add(T data);
        T? Update(T data);
        bool Delete(int id);
        T? Get(int id);
        List<T> GetAll();
        int Count();
        List<T> Search(string? description, string? typeName, int capacity);
        List<T> Search(string? fullName, string? telephone, string? emailAddress);
    }
}
