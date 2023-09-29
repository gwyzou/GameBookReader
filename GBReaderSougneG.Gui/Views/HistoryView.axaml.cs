using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using GBReaderSougneG.Avalonia.Controls;
using GBReaderSougneG.Presentation.IViews;
using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Avalonia.Views
{
    public partial class HistoryView : UserControl, IHistoryView
    {
        public event EventHandler? GoMainPage;
        
        public HistoryView()
        {
            InitializeComponent();
        }

        private void OnGoBack_Click(object? sender, RoutedEventArgs e) => GoMainPage?.Invoke(this, EventArgs.Empty);

        public void SetHistories(IEnumerable<BookHistory> histories)
        {
            HistoryList.Children.Clear();
            foreach (var history in histories)
            {
                var selection = new HistoryITem();
                selection.SetHistory(history);
                HistoryList.Children.Add(selection);
            }
        }
    }
}