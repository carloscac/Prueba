using Entidades.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces.Repositorio
{
    public interface IRepositorio
    {

        Task<OwnerEntidad> CreateOwner(OwnerEntidad owner);
        Task<List<OwnerEntidad>> ListOwner();
        Task<OwnerEntidad> EditOwner(OwnerEntidad owner);
        Task<List<PropertyEntidad>> ListaPropiedades();
        Task<List<PropertyEntidad>> ListaPropiedadesXIdOwner(int IdOwner);
        Task<PropertyEntidad> RegistraPropiedad(PropertyEntidad property);
        Task<bool> ValidarOwner(int idOwner);
        Task<PropertyImageEntidad> RegistrarImagenProperty(PropertyImageEntidad propertyImage);
        Task<PropertyEntidad> ActualizarPropiedad(PropertyEntidad propertyEntidad);
        Task<PropertyEntidad> ConsultaPropertiesPorID(int idProperty);
        Task<PropertyEntidad> ActualizarPricePropiedad(PropertyEntidad propertyEntidad);
        Task<List<PropertyEntidad>> ListadoPropertiesFiltradas(int? year, int? idOwner, int? idProperty);
    }
}
