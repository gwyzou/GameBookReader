using System.Data;
using System.Data.SqlClient;
using GBReaderSougneG.Repositories.Exceptions.BookStorage;
using GBReaderSougneG.Repository.BookStorage.DataBase.Sqls.Dto;
using GBReaderSougneG.Repository.BookStorage.DataBase.Sqls.Mapping;
using GBReaderSougnG.Domains;
using MySql.Data.MySqlClient;

namespace GBReaderSougneG.Repository.BookStorage.DataBase.Sqls
{
    /// <summary>
    /// AutoClosable class
    /// must be used in a "using" section
    /// or explicitly call Dispose(); at the end of utilisation
    /// </summary>
    public class SqlStorage : IStorage
    {
        private readonly IDbConnection _connection;

        public SqlStorage(IDbConnection  co)
        {
            _connection = co ?? throw new ArgumentNullException(nameof(co));
        }


        public IReadOnlyDictionary<CoverBook,int> LoadCovers()
        {
            Dictionary<CoverBook, int> dictionary = new();
            const string sql = "SELECT * FROM BOOK b JOIN AUTHOR a ON a.Id_Author=b.Id_Author WHERE isPublished =1";
            using (var command = _connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                //command.Parameters.AddWithValue("@id", 1);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("Id_Book"));
                        string title   = reader.GetString(reader.GetOrdinal("title"));                  
                        string resume  = reader.GetString(reader.GetOrdinal("resume"));
                        string isbn    = reader.GetString(reader.GetOrdinal("isbn"));
                        string name    = reader.GetString(reader.GetOrdinal("name"));
                        string surname = reader.GetString(reader.GetOrdinal("surname"));
                        dictionary.Add(MappingCover.GetCover(title,resume,isbn,name,surname),id);
                    }
                }
            }

            return dictionary;
        }

        public FullBook GetFullBook(int id,CoverBook cover)
        {
            
            Dictionary<int, Page> pages=new();
            if (id == -1)
            {
                throw new CannotLoadFullBookException("Selected Book couldn't be found in Data");
            }

            var pagesDto = PageDtoList(id);
            PageConversion(pagesDto, pages);

            return new FullBook(cover, pages);

        }

        private void PageConversion(List<PageDto> pagesDto, Dictionary<int, Page> pages)
        {
            foreach (var pageDto in pagesDto)
            {
                pages.Add(pageDto.PageNbr, MappingPage.DtoToPage(pageDto, GetChoices(pageDto)));
            }
        }

        private List<PageDto> PageDtoList(int bookId)
        {
            const string sql = "SELECT Id_Page, pageContent, pageNumber FROM PAGE WHERE Id_Book=@id";
            List<PageDto> toReturn = new();
            using (var command = _connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Parameters.Add(new MySqlParameter("@id", bookId));
                command.Prepare();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        toReturn.Add(new PageDto()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id_Page")),
                            Text = reader.GetString(reader.GetOrdinal("pageContent")),
                            PageNbr = reader.GetInt32(reader.GetOrdinal("pageNumber"))
                        });
                    }
                }
            }
            return toReturn;
        }

        private List<Choice> GetChoices(PageDto page)
        {
            const string sql = "SELECT textChoice, P.pageNumber FROM CHOICE JOIN PAGE P on P.Id_Page = CHOICE.Id_Page_GoTo WHERE CHOICE.Id_Page=@id";
            List<Choice> toReturn = new();
            using (var command = _connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Parameters.Add(new MySqlParameter("@id", page.Id));
                command.Prepare();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        toReturn.Add( GetChoice(reader));
                    }
                }
            }
            return toReturn;
        }

        private static Choice GetChoice(IDataReader reader) =>
            MappingChoice.GetChoice(reader.GetInt32(reader.GetOrdinal("pageNumber")),
                reader.GetString(reader.GetOrdinal("textChoice")));

        public void Dispose()
        {
            try
            {
                _connection.Close();
            }
            catch (SqlException e)
            {
                throw new CannotCloseException("Error on Closing connection",e);
            }
        }
    }
}