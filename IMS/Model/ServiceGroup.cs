using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IMS.Model{

    class ServiceGroup
    {        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }

        public ServiceGroup(int id, string name, string note)
        {
            Id = id;
            Name = name;
            Note = note;
        }

        public ServiceGroup()
        {            
            Id = 1;
            Name = "";
            Note = "";
        }
    }
}
