using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    public class PropertyTraceEntidad
    {
        public int IdPropertyTrace { get; set; }
        public DateTime DateSale { get; set; }
        public string TraceName { get; set; }
        public int TraceValue { get; set; }
        public int TraceTax { get; set; }
        public int IdProperty { get; set; }
    }
}
