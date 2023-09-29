using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Presentation
{
    public interface ICommunicationPresenter
    {
        public event EventHandler<CoverBook>? BookChanged;
        public event EventHandler<Page>? BookFinished;
        public event EventHandler? GoToHistory;
        public void GoHistory();
        
        void NewGame(FullBook? fullBook);
       
        void EndGame(Page page);
    }
}