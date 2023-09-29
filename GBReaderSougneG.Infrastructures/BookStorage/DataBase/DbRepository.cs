using System.Data.SqlClient;
using GBReaderSougneG.Repositories;
using GBReaderSougneG.Repositories.Exceptions.BookStorage;
using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Repository.BookStorage.DataBase
{
    public class DbRepository:IBookStorage
    {
        private readonly IStorageFactory _factory;
        private readonly Dictionary<CoverBook, int> _dictionary = new();
        public FullBook? Book { get; private set; }

        public DbRepository(IStorageFactory factory)
        {
            _factory = factory;
        }

        public IEnumerable<CoverBook> GetList()
        {
            if (!_dictionary.Any())
            {
                foreach (var cover in LoadCovers())
                {
                    _dictionary.Add(cover.Key,cover.Value);
                }
            }
            return _dictionary.Keys;
        }

        private IReadOnlyDictionary<CoverBook,int> LoadCovers()
        {
            try
            {
                using (var connection = _factory.NewStorageSession())
                {
                    return connection.LoadCovers();
                }
            }
            catch (SqlException e)
            {
                throw new CannotLoadCoversException("Error while Retrieving Data From Covers", e);
            }
        }

        public FullBook? LoadFullBook(CoverBook coverBook)
        {
            int i;
            if (_dictionary.ContainsKey(coverBook))
            {
                i = _dictionary[coverBook];
            }
            else
            {
                i = -1;
            }
            try
            {
                using (var connection = _factory.NewStorageSession())
                {
                    Book = connection.GetFullBook(i,coverBook);
                    return Book;
                }
            }
            catch (SqlException e)
            {
                throw new CannotLoadFullBookException("An Error occured while retrieving data from selection",e);
            }
        }
    }
}