namespace GBReaderSougnG.Domains
{
    public record Choice
    {
        public int PageNumber { get; }
        public string Text { get; }

        public Choice(int numberPage, string textChoice)
        {
            PageNumber = numberPage;
            Text = textChoice;
        }
    }
}