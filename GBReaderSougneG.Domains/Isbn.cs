using System.ComponentModel;
using System.Text.RegularExpressions;

namespace GBReaderSougnG.Domains;

public struct Isbn
{
    private int _matricule;
    private byte _bookNumber;

    private byte _language;

    private char _codeControl;

    public Isbn(string isbn) : this()
    {
        CheckNull(isbn);
        CheckPattern(isbn);
        InitValues(isbn);
    }

    private void CheckNull(string input)
    {
        if (input == null)
        {
            throw new ArgumentNullException();
        }
    }

    private void CheckPattern(string input)
    {
        if (!Regex.Match(input, "^[0-9]{1}-[0-9]{6}-[0-9]{2}-([0-9]|X){1}$").Success)
        {
            throw new InvalidEnumArgumentException("ISBN doesn't math pattern");
        }
    }

    private void InitValues(string isbn)
    {
        string[] numbers = new Regex("-").Split(isbn);
        _language = byte.Parse(numbers[0]);
        _matricule=int.Parse(numbers[1]);
        _bookNumber=byte.Parse(numbers[2]);
        _codeControl= numbers[3][0];
    }
    public override string ToString() => _language+"-"+_matricule.ToString("D6")+"-"+_bookNumber.ToString("D2")+"-"+_codeControl;
}