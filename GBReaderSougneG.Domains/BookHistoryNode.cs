namespace GBReaderSougnG.Domains
{
    public record BookHistoryNode
    {
        public DateTime Date { get; }
        public int PageNumber { get; }

        public BookHistoryNode(DateTime date, int number)
        {
            Date = date;
            PageNumber = number;
        }
    }
}