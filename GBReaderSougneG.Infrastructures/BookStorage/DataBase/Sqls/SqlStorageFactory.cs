using GBReaderSougneG.Repositories.Exceptions.BookStorage;
using MySql.Data.MySqlClient;

namespace GBReaderSougneG.Repository.BookStorage.DataBase.Sqls
{
    public class SqlStorageFactory : IStorageFactory
    {
        private readonly string _connectionString;
        private readonly MySqlClientFactory _factory;


        public SqlStorageFactory(string url,string port , string database, string username, string pass)
        {
            _factory = MySqlClientFactory.Instance;

            _connectionString = $"Server={url};port={port};Database={database};User Id={username};Password={pass};";
            
        }

        public IStorage NewStorageSession()
        {
            var connection = _factory.CreateConnection() ?? throw new CreateConnectionException("Error on creating Connection");
            try
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                return new SqlStorage(connection);
            }
            catch (MySqlException e)
            {
                throw new CreateConnectionException("Couldn't open Connection" ,e);
            }
        }
    }
}