using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ISummaryPublisher: IDisplayer
    {
        string PublisherName { get; set; }
        int NumberOfBooks { get; set; }
        decimal TotalPrice { get; set; }
    }
}
