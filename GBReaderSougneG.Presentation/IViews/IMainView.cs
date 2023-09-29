using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Presentation.IViews
{
    public interface IMainView
    {
        void  SetAllBook(IReadOnlyList<CoverBook> bookList);
        void IsError();
        void IsNotError();
        event EventHandler<string> SearchBook; 
        event EventHandler ReloadData;
        event EventHandler<CoverBook?> NewGame;
        event EventHandler? SeeHistory;
        

    }
}