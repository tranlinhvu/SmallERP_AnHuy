using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Model
{
    class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public string Note { get; set; }
        public int IdServiceGroup { get; set; }

        public Service(int id, string name, long price, string note, int idServiceGroup)
        {
            Id = id;
            Name = name;
            Price = price;
            Note = note;
            IdServiceGroup = idServiceGroup;
        }

        public Service()
        {
            
        }
    }
}
