using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Tests.Domains;

public class AuthorTest
{
    [Test]
    public void ConstructorOnNull()
    {
        //Expect
        Assert.Throws<ArgumentException>(
            () => new Author(null,""),"Name and Surname cannot be empty");
        Assert.Throws<ArgumentException>(
            () => new Author("null",null),"Name and Surname cannot be empty");
        
    }
    [Test]
    public void ConstructorOnEmpty()
    {
        //Expect
        Assert.Throws<ArgumentException>(
            () => new Author("","tt"),"Name and Surname cannot be empty");
        Assert.Throws<ArgumentException>(
            () => new Author("tt",""),"Name and Surname cannot be empty");
        
    }

    [Test]
    public void CallToString()
    {
        //Given
        var test = new Author("Bob", "Boby");
        //Expect
        Assert.That(test.ToString(),Is.EqualTo("bob boby"));
    }

    [Test]
    public void Getters()
    {
        //Given
        var test = new Author("Bob", "Boby");
        //Expect
        Assert.That(test.Name,Is.EqualTo("bob"));
        Assert.That(test.Surname,Is.EqualTo("boby"));

    }
}