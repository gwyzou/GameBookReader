using GBReaderSougneG.Presentation.IViews;
using GBReaderSougneG.Presentation.Notification;
using GBReaderSougneG.Presentation.Switch;
using GBReaderSougneG.Repositories;
using GBReaderSougneG.Repositories.Exceptions.History;

namespace GBReaderSougneG.Presentation
{
    public class HistoryViewPresenter : BasicPresenter
    {
        private readonly IHistoryView _view;
        private readonly IGoToViews _router;
        private readonly INotifications _notifications;
        private readonly IGeneralRepo _repository;

        public HistoryViewPresenter(IHistoryView historyView, IGoToViews router, INotifications notifications,
            IGeneralRepo repo, ICommunicationPresenter communicationPresenter)
        {
            _view = CheckAndThrow(historyView, nameof(historyView));
            _router = CheckAndThrow(router, nameof(router));
            _notifications = CheckAndThrow(notifications, nameof(notifications));
            _repository = CheckAndThrow(repo, nameof(repo));
            CheckAndThrow(communicationPresenter, nameof(communicationPresenter)).GoToHistory += this.OnEnter;

            SubscribeEvent();
        }

        private void OnEnter(object? sender, EventArgs e)
        {
            try
            {
                _view.SetHistories(_repository.History.LoadAllHistoriesBook());
            }
            catch (CannotReadException ex)
            {
                _notifications.Display(NotificationUse.Error,"Loading Error",ex.Message);
            }
        }

    private void SubscribeEvent() => _view.GoMainPage += this.GoMainView;

        private void GoMainView(object? sender, EventArgs e) => _router.Goto(SwitchNames.StartPage);
    }
}