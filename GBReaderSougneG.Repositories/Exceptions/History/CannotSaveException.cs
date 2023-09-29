namespace GBReaderSougneG.Repositories.Exceptions.History
{
    public class CannotSaveException : Exception
    {
        public CannotSaveException(string str, Exception e) : base(str, e)
        {
            
        }
    }
}