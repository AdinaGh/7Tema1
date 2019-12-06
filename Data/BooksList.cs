using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BookList: IDisplayer
    {
        public BookList()
        {
            List = new List<IBook>();
        }
        public List<IBook> List { get; set; }
        public string Display()
        {
            var sb = new StringBuilder();
            foreach (var book in List)
            {
                sb.AppendLine(book.Display());
            }

            return sb.ToString();
        }

        public void Add(int publisherId, string title, int year, decimal price, int? bookId = null)
        {
            List.Add(new Book(publisherId, title, year, price, bookId));
        }
    }
}
