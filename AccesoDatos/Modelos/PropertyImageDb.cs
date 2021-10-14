using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.Modelos
{
    public partial class PropertyImageDb
    {
        public int IdPropertyImage { get; set; }
        public int? IdProperty { get; set; }
        public string PropertyFile { get; set; }
        public bool? Habilitada { get; set; }

        public virtual PropertyDb IdPropertyNavigation { get; set; }
    }
}
