using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.Modelos
{
    public partial class PropertyTraceDb
    {
        public int IdPropertyTrace { get; set; }
        public DateTime? DateSale { get; set; }
        public string TraceName { get; set; }
        public decimal? TraceValue { get; set; }
        public decimal? TraceTax { get; set; }
        public int? IdProperty { get; set; }

        public virtual PropertyDb IdPropertyNavigation { get; set; }
    }
}
