using System.ComponentModel;
using GBReaderSougnG.Domains;

namespace GBReaderSougneG.Tests.Domains;

public class IsbnTest
{
    [Test]
    public void ConstructorOnNull() =>
        //Expect
        Assert.Throws<ArgumentNullException>(
            () => new Isbn(null),"Argument null on  ISbn Creation");

    [Test]
    public void ConstructorOnWrongPattern()
    {
        //Expect
        Assert.Throws<InvalidEnumArgumentException>(
            () => new Isbn("null"),"ISBN doesn't math pattern");
        
        Assert.Throws<InvalidEnumArgumentException>(
            () => new Isbn("0-000000-0"),"ISBN doesn't math pattern");
        
        Assert.DoesNotThrow(() => new Isbn("0-000000-00-0"));
    }

    [Test]
    public void toString()
    {
        //Given
        var isbn = new Isbn("2-000000-09-3");
        var isbn2 = new Isbn("2-000000-09-X");
        
        //Expect
        Assert.That(isbn.ToString(),Is.EqualTo("2-000000-09-3"));
        Assert.That(isbn2.ToString(), Is.EqualTo("2-000000-09-X"));
    }
}