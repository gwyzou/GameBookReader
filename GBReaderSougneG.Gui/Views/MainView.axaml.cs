using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using GBReaderSougneG.Avalonia.Controls;
using GBReaderSougneG.Presentation.IViews;
using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Avalonia.Views
{
    public partial class MainView : UserControl,IChooseBookListener,IMainView
    {

        //events
        public event EventHandler<string>? SearchBook;
        public event EventHandler? ReloadData;
        public event EventHandler<CoverBook?>? NewGame;
        public event EventHandler? SeeHistory;

        private CoverBook? _selectedCover;
       

        public MainView()
        {
            InitializeComponent();
        }
        public void IsError()
        {
            SearchButton.IsEnabled = false;
            SearchBar.IsEnabled = false;
            HistoryButton.IsEnabled = false;
            Start.IsEnabled = false;
            Reload.IsEnabled = true;
            Reload.IsVisible = true;
            
        }

        public void IsNotError()
        {
            SearchButton.IsEnabled = true;
            SearchBar.IsEnabled = true;
            HistoryButton.IsEnabled = true;
            Start.IsEnabled = true;
            Reload.IsEnabled = false;
            Reload.IsVisible = false;
            
        }

        private void AddAllBooksToView(IReadOnlyList<CoverBook> bookList)
        {
            BookList.Children.Clear();
            foreach (var cover in bookList)
            {
                var listBookView = new BookItem();
                listBookView.SetBook(cover);
                listBookView.SetListener(this);
                BookList.Children.Add(listBookView);
            }
        }

        private void EditView(IReadOnlyList<CoverBook> bookList)
        {
            AddAllBooksToView(bookList);
            if (bookList.Count != 0)
            {
                SetBookInfo(bookList[0]);
            }
        }

        public void OnBookSelected(CoverBook coverBook) => SetBookInfo(coverBook);

        private void SetBookInfo(CoverBook coverBook)
        {
            _selectedCover = coverBook;
            Title.Text = coverBook.Title;
            Isbn.Text = $"{coverBook.Isbn}";
            Author.Text = $"{coverBook.Author}";
            Resume.Text = coverBook.Resume;
        }

        private void OnSearch_Click(object? sender, RoutedEventArgs e) => SearchBook?.Invoke(this,SearchBar.Text);


        public void SetAllBook(IReadOnlyList<CoverBook> bookList) => EditView(bookList);

        private void OnReload_Click(object? sender, RoutedEventArgs e) => ReloadData?.Invoke(this,EventArgs.Empty);

        private void OnNewGame_Click(object? sender, RoutedEventArgs e) => NewGame?.Invoke(this,_selectedCover);

        private void OnHistory_Click(object? sender, RoutedEventArgs e) => SeeHistory?.Invoke(this,EventArgs.Empty);

        private void StyledElement_OnInitialized(object? sender, EventArgs e) => ReloadData?.Invoke(this,EventArgs.Empty);
    }
}