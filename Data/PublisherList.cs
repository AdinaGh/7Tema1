using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class PublisherList: IDisplayer
    {
        public PublisherList()
        {
            List = new List<IPublisher>();
        }
        public List<IPublisher> List { get; set; }
        public string Display()
        {
            var sb = new StringBuilder();
            foreach (var publisher in List)
            {
                sb.AppendLine(publisher.Display());
            }

            return sb.ToString();
        }

        public void Add(int publisherId, string name)
        {
            List.Add(new Publisher(publisherId, name));
        }
    }

}
