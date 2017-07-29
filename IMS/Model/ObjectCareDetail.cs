using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Model
{
    class ObjectCareDetail
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CreatedDate { get; set; }
        public string CareDate { get; set; }
        public string NextCareDate { get; set; }
        public int IdStaff { get; set; }
        public int IdCarePerson { get; set; }
        public int IdAssistance { get; set; }
        public int IdObjectCare { get; set; }
        public bool IsDone { get; set; }
        public ObjectCareDetail()
        {

        }
    }
}
