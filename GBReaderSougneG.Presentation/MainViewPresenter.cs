using GBReaderSougneG.Presentation.IViews;
using GBReaderSougneG.Presentation.Notification;
using GBReaderSougneG.Presentation.Switch;
using GBReaderSougneG.Repositories;
using GBReaderSougneG.Repositories.Exceptions.BookStorage;
using GBReaderSougneG.Repositories.Exceptions.History;
using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Presentation
{
    public class MainViewPresenter : BasicPresenter
    {
        private readonly IMainView _view;
        private readonly IGoToViews _router;
        private readonly INotifications _notifications;
        private readonly IGeneralRepo _repository;
        private readonly ICommunicationPresenter _communicationPresenter;
        public MainViewPresenter(IMainView mainView,IGoToViews router, INotifications notifications,IGeneralRepo repo,ICommunicationPresenter communicationPresenter)
        {
            _view = CheckAndThrow(mainView, nameof(mainView));
            _router = CheckAndThrow(router, nameof(router));
            _notifications = CheckAndThrow(notifications, nameof(notifications));
            _repository = CheckAndThrow(repo, nameof(repo));
            _communicationPresenter = CheckAndThrow(communicationPresenter, nameof(communicationPresenter));

            SubscribeEvent();
        }
      
        private void SubscribeEvent()
        {
            _view.SearchBook += this.SearchBook;
            _view.ReloadData += this.StartApplication;
            _view.NewGame += this.StartNewGame;
            _view.SeeHistory += this.SeeHistory;
        }
        #region Launch Application

        /**
         * this method is only called once by the app to start at wanted page with wanted data or to reload Book list
         */
        private void StartApplication(object? obj, EventArgs arg) => FirstGetBooks();


        private void FirstGetBooks()
        {
            try
            {
                var listCover = _repository.BookStorage.GetList().ToList().AsReadOnly();
                if (listCover.Count == 0)
                {
                    EmptyStorage();
                }
                else
                {
                    FilledStorage();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler(e);
            }
            
        }

        private void FilledStorage()
        {
            _view.IsNotError();
            _view.SetAllBook(_repository.BookStorage.GetList().ToList().AsReadOnly());
        }

        private void EmptyStorage()
        {
            _notifications.Display(NotificationUse.Error, "No book found", "There is no books in Data storage");
            _view.IsError();
        }

        #endregion
        #region Exception Management
        private void ExceptionHandler(Exception e)
        {
            Dictionary<Type, Action> exceptionHandlers = new ()
            {
                { typeof(CannotLoadCoversException), CannotLoadExceptionHandler },
                { typeof(CannotLoadFullBookException), CannotLoadBookExceptionHandler },
                { typeof(CannotCloseException), ConnectionExceptionHandler },
                { typeof(CreateConnectionException), ConnectionExceptionHandler },
                { typeof(CannotSaveException),CannotLoadHistoryExceptionHandler},
                {typeof(CannotReadException),CannotLoadHistoryExceptionHandler}
            };

            if (exceptionHandlers.ContainsKey(e.GetType()))
            {
                exceptionHandlers[e.GetType()]();
            }
            else
            {
                OtherExceptionHandler();
            }
        }
        private void CannotLoadExceptionHandler()
        {
            _notifications.Display(NotificationUse.Error, "Can't Load Data",
                "There was an error while loading list of book");
            _view.IsError();
        }
        private void CannotLoadHistoryExceptionHandler() =>
            _notifications.Display(NotificationUse.Error, "Can't Load Data",
                "There was an error while loading History");

        private void CannotLoadBookExceptionHandler() =>
            _notifications.Display(NotificationUse.Error, "Can't Load Data",
                "There was an error while loading data book");

        private void ConnectionExceptionHandler()
        {
            _notifications.Display(NotificationUse.Error, "Server problem",
                "an error occured in server connection");
            _view.IsError();
        }
        private void OtherExceptionHandler()
        {
            _notifications.Display(NotificationUse.Error, "Error",
                "an unknown error occured");
            _view.IsError();
        }
        #endregion
        
        private void SearchBook(object? sender, string e) => _view.SetAllBook(SearchInBook(e));


        private IReadOnlyList<CoverBook> SearchInBook(string search)
        {
            var toReturn = _repository.BookStorage.GetList().ToList()
                .Where(item =>$"{item.Isbn}".Contains(search) | item.Title.Contains(search))
                .ToList();
            return toReturn.Count != 0 ? toReturn : FullList(search);
        }

        private IReadOnlyList<CoverBook> FullList(string search)
        {
            _notifications.Display(NotificationUse.Warning, "Can't find Book",
                "There are no books corresponding to : " + search);
            return _repository.BookStorage.GetList().ToList();
        }

        private void StartNewGame(object? sender, CoverBook? e)
        {
            if (e != null)
            {
                try
                {
                    var book = LoadFullBook(e);
                    LoadHistory(book);
                    _router.Goto(SwitchNames.GameBook);
                    _communicationPresenter.NewGame(book);

                }
                catch (Exception ex)
                {
                    ExceptionHandler(ex);
                }
            }
            else
            {
                _notifications.Display(NotificationUse.Error,"Can't find Book","An Error occured with selection Book");
            }
        }

        private void LoadHistory(FullBook? book) => _repository.History.LoadHistory($"{book?.Cover.Isbn}");

        private FullBook? LoadFullBook(CoverBook e) => _repository.BookStorage.LoadFullBook(e);

        private void SeeHistory(object? obj, EventArgs arg)
        {
            _communicationPresenter.GoHistory();
            _router.Goto(SwitchNames.HistoryPage);
        }
    }
}