namespace GBReaderSougnG.Domains
{
    public record FullBook
    {
        public  CoverBook Cover { get; }
        
        private readonly Dictionary<int, Page> _pageMap=new();

        public FullBook(CoverBook book,IDictionary<int, Page> pageList)
        {
            Cover = book ?? throw new ArgumentNullException(nameof(book));
            if (pageList == null)
            {
                throw new ArgumentNullException(nameof(pageList));
            }

            foreach (var page in pageList)
            {
                _pageMap.Add(page.Key,page.Value);
            }
        }
        public Page GetPage(int pageNumber) => _pageMap[pageNumber];

       
        

    }
}