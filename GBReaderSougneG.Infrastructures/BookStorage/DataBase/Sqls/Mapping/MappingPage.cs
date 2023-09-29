using GBReaderSougneG.Repository.BookStorage.DataBase.Sqls.Dto;
using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Repository.BookStorage.DataBase.Sqls.Mapping
{
    public static class MappingPage
    {
        public static Page DtoToPage(PageDto dto,IEnumerable<Choice> choices) => new (dto.Text, choices.ToList());
    }
}