namespace ReWork.Logic.Hubs.Abstraction
{
    public interface INotificationHub
    {
        void RefreshNotifications(string userId, string notificationsJson);
    }
}
