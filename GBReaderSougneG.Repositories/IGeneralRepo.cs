namespace GBReaderSougneG.Repositories
{
    public interface IGeneralRepo
    {
        public IBookStorage BookStorage { get; }
        public IHistoryStorage History { get; }
    }
}