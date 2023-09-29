using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Tests.Domains;

public class BookTest
{
    [Test]
    public void ConstructorOnNull()
    {
        //Expect
        Assert.Throws<ArgumentException>(
            () => new CoverBook(null,"notNull",new Isbn("1-234567-89-0"),new Author("Jhon","Doe")),"Title and Resume cannot be empty or null");
        Assert.Throws<ArgumentException>(
            () => new CoverBook("","notNull",new Isbn("1-234567-89-0"),new Author("Jhon","Doe")),"Title and Resume cannot be empty or null");
        Assert.Throws<ArgumentException>(
            () => new CoverBook("null",null,new Isbn("1-234567-89-0"),new Author("Jhon","Doe")),"Title and Resume cannot be empty or null");
        Assert.Throws<ArgumentException>(
            () => new CoverBook(null,"",new Isbn("1-234567-89-0"),new Author("Jhon","Doe")),"Title and Resume cannot be empty or null");
    }
    
    [Test]
    public void ConstructOnMaxSize()
    {
        //Given
        string size151 =0.ToString("D151");
        string size501 =0.ToString("D501");
        
        //Expect
        Assert.Throws<ArgumentException>(
            () => new CoverBook(size151,"notNull",new Isbn("1-234567-89-0"),new Author("Jhon","Doe")),"Wrong Input: String Size exceeded MaxLength allowed :150");
        Assert.Throws<ArgumentException>(
            () => new CoverBook("null",size501,new Isbn("1-234567-89-0"),new Author("Jhon","Doe")),"Wrong Input: String Size exceeded MaxLength allowed :500");
    }
}