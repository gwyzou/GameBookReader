using GBReaderSougneG.Presentation.IViews;
using GBReaderSougneG.Presentation.Notification;
using GBReaderSougneG.Presentation.Switch;
using GBReaderSougneG.Repositories;
using GBReaderSougneG.Repositories.Exceptions.History;
using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Presentation
{
    public class EndViewPresenter :BasicPresenter
    {
        private readonly IEndView _view;
        private readonly IGoToViews _router;
        private readonly INotifications _notifications; 
        private readonly IGeneralRepo _repository; 
        
        public EndViewPresenter(IEndView endView, IGoToViews router, INotifications notifications, IGeneralRepo repo,ICommunicationPresenter communicationPresenter)
        {
            _view = CheckAndThrow(endView, nameof(endView));
            _router = CheckAndThrow(router, nameof(router));
            _notifications = CheckAndThrow(notifications, nameof(notifications));
            _repository = CheckAndThrow(repo, nameof(repo));
            
            CheckAndThrow(communicationPresenter, nameof(communicationPresenter)).BookFinished += this.OnEnter;

            

            SubscribeEvent();
        }
        

        private void SubscribeEvent() => _view.GoMainPage += this.GoMainPage;

        private void GoMainPage(object? obj,EventArgs args) => _router.Goto(SwitchNames.StartPage);

        private void OnEnter(object? obj,Page args)
        {
            if (_repository.BookStorage.Book != null)
            {
                EndOfGame(_repository.BookStorage.Book, args);
            }
            else
            {
                _router.Goto(SwitchNames.StartPage);
                _notifications.Display(NotificationUse.Error,"Error while Loading","An error occured while Loading Page, Please try again");

            }
            
        }

        private void EndOfGame(FullBook book, Page args)
        {
            _view.SetBookInfo(book.Cover);
            try
            {
                _view.SetPage(args.Text);
                RemoveHistory();
            }
            catch (CannotSaveException e)
            {
                _router.Goto(SwitchNames.StartPage);
                _notifications.Display(NotificationUse.Error, "Error while Loading", e.Message);
            }
        }

        private void RemoveHistory()
        {
            _repository.History.GameHistory.FinishGameHistory();
            UploadHistory();
        }
        private void UploadHistory()
        {
            try
            {
                _repository.History.SaveData();
            }
            catch (CannotSaveException e)
            {
                _notifications.Display(NotificationUse.Error,"Error while Resetting",e.Message);
            }
        }
    }
}