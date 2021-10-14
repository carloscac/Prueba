using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.Modelos
{
    public partial class PropertyDb
    {
        public PropertyDb()
        {
            PropertyImageDbs = new HashSet<PropertyImageDb>();
            PropertyTraceDbs = new HashSet<PropertyTraceDb>();
        }

        public int IdProperty { get; set; }
        public string NameProperty { get; set; }
        public string Addres { get; set; }
        public decimal? Price { get; set; }
        public string CodeInternal { get; set; }
        public int? YearProperty { get; set; }
        public int? IdOwner { get; set; }

        public virtual OwnerDb IdOwnerNavigation { get; set; }
        public virtual ICollection<PropertyImageDb> PropertyImageDbs { get; set; }
        public virtual ICollection<PropertyTraceDb> PropertyTraceDbs { get; set; }
    }
}
