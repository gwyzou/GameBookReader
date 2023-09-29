using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GBReaderSougneG.Presentation.IViews;
using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Avalonia.Views
{
    public partial class EndView : UserControl,IEndView
    {
        public EndView()
        {
            InitializeComponent();
        }
        public event EventHandler? GoMainPage;

        private void OnClick_GoBack(object? sender, RoutedEventArgs e) => GoMainPage?.Invoke(this,EventArgs.Empty);

        public void SetBookInfo(CoverBook cover)
        {
            Title.Text = cover.Title;
            Author.Text = $"{cover.Author}";
            Isbn.Text = $"{cover.Isbn}";
        }

        public void SetPage(string text) => PageText.Text=text;
    }
}