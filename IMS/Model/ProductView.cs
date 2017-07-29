using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Model
{
    class ProductView
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Standard { get; set; }
        public long PriceIn { get; set; }
        public long PriceOut { get; set; }
        public string UnitName { get; set; }
        public string GroupName { get; set; }
        public string ManufactureName { get; set; }
        public int RowNumber { get; internal set; }

        public ProductView(int id, string code, string name, string standard, long priceIn, long priceOut, string unitName, string groupName, string Manufacturename)
        {
            Id = id;
            Code = code;
            Name = name;
            Standard = standard;
            PriceIn = priceIn;
            PriceOut = priceOut;
            UnitName = unitName;
            GroupName = groupName;
            ManufactureName = Manufacturename;
        }

        public ProductView()
        {

        }
    }

}
