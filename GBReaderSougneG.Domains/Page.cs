namespace GBReaderSougnG.Domains
{
    public readonly struct Page
    {
        public string Text { get; }
        
        private readonly List<Choice> _choices = new();

        public Page(string text, List<Choice> choices)
        {
            Text = text ??throw new ArgumentNullException(nameof(text));
            _choices.AddRange(choices ?? throw new ArgumentNullException(nameof(choices)));
        }
        public IReadOnlyList<Choice> GetChoicesList() => _choices;
    }
}