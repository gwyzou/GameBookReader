namespace GBReaderSougneG.Repositories.Exceptions.BookStorage
{
    public class CannotCloseException: Exception
    {
        public CannotCloseException(string str, Exception e) : base(str, e)
        {
            
        }
    }
}