using System;
using System.Data;
using System.Data.SqlClient;
using IMS.DBHelper;

namespace IMS.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public int IdProvince { get; set; }
        public int IdDistrict { get; set; }
        public int IdWard { get; set; }
        public string Address { get; set; }
        public string Ocupation { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Customer()
        {

        }
    }
}