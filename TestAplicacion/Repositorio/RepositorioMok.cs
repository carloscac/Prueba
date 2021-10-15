using Aplicacion.Interfaces.Repositorio;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAplicacion.Repositorio
{
    public class RepositorioMok : IRepositorio
    {
        private List<PropertyEntidad> properties;
        private List<OwnerEntidad> ownerEntidads;

        public RepositorioMok()
        {
            properties = new List<PropertyEntidad> {
                new PropertyEntidad { Addres = "CALLE 123", CodeInternal = "123", IdOwner = 1, idProperty = 1, NameProperty = "CASA 1", Price = 250000000, YearProperty = 2001 },
            new PropertyEntidad { Addres = "CALLE 124", CodeInternal = "124", IdOwner = 1, idProperty = 2, NameProperty = "CASA 2", Price = 250000000, YearProperty = 2020 },
            new PropertyEntidad { Addres = "CALLE 125", CodeInternal = "125", IdOwner = 2, idProperty = 3, NameProperty = "CASA 3", Price = 250000000, YearProperty = 2001 },
            new PropertyEntidad { Addres = "CALLE 126", CodeInternal = "126", IdOwner = 2, idProperty = 4, NameProperty = "CASA 4", Price = 250000000, YearProperty = 2010 },
            new PropertyEntidad { Addres = "CALLE 127", CodeInternal = "127", IdOwner = 3, idProperty = 5, NameProperty = "CASA 5", Price = 250000000, YearProperty = 2015 }
            };

            ownerEntidads = new List<OwnerEntidad> { 
            new OwnerEntidad{ Address = "Calle 456", Birthday = Convert.ToDateTime("1995-01-01"), idOwner = 1, Name = "Pedro"},
            new OwnerEntidad{ Address = "Calle 987", Birthday = Convert.ToDateTime("1985-10-01"), idOwner = 2, Name = "Pedro"},
            new OwnerEntidad{ Address = "Calle 5466", Birthday = Convert.ToDateTime("2001-06-01"), idOwner = 3, Name = "Pedro"},
            new OwnerEntidad{ Address = "Calle 12 32", Birthday = Convert.ToDateTime("1979-06-01"), idOwner = 4, Name = "Pedro"},
            new OwnerEntidad{ Address = "Calle 487", Birthday = Convert.ToDateTime("2010-01-01"), idOwner = 5, Name = "Pedro"},
            };
        }

        public Task<PropertyEntidad> ActualizarPricePropiedad(PropertyEntidad propertyEntidad)
        {
            throw new NotImplementedException();
        }

        public Task<PropertyEntidad> ActualizarPropiedad(PropertyEntidad propertyEntidad)
        {
            PropertyEntidad property = properties.Where(e => e.idProperty == propertyEntidad.idProperty).FirstOrDefault();
            property.Addres = string.IsNullOrEmpty(propertyEntidad.Addres) ? property.Addres : propertyEntidad.Addres;
            property.CodeInternal = string.IsNullOrEmpty(propertyEntidad.CodeInternal) ? property.CodeInternal : propertyEntidad.CodeInternal;
            property.IdOwner = string.IsNullOrEmpty(propertyEntidad.idProperty.ToString()) ? property.IdOwner : propertyEntidad.IdOwner;
            property.NameProperty = string.IsNullOrEmpty(propertyEntidad.NameProperty) ? property.NameProperty : propertyEntidad.NameProperty;
            property.Price = string.IsNullOrEmpty(propertyEntidad.Price.ToString()) ? property.Price : propertyEntidad.Price;

            return Task.FromResult(property);
        }

        public Task<PropertyEntidad> ConsultaPropertiesPorID(int idProperty)
        {
            return Task.FromResult(properties.Where(e => e.idProperty == idProperty).FirstOrDefault());
        }

        public Task<OwnerEntidad> CreateOwner(OwnerEntidad owner)
        {
            throw new NotImplementedException();
        }

        public Task<OwnerEntidad> EditOwner(OwnerEntidad owner)
        {
            throw new NotImplementedException();
        }

        public Task<List<PropertyEntidad>> ListadoPropertiesFiltradas(int? year, int? idOwner, int? idProperty)
        {
            var query = properties.AsQueryable();
            if (year != null)
            {
                query = query.Where(e => e.YearProperty == year);
            }
            if (idOwner != null)
            {
                query = query.Where(e => e.IdOwner == idOwner);
            }
            if (idProperty != null)
            {
                query = query.Where(e => e.idProperty == idProperty);
            }
            return Task.FromResult(query.ToList());
        }

        public Task<List<PropertyEntidad>> ListaPropiedades()
        {
            throw new NotImplementedException();
        }

        public Task<List<PropertyEntidad>> ListaPropiedadesXIdOwner(int IdOwner)
        {
            throw new NotImplementedException();
        }

        public Task<List<OwnerEntidad>> ListOwner()
        {
            throw new NotImplementedException();
        }

        public Task<PropertyEntidad> RegistraPropiedad(PropertyEntidad property)
        {

            int newIdProperty = properties.Max(e => e.idProperty) + 1;
            property.idProperty = newIdProperty;
            properties.Add(property);
            return Task.FromResult(property);
        }

        public Task<PropertyImageEntidad> RegistrarImagenProperty(PropertyImageEntidad propertyImage)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidarOwner(int idOwner)
        {
            OwnerEntidad ownerEntidad = ownerEntidads.Where(e => e.idOwner == idOwner).FirstOrDefault();
            return Task.FromResult(ownerEntidad == null ? false : true);
        }
    }
}
