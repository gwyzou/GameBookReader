using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Tests.Domains
{
    public class CoverBookTest
    {
        private readonly Isbn _isbn = new ("0-000000-00-0");
        private readonly Author _author = new ("a", "a");
        [Test]
        public void Constructor()
        {
            //Expect
            Assert.Throws<ArgumentException>(() => new CoverBook(string.Empty, "aa", _isbn, _author));
            Assert.Throws<ArgumentException>(() => new CoverBook("a",string.Empty, _isbn, _author));
            Assert.Throws<ArgumentException>(() => new CoverBook(null,"a", _isbn, _author));
            Assert.Throws<ArgumentException>(() => new CoverBook("null",null, _isbn, _author));

            Assert.Throws<ArgumentException>(() => new CoverBook(new string('c', 501),"null", _isbn, _author));
            Assert.Throws<ArgumentException>(() => new CoverBook("a",new string('c', 501), _isbn, _author));
            Assert.DoesNotThrow(()=> new CoverBook("a","a", _isbn, _author));
        }

        [Test]
        public void Getters()
        {
            //Given
            CoverBook coverBook = new ("a", "b", _isbn, _author);
            
            //Expect
            Assert.That(coverBook.Title,Is.EqualTo("a"));
            Assert.That(coverBook.Resume,Is.EqualTo("b"));
            Assert.That($"{coverBook.Isbn}",Is.EqualTo($"{_isbn}"));
            Assert.That($"{coverBook.Author}",Is.EqualTo($"{_author}"));
        }
    }
}