using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace GBReaderSougneG.Avalonia.Controls
{
    public interface ISelectChoiceListener
    {
        void OnChoiceSelected(int pageNumber);
    }
    public partial class ChoiceItem : UserControl
    {
        private int _goTo;
        private ISelectChoiceListener _listener;
        public ChoiceItem()
        {
            InitializeComponent();
        }

        public void SetChoice(string text, int pageNumber,ISelectChoiceListener listener)
        {
            _goTo = pageNumber;
            _listener = listener;
            ChoiceText.Text = text;
        }

        private void ChoiceSelected_onClick(object? sender, PointerPressedEventArgs e) => _listener.OnChoiceSelected(_goTo);
    }
}