using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
   public class RespuestaServicio <T>
    {
        public bool Realizado { get; set; }
        public string Mensaje { get; set; }
        public T ObjetoRespuesta { get; set; }
        public List<MensajesServicio> Mensajes { get; set; }
    }

   
}
