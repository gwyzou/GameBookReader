namespace GBReaderSougneG.Presentation
{
    public class BasicPresenter
    {
        public static T CheckAndThrow<T>(T argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }

            return argument;
        }
    }
}