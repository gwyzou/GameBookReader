namespace GBReaderSougneG.Repositories.Exceptions.BookStorage
{
    public class CannotLoadCoversException:Exception 
    {
        public CannotLoadCoversException(string str, Exception e) : base(str, e)
        {
            
        }
    }
}