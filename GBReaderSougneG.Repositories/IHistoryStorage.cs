using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Repositories
{
    public interface IHistoryStorage
    {
        public BookHistory GameHistory { get; }
        public void LoadHistory(string isbn);
        IEnumerable<BookHistory> LoadAllHistoriesBook();
        public void SaveData();
    }
}