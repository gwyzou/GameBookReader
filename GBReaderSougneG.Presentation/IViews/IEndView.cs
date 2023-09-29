using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Presentation.IViews
{
    public interface IEndView
    {
        public void SetBookInfo(CoverBook cover);
        public void SetPage(string text);
        public event EventHandler? GoMainPage;
    }
}