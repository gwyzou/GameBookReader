using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Repository.BookStorage.DataBase.Sqls.Mapping
{
    public static class MappingChoice
    {
        public static Choice GetChoice(int nbr, string content) => new (nbr, content);
    }
}