using AccesoDatos.Modelos;
using Aplicacion.Interfaces.Repositorio;
using AutoMapper;
using Entidades.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class Repositorio : IRepositorio
    {
        private InmobiliariaContext _context;
        private IMapper _mapper;

        public Repositorio(InmobiliariaContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }


        #region Owner

        /// <summary>
        /// Método para realizar la creación de un nuevo propietario en la aplicación
        /// </summary>
        /// <param name="owner">Objeto con la información del propietario</param>
        /// <returns></returns>
        public async Task<OwnerEntidad> CreateOwner(OwnerEntidad owner)
        {
            var propertyOwner = _mapper.Map<OwnerDb>(owner);

            await _context.OwnerDbs.AddAsync(propertyOwner);
            _context.SaveChanges();
            owner.idOwner = propertyOwner.IdOwner;

            return owner;

        }

        /// <summary>
        /// Método para obtener un listado de los propietarios creados en el sistema
        /// </summary>
        /// <returns>Lista de propietarios</returns>
        public async Task<List<OwnerEntidad>> ListOwner()
        {
            List<OwnerDb> propertyOwners = await _context.OwnerDbs.Include(x => x.PropertyDbs).ToListAsync();

            return _mapper.Map<List<OwnerEntidad>>(propertyOwners);
        }

        /// <summary>
        /// Método para realizar la actualización de un propietario
        /// </summary>
        /// <param name = "owner" ></ param >
        /// < returns ></ returns >
        public async Task<OwnerEntidad> EditOwner(OwnerEntidad owner)
        {
            OwnerDb propertyOwner = _mapper.Map<OwnerDb>(owner);

            if (propertyOwner != null)
            {
                _context.OwnerDbs.Update(propertyOwner);
                await _context.SaveChangesAsync();
                return owner;
            }
            else
            {
                return new OwnerEntidad();
            }

        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Método para obtener la información de las propiedades ingresadas en el sistema
        /// </summary>
        /// <returns>Listado de propiedades</returns>
        public async Task<List<PropertyEntidad>> ListaPropiedades()
        {
            List<PropertyDb> properties = await _context.PropertyDbs
                 .Include(x => x.PropertyImageDbs)
                .Include(x => x.PropertyTraceDbs)
                .ToListAsync();
            return _mapper.Map<List<PropertyEntidad>>(properties);
        }

        /// <summary>
        /// Método para consultar las propiedades utilizando como filtro el id del owner
        /// </summary>
        /// <param name="IdOwner">Identificador del owner</param>
        /// <returns>Listado de propiedades</returns>
        public async Task<List<PropertyEntidad>> ListaPropiedadesXIdOwner(int IdOwner)
        {
            List<PropertyDb> properties = await _context.PropertyDbs.Where(e => e.IdOwner == IdOwner)
                 .Include(x => x.PropertyImageDbs)
                .Include(x => x.PropertyTraceDbs)
                .ToListAsync();
            return _mapper.Map<List<PropertyEntidad>>(properties);
        }

        /// <summary>
        /// Método para realiza la creación de una nueva propiedad
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public async Task<PropertyEntidad> RegistraPropiedad(PropertyEntidad property)
        {

            var propertyNew = _mapper.Map<PropertyDb>(property);

            await _context.PropertyDbs.AddAsync(propertyNew);
            _context.SaveChanges();
            PropertyDb propertyDbs = await _context.PropertyDbs
                .Include(x => x.PropertyImageDbs)
                .Include(x => x.PropertyTraceDbs)
                .Where(e => e.IdProperty == propertyNew.IdProperty).FirstOrDefaultAsync();
            property = _mapper.Map<PropertyEntidad>(propertyDbs);

            return property;
        }

        /// <summary>
        /// Método para validar la existencia de un owner
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        public async Task<bool> ValidarOwner(int idOwner)
        {
            bool bResultado;
            OwnerDb owner = await _context.OwnerDbs.Where(e => e.IdOwner == idOwner).FirstOrDefaultAsync();
            bResultado = (owner == null) ? false : true;
            return bResultado;
        }

        /// <summary>
        /// Método para realizar el registro de una imagen asociada a una property
        /// </summary>
        /// <param name="propertyImage">Objeto de la imagen que se quiere registrar</param>
        /// <returns></returns>
        public async Task<PropertyImageEntidad> RegistrarImagenProperty(PropertyImageEntidad propertyImage)
        {
            try
            {
                PropertyImageDb imageDb = _mapper.Map<PropertyImageDb>(propertyImage);
                await _context.PropertyImageDbs.AddAsync(imageDb);
                _context.SaveChanges();
                propertyImage.IdPropertyImage = imageDb.IdPropertyImage;
            }
            catch (Exception)
            {

                propertyImage = new PropertyImageEntidad();
            }

            return propertyImage;
        }

        /// <summary>
        /// Método para realizar el guardado de una imagen
        /// </summary>
        /// <param name="idProperty">Identificador de la propiedad que se quiere registrar</param>
        /// <returns></returns>
        public async Task<bool> ValidarProperty(int idProperty)
        {
            bool bResultado;
            PropertyDb property = await _context.PropertyDbs.Where(e => e.IdOwner == idProperty).FirstOrDefaultAsync();
            bResultado = (property == null) ? false : true;
            return bResultado;
        }

        /// <summary>
        /// Método para actualizar la información de una propiedad
        /// </summary>
        /// <param name="propertyEntidad">Objeto con la información de la property que se quiere cambiar</param>
        /// <returns></returns>
        public async Task<PropertyEntidad> ActualizarPropiedad(PropertyEntidad propertyEntidad)
        {
            PropertyDb propertyDb = await _context.PropertyDbs.Where(e => e.IdProperty == propertyEntidad.idProperty).FirstOrDefaultAsync();

            propertyDb.IdOwner = propertyEntidad.IdOwner == 0 ? propertyDb.IdOwner : propertyEntidad.IdOwner;
            propertyDb.Price = propertyEntidad.Price == 0 ? propertyDb.Price : propertyEntidad.Price;
            propertyDb.YearProperty = propertyEntidad.YearProperty == 0 ? propertyDb.YearProperty : propertyEntidad.YearProperty;
            propertyDb.Addres = propertyEntidad.Addres ?? propertyDb.Addres;
            propertyDb.CodeInternal = propertyEntidad.CodeInternal ?? propertyDb.CodeInternal;
            propertyDb.NameProperty = propertyEntidad.NameProperty ?? propertyDb.NameProperty;

            await _context.SaveChangesAsync();
            return _mapper.Map<PropertyEntidad>(propertyDb);
        }

        /// <summary>
        /// Método para consultar una propiedad por su id
        /// </summary>
        /// <param name="idProperty">Identificador de la propiedad</param>
        /// <returns></returns>
        public async Task<PropertyEntidad> ConsultaPropertiesPorID(int idProperty)
        {
            PropertyDb propertyDb = await _context.PropertyDbs.Where(e => e.IdProperty == idProperty).FirstOrDefaultAsync();
            return _mapper.Map<PropertyEntidad>(propertyDb);
        }

        /// <summary>
        /// Método para realizar la actualización del atributo price a una property especifica
        /// </summary>
        /// <param name="propertyEntidad">Entidad con la información que se reuiere para acutalizar la property</param>
        /// <returns></returns>
        public async Task<PropertyEntidad> ActualizarPricePropiedad(PropertyEntidad propertyEntidad)
        {
            PropertyDb propertyDb = await _context.PropertyDbs.Where(e => e.IdProperty == propertyEntidad.idProperty).FirstOrDefaultAsync();
            propertyDb.Price = propertyEntidad.Price;

            await _context.SaveChangesAsync();
            return _mapper.Map<PropertyEntidad>(propertyDb);
        }

        /// <summary>
        /// Método para realizar consulta de propiedades aplicando filtros
        /// </summary>
        /// <param name="year">Año ingresado por el usuario</param>
        /// <param name="idOwner">Identificador del Owner</param>
        /// <param name="idProperty">Indentificador property</param>
        /// <returns>Listado de properties</returns>
        public async Task<List<PropertyEntidad>> ListadoPropertiesFiltradas(int? year, int? idOwner, int? idProperty)
        {

            string sYear = string.IsNullOrEmpty(year.ToString()) ? "null" : year.ToString();
            string sIdOwner = string.IsNullOrEmpty(idOwner.ToString()) ? "null" : idOwner.ToString();
            string sIdProperty = string.IsNullOrEmpty(idProperty.ToString()) ? "null" : idProperty.ToString();

            List<PropertyDb> properties = await _context.PropertyDbs.FromSqlRaw($"EXECUTE Inmobiliaria.PA_ConsultaPropiedadesPorFiltros {sYear}, {sIdOwner}, {sIdProperty}").ToListAsync();

            return _mapper.Map<List<PropertyEntidad>>(properties);
        }

        #endregion



    }
}
