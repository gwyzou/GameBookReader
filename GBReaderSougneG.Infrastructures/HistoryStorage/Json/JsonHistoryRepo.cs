using System.Text.Json;
using GBReaderSougneG.Repositories;
using GBReaderSougneG.Repositories.Exceptions.History;
using GBReaderSougneG.Repository.HistoryStorage.Json.Dto;
using GBReaderSougneG.Repository.HistoryStorage.Json.Mapping;
using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Repository.HistoryStorage.Json
{
    public class JsonHistoryRepo: IHistoryStorage
    {
        private readonly string _path;
        private List<BookHistory>? _histories;
        public BookHistory? GameHistory { get; private set; }
        public JsonHistoryRepo(string path)
        {
            _path = path;

            InitPath();
            
        }

        private void InitPath()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_path) ?? string.Empty);
                if (!File.Exists(Path.GetFullPath(_path)))
                {
                    using (File.Create(Path.GetFullPath(_path))) { }
                }
            }
            catch (Exception e)
            {
                throw new PathCreationException("Couldn't create file path",e);
            }
            
        }

        private void LoadAllHistory()
        {
            try
            {
                _histories ??= Deserialize(ReadJson());
            }
            catch (Exception e)
            {
                throw new CannotReadException("Couldn't read History data", e);
            }
        }

        private string ReadJson()
        {
            using (FileStream stream = new (_path, FileMode.Open))
            {
                using (StreamReader reader = new (stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }


        private List<BookHistory> Deserialize(string readJson)
        {
            List<BookHistory> objectOut = new();
            if (readJson.Length != 0)
            {
                var output = JsonSerializer.Deserialize<List<DtoPlayHistory>>(readJson);
                if (output != null)
                {
                    objectOut.AddRange(OutputToModels(output));
                }
            }

            RemoveEmpty(objectOut);
            return objectOut;
        }

        private static void RemoveEmpty(List<BookHistory> objectOut) => objectOut.RemoveAll(x => !x.GetHistory().Any());

        private static IEnumerable<BookHistory> OutputToModels(IEnumerable<DtoPlayHistory> output) => output.Select(MappingPlayHistory.DtoToModel);

        private string Serialize()
        {
            List<DtoPlayHistory> dto = new();
            if (_histories != null)
            {
                RemoveEmpty(_histories);
                dto.AddRange(HistoriesToDto(_histories));
            }
            return JsonSerializer.Serialize(dto);
        }

        private static IEnumerable<DtoPlayHistory> HistoriesToDto(IEnumerable<BookHistory> list) => list.Select(MappingPlayHistory.ModelToDto);

        public void LoadHistory(string isbn)
        {
            LoadAllHistory();
            GameHistory =_histories?.FirstOrDefault(i => i.GetIsbn().Equals(isbn)) ?? NewHistory(isbn);
        }

        private BookHistory NewHistory(string isbn)
        {
            var toAdd = new BookHistory(isbn);
            toAdd.AddHistory(new BookHistoryNode(DateTime.Now, 1));
            _histories?.Add(toAdd);
            SaveData();
            return toAdd;
        }
        public IEnumerable<BookHistory> LoadAllHistoriesBook()
        {
            LoadAllHistory();
            return _histories;
        } 
        public void SaveData()
        {
            try
            {
                using (FileStream stream = new (_path, FileMode.Create))
                {
                    using (StreamWriter writer = new (stream))
                    {
                        writer.Write(Serialize());
                    }
                }
            }
            catch (Exception e)
            {
                throw new CannotSaveException("Couldn't save History data", e);
            }
        } 
    }
}