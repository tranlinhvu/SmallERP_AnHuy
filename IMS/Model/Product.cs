using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Model
{
    class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Standard { get; set; }
        public long PriceIn { get; set; }
        public long PriceOut { get; set; }
        public int IdUnit { get; set; }
        public int IdGroup { get; set; }
        public int IdManufacture { get; set; }

        public Product(int id, string code, string name, string standard, long priceIn, long priceOut, int idUnit, int idGroup, int idManufacture)
        {
            Id = id;
            Code = code;
            Name = name;
            Standard = standard;
            PriceIn = priceIn;
            PriceOut = priceOut;
            IdUnit = idUnit;
            IdGroup = idGroup;
            IdManufacture = idManufacture;
        }

        public Product()
        {

        }
    }
   
}
