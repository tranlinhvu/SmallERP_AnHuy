using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Model
{
    class ServiceView
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int IdServiceGroup { get; set; }

        public ServiceView(int id, string name, int idServiceGroup)
        {
            Id = id;
            Name = name;
            IdServiceGroup = idServiceGroup;
        }

        public ServiceView()
        {

        }


    }
}
