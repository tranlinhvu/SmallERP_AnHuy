using System;
using System.Data;
using System.Data.SqlClient;
using IMS.DBHelper;

namespace IMS.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Key { get; set; }
        public int IdGroup { get; set; }
        public int IdStaff { get; set; }

        public User()
        {
            ;
        }

    }
}