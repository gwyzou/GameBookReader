using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Repository.BookStorage.DataBase.Sqls.Mapping
{
    public static class MappingCover
    {
        public static CoverBook GetCover(string title,string resume, string isbn,string name,string surname) => new CoverBook(title, resume, new Isbn(isbn), new Author(name, surname));
    }
}