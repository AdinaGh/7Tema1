using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IBook: IDisplayer
    {
        int BookId { get; set; }
        int PublisherId { get; set; }
        string Title { get; set; }
        int Year { get; set; }
        decimal Price { get; set; }
    }
}
