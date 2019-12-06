using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SummaryPublisherList
    {
        public SummaryPublisherList()
        {
            List = new List<ISummaryPublisher>();
        }
        public List<ISummaryPublisher> List { get; set; }
        public string Display()
        {
            var sb = new StringBuilder();
            foreach (var publisher in List)
            {
                sb.AppendLine(publisher.Display());
            }

            return sb.ToString();
        }

        public void Add(string name, int count, decimal price)
        {
            List.Add(new SummaryPublisher(name, count, price));
        }
    }
}
