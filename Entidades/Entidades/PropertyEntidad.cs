using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    public class PropertyEntidad
    {
        public int idProperty { get; set; }
        public string NameProperty { get; set; }
        public string Addres { get; set; }
        public int Price { get; set; }
        public string CodeInternal { get; set; }
        public int YearProperty { get; set; }
        public int IdOwner { get; set; }
        public List<PropertyImageEntidad> lsImagenes { get; set; }
        public List<PropertyTraceEntidad> lstTrace { get; set; }
    }
}
