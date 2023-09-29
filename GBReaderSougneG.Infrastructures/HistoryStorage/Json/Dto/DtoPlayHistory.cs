namespace GBReaderSougneG.Repository.HistoryStorage.Json.Dto
{
    public class DtoPlayHistory
    {
        public string? Isbn { get; set; }
        public List<DtoHistoryNode>? HistoryList { get; set; }
    }
}