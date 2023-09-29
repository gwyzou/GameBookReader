using Avalonia.Controls;
using Avalonia.Input;
using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Avalonia.Controls
{
    public interface IChooseBookListener
    {
        void OnBookSelected(CoverBook coverBook);
    }
    public partial class BookItem : UserControl
    {
        private CoverBook _coverBook;
        private IChooseBookListener _listener;

        public BookItem()
        {
            InitializeComponent();
        }
        public void SetBook(CoverBook coverBook)
        {
            _coverBook = coverBook;
            SetBookValues(coverBook);
            
        }

        private void SetBookValues(CoverBook coverBook)
        {
            Title.Text = $"{coverBook.Title}";
            Author.Text = $"{coverBook.Author}";
            Isbn.Text = $"{coverBook.Isbn}";

        }
        public void SetListener(IChooseBookListener listener)
        {
            _listener = listener;
        }

        private void ViewInfo_onClick(object sender, PointerPressedEventArgs args)
        {
            _listener.OnBookSelected(this._coverBook);
        }

       
    }
}