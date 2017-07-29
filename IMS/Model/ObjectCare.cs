using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Model
{
    class ObjectCare
    {
        public int Id { get; set; }       
        public string Name { get; set; }
        public string Note { get; set; }
        public string CreatedDate { get; set; }
        public int IdStaff { get; set; }
        public int IdCustomer { get; set; }
        public bool IsDone { get; set; }
        public ObjectCare()
        {

        }
    }
}
