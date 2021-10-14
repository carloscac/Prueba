using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiInmobiliaria.Models
{
    public class PropertyModel
    {
        public int idProperty { get; set; }
        public string NameProperty { get; set; }
        public string Addres { get; set; }
        public int Price { get; set; }
        public string CodeInternal { get; set; }
        public int YearProperty { get; set; }
        public int IdOwner { get; set; }
    }
}
