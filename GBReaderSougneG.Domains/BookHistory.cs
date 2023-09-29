namespace GBReaderSougnG.Domains
{
    public record BookHistory
    {
        private readonly string _isbn;
        private readonly List<BookHistoryNode> _history = new();

        public BookHistory(string isbn)
        {
            _isbn = isbn;
        }
        public void AddHistory(BookHistoryNode bookHistoryNode) => _history.Add(bookHistoryNode);
        public int GetLastPage() =>_history[^1].PageNumber;
        public IReadOnlyList<BookHistoryNode> GetHistory() => _history;
        public void FinishGameHistory() => _history.Clear();

        public string GetIsbn() => _isbn;
    }
}