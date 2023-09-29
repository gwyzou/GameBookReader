namespace GBReaderSougneG.Repositories.Exceptions.History
{
    public class CannotReadException :Exception
    {
        public CannotReadException(string str, Exception e) : base(str, e)
        {
            
        }
    }
}