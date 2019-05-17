namespace ReWork.Logic.Hubs.Abstraction
{
    public interface INotificationHub
    {
        void ConnectUser(string userId);
        void RefreshNotifications(string userId, string notificationsJson);
    }
}
