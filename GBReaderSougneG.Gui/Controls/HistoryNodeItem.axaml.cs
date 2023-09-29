using Avalonia.Controls;
using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Avalonia.Controls
{
    public partial class HistoryNodeItem : UserControl
    {
        public HistoryNodeItem()
        {
            InitializeComponent();
        }

        public void SetNode(BookHistoryNode node)
        {
            PageNumber.Text = $"Page : {node.PageNumber}";
            Time.Text = node.Date.ToString("dd/MM/yyyy");
            Date.Text = node.Date.ToString("HH:mm");
        }
    }
}