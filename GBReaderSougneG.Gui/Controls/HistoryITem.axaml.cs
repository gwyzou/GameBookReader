using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Avalonia.Controls
{
    public partial class HistoryITem : UserControl
    {
        public HistoryITem()
        {
            InitializeComponent();
        }

        public void SetHistory(BookHistory bookHistory)
        {
            HistoryList.Children.Clear();
            Isbn.Text = bookHistory.GetIsbn();
            foreach (var node in bookHistory.GetHistory())
            {
                var selection = new HistoryNodeItem();
                selection.SetNode(node);
                HistoryList.Children.Add(selection);
            }
        }

    }
}