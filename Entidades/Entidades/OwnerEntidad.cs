using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    public class OwnerEntidad
    {
        
        public int idOwner { get; set; }
        [Required(ErrorMessage = "El nombre del propietario es requerido")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public DateTime Birthday { get; set; }
        public ICollection<PropertyEntidad> LstProperties { get; set; }
    }
}
