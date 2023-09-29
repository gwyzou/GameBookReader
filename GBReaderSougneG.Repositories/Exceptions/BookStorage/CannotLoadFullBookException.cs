namespace GBReaderSougneG.Repositories.Exceptions.BookStorage
{
    public class CannotLoadFullBookException : Exception
    {
        public CannotLoadFullBookException(string str, Exception e) : base(str, e)
        {
            
        }

        public CannotLoadFullBookException(string str) : base(str)
        {
            
        }
    }
}