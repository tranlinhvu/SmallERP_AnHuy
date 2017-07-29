using System;
using System.Data;
using System.Data.SqlClient;
using IMS.DBHelper;

namespace IMS.Model
{
    public class InventoryInputItem
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int DiffNo { get; set; }
        public string CodeEx { get; set; }

        public InventoryInputItem()
        {

        }
    }
}