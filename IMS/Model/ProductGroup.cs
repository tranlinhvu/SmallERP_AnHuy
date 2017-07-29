using System;
using System.Data;
using System.Data.SqlClient;
using IMS.DBHelper;

namespace IMS.Model
{
    public class ProductGroup
    {
        int id;
        string name;
        string note;

        public ProductGroup()
        {
            ;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public string Note
        {
            get { return note; }
            set { note = value; }
        }     
    }
}