using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Presentation.IViews
{
    public interface IGameView
    {
        event EventHandler GoMainPage;
        event EventHandler<int> GoToPage;

        void SetBookInfo(CoverBook cover);
        void SetPage(Page page);
        
    }
}