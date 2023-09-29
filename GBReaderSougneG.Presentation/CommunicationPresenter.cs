using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Presentation
{
    public class CommunicationPresenter: ICommunicationPresenter 
    {
        public event EventHandler<CoverBook>? BookChanged;
        public event EventHandler<Page>? BookFinished;

        public event EventHandler? GoToHistory;

        public void GoHistory() => GoToHistory?.Invoke(this,EventArgs.Empty);

        public void EndGame(Page page) => BookFinished?.Invoke(this,page);
        
        public void NewGame(FullBook? fullBook)
        {
            if (fullBook == null)
            {
                return;
            }
            
            BookChanged?.Invoke(this,fullBook.Cover);
        }

        
        
    }
}