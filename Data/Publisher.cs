using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Publisher : IPublisher
    {
        public Publisher(int publisherId, string name)
        {
            PublisherId = publisherId;
            Name = name;
        }

        public int PublisherId { get; set; }
        public string Name { get; set; }
        public string Display()
        {
            return $"{PublisherId} - {Name}";
        }
    }
}