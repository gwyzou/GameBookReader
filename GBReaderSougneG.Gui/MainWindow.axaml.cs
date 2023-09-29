using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using GBReaderSougneG.Presentation.Notification;
using GBReaderSougneG.Presentation.Switch;


namespace GBReaderSougneG.Avalonia
{

    public partial class MainWindow : Window, IGoToViews, INotifications
    {
        private readonly WindowNotificationManager _notificationManager;
        private readonly IDictionary<SwitchNames, UserControl> _pages = new Dictionary<SwitchNames, UserControl>();
        public MainWindow()
        {
            InitializeComponent();
            _notificationManager = new WindowNotificationManager(this)
            {
                Position = NotificationPosition.BottomRight
            };
        }
        internal void RegisterPage(SwitchNames pageName, UserControl page)
        {
            _pages[pageName] = page;
            Content ??= page;
        }

        public void Goto(SwitchNames pageNames) => Content = _pages[pageNames];

        public void Display(NotificationUse severity, string title, string message)
        {
            var notification = new Notification(title, message, severity switch
            {
                NotificationUse.Info => NotificationType.Information,
                NotificationUse.Error => NotificationType.Error,
                NotificationUse.Success => NotificationType.Success,
                _ => NotificationType.Warning,
            });

            if(IsVisible)
            {
                _notificationManager.Show(notification);
            }        
        }
        
    }
}