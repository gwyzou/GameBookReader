using GBReaderSougneG.Repositories.Exceptions.History;
using GBReaderSougneG.Repository.HistoryStorage.Json;
using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Tests.Infrastructures.HistoryStorage
{
    public class JsonHistoryRepoTest
    {
        private readonly string _path = Path.Combine(Path.GetTempPath(),
            "ue36", "i210310-test.json");

        private JsonHistoryRepo repo;
        private readonly DateTime _date1 = DateTime.Now;
        private readonly DateTime _date2 = DateTime.Now.AddSeconds(5);
        private readonly DateTime _date3 = new (0001, 01, 01, 00, 00, 00, 00);

        [SetUp]
        public void Setup() => repo = new JsonHistoryRepo(_path);

        [Test]
        public void InitPathCreatePathIfNotExist()
        {
            //GIVEN
            TearDown();
            Assert.That(File.Exists(_path), Is.False);
            Assert.That(Directory.Exists(Path.GetDirectoryName(_path)), Is.False);
            Setup();
            
            //EXPECT
            Assert.That(Directory.Exists(Path.GetDirectoryName(_path)), Is.True);
            Assert.That(File.Exists(_path), Is.True);
        }
        [Test]
        public void InitPathCompletePathIfNotComplete()
        {
            //GIVEN
            Assert.That(File.Exists(_path), Is.True);
            Assert.That(Directory.Exists(Path.GetDirectoryName(_path)), Is.True);
            Assert.DoesNotThrow(()=>File.Delete(_path));
            Assert.That(File.Exists(_path), Is.False);
            Setup();
            
            //EXPECT
            Assert.That(Directory.Exists(Path.GetDirectoryName(_path)), Is.True);
            Assert.That(File.Exists(_path), Is.True);
            
        }

        [Test]
        public void FileDoesNotExistAfterSetupLoadHistory()
        {
            //Given
            Assert.DoesNotThrow(()=>File.Delete(_path));

            //Expect
            Assert.Throws<CannotReadException>(() => repo.LoadHistory("11"));
        }
        [Test]
        public void FileDoesNotExistAfterSetupLoadAllHistory()
        {
            //Given
            Assert.DoesNotThrow(()=>File.Delete(_path));

            //Expect
            Assert.Throws<CannotReadException>(() => repo.LoadAllHistoriesBook());
        }
        [Test]
        public void InitPathOnWrongPath()
        {
            //Expect
            Assert.Throws<PathCreationException>(() => new JsonHistoryRepo("\fze.;efzfa;zem;invalid\\path\\data.json"));
            Assert.Throws<PathCreationException>(() => new JsonHistoryRepo(" "));
        }

        [Test]
        public void LoadHistory()
        {
            //Given
            Assert.That(repo.GameHistory,Is.Null);
            repo.LoadHistory("11");
            
            //Then
            string json =Read();

            //Expect
            Assert.That(repo.GameHistory,Is.Not.Null);
            Assert.That(repo.GameHistory?.GetHistory().Count,Is.EqualTo(1));
            Assert.That(repo.GameHistory?.GetLastPage(),Is.EqualTo(1));
            Assert.That(json,Is.Not.Empty);
        }

        [Test]
        public void AddHistory()
        {
            //Given
            Assert.That(repo.GameHistory,Is.Null);
            repo.LoadHistory("11");
            repo.GameHistory?.AddHistory(new BookHistoryNode(DateTime.Now, 2));

            //Expect
            Assert.That(repo.GameHistory,Is.Not.Null);
            Assert.That(repo.GameHistory?.GetHistory().Count,Is.EqualTo(2));
            Assert.That(repo.GameHistory?.GetLastPage(),Is.EqualTo(2));
            
        }

        [Test]
        public void AutoSaveOnFirstLoadHistory()
        {
            //Given
            repo.LoadHistory("11");
            string json = Read();
            repo.SaveData();
            string json2 = Read();
            
            //Expect
            Assert.That(json,Is.EqualTo(json2));
        }
        [Test]
        public void MultipleSameLoadHistoryDoesNotModifyJson()
        {
            //Given
            repo.LoadHistory("11");
            string json = Read();
            repo.LoadHistory("11");
            string json2 = Read();
            
            //Expect
            Assert.That(json,Is.EqualTo(json2));
        }

        [Test]
        public void SaveDataChangeJson()
        {
            //Given
            repo.LoadHistory("11");
            repo.GameHistory?.AddHistory(new BookHistoryNode(DateTime.Now, 2));
            
            //Then
            string json = Read();
            repo.SaveData();
            string json2 = Read();
            
            //Expect
            Assert.That(json,Is.Not.EqualTo(json2));
        }

        [Test]
        public void SaveDataFormat()
        {
            //Given
            repo.LoadHistory("11");
            DateTime first = repo.GameHistory.GetHistory().First().Date;
            repo.GameHistory.AddHistory(new BookHistoryNode(_date2,2));
            repo.GameHistory.AddHistory(new BookHistoryNode(_date3,3));

            repo.SaveData();
            string json = Read();
            string expected = "[{\"Isbn\":\"11\",\"HistoryList\":[" +
                              $"{{\"Date\":\"{first:yyyy-MM-ddTHH:mm:ss.FFFFFFFK}\",\"PageNumber\":1}}," +
                              $"{{\"Date\":\"{_date2:yyyy-MM-ddTHH:mm:ss.FFFFFFFK}\",\"PageNumber\":2}}," +
                              $"{{\"Date\":\"{_date3:yyyy-MM-ddTHH:mm:ss.FFFFFFFK}\",\"PageNumber\":3}}" +
                              "]}]"
                ;
            //Expect
            Assert.That(json,Is.EqualTo(expected));
            
        }
        
        [Test]
        public void LoadHistoryOnNotFormattedJson()
        {
            //Given
            Write("akzleakjeazlnd zefze");

            //Expect
            Exception? ex = Assert.Throws<CannotReadException>(() => repo.LoadHistory("frzef"));
            Assert.That(ex?.InnerException , Is.Not.Null);
            Assert.That(ex?.InnerException?.Message , Is.EqualTo("'a' is an invalid start of a value. Path: $ | LineNumber: 0 | BytePositionInLine: 0."));
        }

        [Test]
        public void LoadWithMissingElementNodeNumberInJson()
        {
            string json = "[{\"Isbn\":\"11\",\"HistoryList\":[" +
                          $"{{\"Date\":\"{_date1:yyyy-MM-ddTHH:mm:ss.FFFFFFFK}\",\"PageNumber\":1}}," +
                          $"{{\"Date\":\"{_date2:yyyy-MM-ddTHH:mm:ss.FFFFFFFK}\",\"PageNumber\":2}}," +
                          $"{{\"Date\":\"{_date3:yyyy-MM-ddTHH:mm:ss.FFFFFFFK}\"}}" +
                          "]}]";
            Write(json);

            Assert.Throws<CannotReadException>(() => repo.LoadHistory("1"));
        }
        [Test]
        public void LoadWithMissingElementNodeDateInJson()
        {
            string json = "[{\"Isbn\":\"11\",\"HistoryList\":[" +
                          $"{{\"Date\":\"{_date1:yyyy-MM-ddTHH:mm:ss.FFFFFFFK}\",\"PageNumber\":1}}," +
                          $"{{\"Date\":\"{_date2:yyyy-MM-ddTHH:mm:ss.FFFFFFFK}\",\"PageNumber\":2}}," +
                          "{\"PageNumber\":3}" +
                          "]}]";
            
            Write(json);

            Assert.Throws<CannotReadException>(() => repo.LoadHistory("1"));
        }
        [Test]
        public void LoadWithMissingElementIsbnInJson()
        {
            string json = "[{\"Isbn\":\"\",\"HistoryList\":[" +
                          $"{{\"Date\":\"{_date1:yyyy-MM-ddTHH:mm:ss.FFFFFFFK}\",\"PageNumber\":1}}," +
                          $"{{\"Date\":\"{_date2:yyyy-MM-ddTHH:mm:ss.FFFFFFFK}\",\"PageNumber\":2}}," +
                          "{\"PageNumber\":3}" +
                          "]}]";
            Write(json);

            Assert.Throws<CannotReadException>(() => repo.LoadHistory("1"));
        }

        

        [Test]
        public void LoadWithBadFormatInJson()
        {
            string json = "[{\"Isbn\":\"11\",\"HistoryList\":[" +
                          $"{{\"Date\":\"{_date1:yyyy-MM-ddTHH:mm:ss.FFFFFFFK}\",\"PageNumber\":1}}," +
                          $"{{\"Date\":\"{_date2:yyyy-MM-ddTHH:mm:ss.FFFFFFFK}\",\"PageNumber\":2}}" +
                          $"{{\"Date\":\"{_date3:yyyy-MM-ddTHH:mm:ss.FFFFFFFK}\",\"PageNumber\":3}}" +
                          "]}]";
            Write(json);
            Assert.Throws<CannotReadException>(() => repo.LoadHistory("1"));
            
        }
        [Test]
        public void LoadHistoryOnOccupiedJson()
        {
            using (FileStream stream = new (_path, FileMode.Create))
            {
                using (StreamWriter writer = new (stream))
                {
                    //Given
                    writer.Write("akzleakjeazlnd zefze");
                    
                    //Expect
                    //Cannot read because file still opened in writer
                    Exception? ex = Assert.Throws<CannotReadException>(() => repo.LoadHistory("frzef"));
                    Assert.That(ex?.InnerException , Is.Not.Null);
                    Assert.That(ex?.InnerException?.Message , Is.EqualTo("The process cannot access the file '"+_path+"' because it is being used by another process."));
                }
            }
        }

        [Test]
        public void LoadAllHistoriesEmpty() =>
            //Expect
            Assert.That(repo.LoadAllHistoriesBook().Count(),Is.EqualTo(0));

        [Test]
        public void AddElementWithoutSave()
        {
            //Given
            repo.LoadHistory("11");
            string json1 = Read();
            repo.GameHistory?.AddHistory(new BookHistoryNode(DateTime.Now, 2));
            string json2 = Read();
            
            //Expect
            Assert.That(json1,Is.EqualTo(json2));
        }
        
        [Test]
        public void LoadAllHistoriesOneElement()
        {
            //je créer ici des nouveaux repo pour qu'on puisse lire les données json
            //plutot que regarder ce qui est en mémoire de repo
            
            
            //Given
            JsonHistoryRepo repo1 = new (_path); 
            repo.LoadHistory("11");
            //Expect
            Assert.That(repo1.LoadAllHistoriesBook().Count(),Is.EqualTo(1));
            Assert.That(repo1.LoadAllHistoriesBook().First().GetHistory().Count,Is.EqualTo(1));
            
            
            //Given
            JsonHistoryRepo repo2 = new (_path);
            repo.GameHistory?.AddHistory(new BookHistoryNode(DateTime.Now, 99));
            repo.SaveData();
            //Expect
            Assert.That(repo2.LoadAllHistoriesBook().First().GetHistory().Count,Is.EqualTo(2));
            Assert.That(repo2.LoadAllHistoriesBook().First().GetHistory()[1].PageNumber,Is.EqualTo(99));

        }

        [TearDown]
        public void TearDown()
        {
            string? path = Path.GetDirectoryName(_path);
            if (path!=null && Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            
        }

        private string Read()
        {
            using (FileStream stream = new (_path, FileMode.Open))
            {
                using (StreamReader reader = new (stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        private void Write(string json)
        {
            using (FileStream stream = new(_path, FileMode.Create))
            {
                using (StreamWriter writer = new(stream))
                {
                    writer.Write(json);
                }
            }
        }
        
    }
}