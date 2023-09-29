namespace GBReaderSougneG.Repository.BookStorage.DataBase
{
    public interface IStorageFactory
    {
        IStorage  NewStorageSession();
    }
}