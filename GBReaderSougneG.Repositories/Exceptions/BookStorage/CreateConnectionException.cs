namespace GBReaderSougneG.Repositories.Exceptions.BookStorage
{
    public class CreateConnectionException:Exception
    {
        public CreateConnectionException(string str) : base(str)
        {
            
        }
        public CreateConnectionException(string str,Exception e) : base(str,e)
        {
            
        }
    }
}