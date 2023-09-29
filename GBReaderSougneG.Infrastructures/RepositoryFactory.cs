using GBReaderSougneG.Repositories;
using GBReaderSougneG.Repository.BookStorage.DataBase;
using GBReaderSougneG.Repository.BookStorage.DataBase.Sqls;
using GBReaderSougneG.Repository.HistoryStorage.Json;

namespace GBReaderSougneG.Repository
{
    public class RepositoryFactory
    {
        public IGeneralRepo GetRepo(RepoType storage, RepoType history) => new GeneralRepo(GetStorage(storage),GetHistory(history));

        private IBookStorage GetStorage(RepoType storage)
        {
            switch (storage){
                case RepoType.Mysql:
                    return new DbRepository(new SqlStorageFactory("192.168.132.200","13306","I210310","I210310","0310"));
                        // BDRepository( new SqlStorageFactory(
                        //        "com.mysql.cj.jdbc.Driver",
                        //        "jdbc:mysql://192.168.128.13:3306/in21b10310",
                        //        "in21b10310",
                        //        "0310"
                      //  "com.mysql.cj.jdbc.Driver","jdbc:mysql://asterix-intra.cg.helmo.be:13306/I210310","I210310","0310"
                    //));
                    
                default:
                    throw new ArgumentException("specified book storage is unimplemented",nameof(storage));
            }
        }

        private IHistoryStorage GetHistory(RepoType history)
        {
            switch (history)
            {
                case RepoType.Json:
                    return new JsonHistoryRepo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),"ue36","i210310-session.json"));
                default:
                    throw new ArgumentException("specified history storage is unimplemented",nameof(history));
            }
        }
    }
    
}