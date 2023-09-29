namespace GBReaderSougnG.Domains;

public readonly struct Author
{
    public string Name { get; }

    public string Surname { get; }

    public Author(string name,string surname):this()
    {
        Name =CheckEmptyString(name);
        Surname =CheckEmptyString(surname);
    }
    private string CheckEmptyString(string input)
    {
        
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Name and Surname cannot be empty");
        }

        return input.ToLower();
    }

    public override string ToString() => Name+" "+Surname;
}