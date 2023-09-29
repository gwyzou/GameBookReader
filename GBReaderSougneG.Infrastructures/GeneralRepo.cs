using GBReaderSougneG.Repositories;

namespace GBReaderSougneG.Repository
{
    public class GeneralRepo:IGeneralRepo
    {
        public IBookStorage BookStorage { get; }
        public IHistoryStorage History { get; }
        public GeneralRepo(IBookStorage bookStorage, IHistoryStorage history)
        {
            BookStorage = bookStorage;
            History = history;
        }
    }
}