using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Model
{
    class ServiceDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public bool IsCheck { get; set; }
        public int IdService { get; set; }

        public ServiceDetail(int id, string name, string note, bool isCheck, int idService)
        {
            Id = id;
            Name = name;
            Note = note;
            IsCheck = IsCheck;
            IdService = idService;
        }

        public ServiceDetail()
        {

        }
    }
}
