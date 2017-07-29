using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Model
{
    class UserView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int UseGroupName { get; set; }
        public int StaffName { get; set; }

        public UserView()
        {
            ;
        }
    }
}
