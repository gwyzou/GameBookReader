using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using GBReaderSougneG.Avalonia.Controls;
using GBReaderSougneG.Presentation.IViews;
using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Avalonia.Views
{
    public partial class GameView : UserControl,ISelectChoiceListener ,IGameView
    {
        public GameView()
        {
            InitializeComponent();
        }

        public event EventHandler? GoMainPage;
        public event EventHandler<int>? GoToPage;

        public void SetBookInfo(CoverBook cover)
        {
            Title.Text = cover.Title;
            Author.Text = $"{cover.Author}";
            Isbn.Text = $"{cover.Isbn}";
        }

        public void SetPage(Page page)
        {
            PageText.Text=page.Text;
            SetChoices(page.GetChoicesList());
        } 

        private void SetChoices(IReadOnlyList<Choice> choices)
        {
            ChoiceList.Children.Clear();
            foreach (var association in choices)
            {
                var choiceItem = new ChoiceItem();
                choiceItem.SetChoice(association.Text,association.PageNumber,this);
                ChoiceList.Children.Add(choiceItem);
            }
        }
        private void OnClick_Back(object? sender, RoutedEventArgs e) => GoMainPage?.Invoke(this,EventArgs.Empty);

        public void OnChoiceSelected(int pageNumber) => GoToPage?.Invoke(this,pageNumber);

      
    }
}