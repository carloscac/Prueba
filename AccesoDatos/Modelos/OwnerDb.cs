using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.Modelos
{
    public partial class OwnerDb
    {
        public OwnerDb()
        {
            PropertyDbs = new HashSet<PropertyDb>();
        }

        public int IdOwner { get; set; }
        public string OwnerName { get; set; }
        public string OwnerAddress { get; set; }
        public string Photo { get; set; }
        public DateTime? Birthday { get; set; }

        public virtual ICollection<PropertyDb> PropertyDbs { get; set; }
    }
}
