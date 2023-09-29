using GBReaderSougneG.Repository.HistoryStorage.Json.Dto;
using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Repository.HistoryStorage.Json.Mapping
{
    public static class MappingHistoryNode
    {
        public static BookHistoryNode DtoToModel(DtoHistoryNode dto) => 
            new(dto.Date!=DateTime.MinValue? dto.Date:throw new ArgumentException(), dto.PageNumber!=0?dto.PageNumber : throw new ArgumentException());

        public static DtoHistoryNode ModelToDto(BookHistoryNode model) =>
            new()
            {
                Date = model.Date,
                PageNumber = model.PageNumber
            };
    }
}