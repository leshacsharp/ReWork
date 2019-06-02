namespace ReWork.Logic.Hubs.Abstraction
{
    public interface IUserHub
    {
        void RefreshUsersCounter();
        void CheckUserStatus(string userId);
    }
}
