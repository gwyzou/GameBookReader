using GBReaderSougneG.Presentation.IViews;
using GBReaderSougneG.Presentation.Notification;
using GBReaderSougneG.Presentation.Switch;
using GBReaderSougneG.Repositories;
using GBReaderSougneG.Repositories.Exceptions.History;
using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Presentation
{
    public class GameViewPresenter : BasicPresenter
    {
        private readonly IGameView _view;
        private readonly IGoToViews _router;
        private readonly INotifications _notifications; 
        private readonly IGeneralRepo _repository; 
        private readonly ICommunicationPresenter _communicationPresenter;
        public GameViewPresenter(IGameView gameView,IGoToViews router, INotifications notifications,IGeneralRepo repo,ICommunicationPresenter communicationPresenter)
        {
            _view = CheckAndThrow(gameView, nameof(gameView));
            _router = CheckAndThrow(router, nameof(router));
            _notifications = CheckAndThrow(notifications, nameof(notifications));
            _repository = CheckAndThrow(repo, nameof(repo));
            _communicationPresenter = CheckAndThrow(communicationPresenter, nameof(communicationPresenter));

            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            _view.GoMainPage += this.GoMainView;
            _view.GoToPage += this.GoToPage;

            _communicationPresenter.BookChanged += this.NewBookPlayStatus;
        }

        private void NewBookPlayStatus(object? obj,CoverBook args)
        {
            _view.SetBookInfo(args);
            if (_repository.BookStorage.Book != null)
            {
                ChangePage(_repository.BookStorage.Book.GetPage(_repository.History.GameHistory.GetLastPage()));
            }
        }

        private void GoToPage(object? obj,int pageNumber)
        {
            _repository.History.GameHistory.AddHistory(new BookHistoryNode(DateTime.Now, pageNumber));

            UploadHistory();
            if (_repository.BookStorage.Book != null)
            {
                ChangePage(_repository.BookStorage.Book.GetPage(pageNumber));
            }
        }

        private void ChangePage(Page page)
        {
            if (page.GetChoicesList().Count==0)
            {
                _communicationPresenter.EndGame(page);
                _router.Goto(SwitchNames.EndPage);
            }
            else
            {
                _view.SetPage(page);
            }
        }

        private void GoMainView(object? obj,EventArgs args) => _router.Goto(SwitchNames.StartPage);

        private void UploadHistory()
        {
            try
            {
                _repository.History.SaveData();
            }
            catch (CannotSaveException e)
            {
                _notifications.Display(NotificationUse.Error,e.Message,"An error occured while saving your game\n please try again.");
            }
        }
    }
}