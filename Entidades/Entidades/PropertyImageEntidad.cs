using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
   public class PropertyImageEntidad
    {
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        /// <summary>
        /// Recibe una imagen en base64
        /// </summary>
        public string PropertyFile { get; set; }
        public bool Habilidata { get; set; }
    }
}
