namespace GBReaderSougnG.Domains;

public record CoverBook
{
    public string Title { get; }

    public string Resume { get; }

    public Isbn Isbn { get; }

    public Author Author { get; }

    public CoverBook(string title, string resume, Isbn isbn, Author author)
    {
        CheckSize(title,150);
        CheckSize(resume,500);
        
        Author = author;
        Isbn = isbn;
        Title = title;
        Resume = resume;
    }

    private void CheckEmpty(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Title and Resume cannot be empty or null");
        }
    }

    private void CheckSize(string input, int maxValue)
    {
        CheckEmpty(input);
        if (input.Length > maxValue)
        {
            throw new ArgumentException("Wrong Input: String Size exceeded MaxLength allowed :"+maxValue);
        }
    }
}