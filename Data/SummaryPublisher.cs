using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SummaryPublisher : ISummaryPublisher
    {
        public SummaryPublisher(string publisherName, int numberOfBooks, decimal totalPrice)
        {
            PublisherName = publisherName;
            NumberOfBooks = numberOfBooks;
            TotalPrice = totalPrice;
        }
        public string PublisherName { get; set; }
        public int NumberOfBooks { get; set; }
        public decimal TotalPrice { get; set; }

        public string Display()
        {
            return $"{PublisherName}: number of books {NumberOfBooks}, total price {TotalPrice}";
        }
    }
}
