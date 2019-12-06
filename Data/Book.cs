using Data.Interfaces;

namespace Data
{
    public class Book : IBook
    {
        public Book()
        {
        }

        public Book(int publisherId, string title, int year, decimal price, int? bookId = null)
        {
            if (bookId != null)
            {
                BookId = bookId.Value;
            }
            PublisherId = publisherId;
            Title = title;
            Year = year;
            Price = price;
        }
        public int BookId { get; set; }
        public int PublisherId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }

        public string Display()
        {
            return $"{Title} - {Year} - {Price}";
        }
    }
}
