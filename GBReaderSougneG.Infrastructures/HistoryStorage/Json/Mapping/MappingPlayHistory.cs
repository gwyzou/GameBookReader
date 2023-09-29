using GBReaderSougneG.Repository.HistoryStorage.Json.Dto;
using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Repository.HistoryStorage.Json.Mapping
{
    public static class MappingPlayHistory
    {
        public static BookHistory DtoToModel(DtoPlayHistory dto)
        {
            var model = new BookHistory(!string.IsNullOrEmpty(dto.Isbn)? dto.Isbn:throw new ArgumentException(nameof(dto.Isbn)));
            if (dto.HistoryList != null)
            {
                foreach (var historyNodeDto in dto.HistoryList)
                {
                    model.AddHistory(MappingHistoryNode.DtoToModel(historyNodeDto));
                }
            }

            return model;
        }

        public static DtoPlayHistory ModelToDto(BookHistory model) =>
            new()
            {
                Isbn = model.GetIsbn(),
                HistoryList = GetHistoryList(model)
            };

        private static List<DtoHistoryNode> GetHistoryList(BookHistory model) => model.GetHistory().Select(MappingHistoryNode.ModelToDto).ToList();
    }
}