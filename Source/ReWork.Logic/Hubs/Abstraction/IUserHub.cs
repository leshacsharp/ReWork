namespace ReWork.Logic.Hubs.Abstraction
{
    public interface IUserHub
    {
        void RefreshUsersCounter();
        void IsOnline(string userId);
    }
}
