namespace BookAPI.Models
{
    public class Book
    {
        private static int _nextId = 1;

        public int Id { get; private set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int PublishedYear { get; set; }

        public Book()
        {
            Id = _nextId++;
        }
    }
}
