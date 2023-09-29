using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Presentation.IViews
{
    public interface IHistoryView
    {
        event EventHandler GoMainPage;
        void SetHistories(IEnumerable<BookHistory> histories);
    }
}