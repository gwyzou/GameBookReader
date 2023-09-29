using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Repositories
{
    public interface IBookStorage
    {
        FullBook? Book { get; }
        public IEnumerable<CoverBook> GetList();
        public FullBook? LoadFullBook(CoverBook coverBook);
    }
}