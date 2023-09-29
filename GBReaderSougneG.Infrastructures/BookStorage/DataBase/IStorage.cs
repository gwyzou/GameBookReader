using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Repository.BookStorage.DataBase
{
    public interface IStorage : IDisposable
    {
        IReadOnlyDictionary<CoverBook,int> LoadCovers();
        FullBook GetFullBook(int id , CoverBook coverBook);
    }
}