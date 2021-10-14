using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
   public interface IRepositorioService
    {
        Task<RespuestaServicio<OwnerEntidad>> CreateOwner(OwnerEntidad owner);
        Task<RespuestaServicio<List<OwnerEntidad>>> ListOwner();
        Task<RespuestaServicio<OwnerEntidad>> EditOwner(OwnerEntidad owner);
        Task<RespuestaServicio<List<PropertyEntidad>>> ListaPropiedades();
        Task<RespuestaServicio<List<PropertyEntidad>>> ListaPropiedadesPorIdOwner(int idOwner);
        Task<RespuestaServicio<PropertyEntidad>> RegistraPropiedad(PropertyEntidad property);
        Task<RespuestaServicio<PropertyImageEntidad>> RegistraImagenPropiedad(PropertyImageEntidad propertyImagen);
        Task<RespuestaServicio<PropertyEntidad>> ActualizarProperties(PropertyEntidad property);
        Task<RespuestaServicio<PropertyEntidad>> ActualizarPricePropiedad(PropertyEntidad property);
        Task<RespuestaServicio<List<PropertyEntidad>>> ListaPropiedadesPorFiltro(int? year, int? idOwner, int? idProperty);
    }
}
