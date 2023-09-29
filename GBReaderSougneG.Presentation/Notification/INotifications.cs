namespace GBReaderSougneG.Presentation.Notification
{
    public interface INotifications
    {
        void Display(NotificationUse severity, string title, string message);
    }
}