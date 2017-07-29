using System;
using System.Data;
using System.Data.SqlClient;
using IMS.DBHelper;

namespace IMS.Model
{
    public class Privilege
    {
        int id;
        string privilegeName;

        public Privilege()
        {
            ;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string PrivilegeName
        {
            get { return privilegeName; }
            set { privilegeName = value; }
        }        
    }
}