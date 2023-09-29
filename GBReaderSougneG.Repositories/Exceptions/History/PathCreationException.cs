namespace GBReaderSougneG.Repositories.Exceptions.History
{
    public class PathCreationException:Exception
    {
        public PathCreationException(string str, Exception e) : base(str, e)
        {
            
        }
    }
}