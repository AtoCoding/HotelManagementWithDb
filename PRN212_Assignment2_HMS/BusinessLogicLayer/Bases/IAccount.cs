namespace BusinessLogicLayer.Bases
{
    public interface IAccount<T>
    {
        (bool isAuthen, string role) CheckAuth(string email, string password);
    }
}
