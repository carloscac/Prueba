using AccesoDatos;
using AccesoDatos.Modelos;
using ApiInmobiliaria.Imagenes;
using ApiInmobiliaria.Models;
using Aplicacion.Interfaces;
using AutoMapper;
using Entidades.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiInmobiliaria.Controllers
{


    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class InmobiliariaController : ControllerBase
    {
        private IMapper _mapper;
        private IRepositorioService _servicio;

        public InmobiliariaController(IMapper mapper, IRepositorioService servicio)
        {
            _mapper = mapper;
            _servicio = servicio;
        }

        #region Property

        #region GET

        /// <summary>
        /// Método para obtener el listado de propiedades por Owner
        /// </summary>
        /// <param name="idOwner">Idnetificador del owner que se esta consultando</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RespuestaServicio<List<PropertyEntidad>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RespuestaServicio<List<PropertyEntidad>>))]
        public async Task<IActionResult> ListaPropiedadesPorOwner(string idOwner)
        {
            int idConsulta = -1;
            if (int.TryParse(idOwner, out idConsulta) == false)
            {
                RespuestaServicio<List<PropertyEntidad>> respuesta = new RespuestaServicio<List<PropertyEntidad>>();
                respuesta.Mensaje = $"El id '{idOwner}' no es valido";
                respuesta.Realizado = false;
                respuesta.Mensajes = null;
                return BadRequest(respuesta);

            }
            else
            {
                return Ok(await _servicio.ListaPropiedadesPorIdOwner(idConsulta));
            }

        }

        /// <summary>
        /// Método para listar propiedades aplicando filtros
        /// </summary>
        /// <param name="year"></param>
        /// <param name="idOwner"></param>
        /// <param name="idProperty"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RespuestaServicio<List<PropertyEntidad>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RespuestaServicio<List<PropertyEntidad>>))]
        public async Task<RespuestaServicio<List<PropertyEntidad>>> ConsultaPropiedadesPorFiltro(int? year, int? idOwner, int? idProperty)
        {
            return await _servicio.ListaPropiedadesPorFiltro(year, idOwner, idProperty);
        }
               
        #endregion

        #region Post

        /// <summary>
        /// Método para registrar una propiedad
        /// </summary>
        /// <param name="property">Objeto con la información de la propiedad</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RespuestaServicio<PropertyEntidad>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RespuestaServicio<PropertyEntidad>))]
        public async Task<ActionResult> RegistrarProperty(PropertyEntidad property)
        {
            ManejoArchivos manejoArchivos = new ManejoArchivos();
            RespuestaServicio<PropertyEntidad> respuesta = new RespuestaServicio<PropertyEntidad>();

            foreach (var item in property.lsImagenes)
            {
                RespuestaImagen respuestaImagen = manejoArchivos.GuardarImagen(item.PropertyFile, "Property");
                item.PropertyFile = (respuestaImagen.Realizado) ? respuestaImagen.direccionImagen : item.PropertyFile;
            }

            respuesta = await _servicio.RegistraPropiedad(property);
            if (respuesta.Realizado)
            {
                return Ok(respuesta);
            }
            else
            {
                return BadRequest(respuesta);
            }
        }
        /// <summary>
        /// Método para el registro de una imagen asociado a una propiedad
        /// </summary>
        /// <param name="propertyImage">Objeto con la información de la imagen</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RespuestaServicio<PropertyImageEntidad>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RespuestaServicio<PropertyImageEntidad>))]
        public async Task<ActionResult> RegistrarImagenProperty(PropertyImageEntidad propertyImage)
        {
            ManejoArchivos manejoArchivos = new ManejoArchivos();
            RespuestaServicio<PropertyImageEntidad> respuesta = new RespuestaServicio<PropertyImageEntidad>();

            try
            {
                RespuestaImagen respuestaImagen = manejoArchivos.GuardarImagen(propertyImage.PropertyFile, "Property");
                if (respuestaImagen.Realizado)
                {
                    propertyImage.PropertyFile = respuestaImagen.direccionImagen;
                    respuesta = await _servicio.RegistraImagenPropiedad(propertyImage);

                }
                else
                {
                    respuesta.Realizado = false;
                    respuesta.Mensaje = "Este campo recibe un archivo traducido a base64, el string ingresado no es la representación de una imagen";
                }


                if (respuesta.Realizado)
                {
                    return Ok(respuesta);
                }
                else
                {
                    return BadRequest(respuesta);
                }
            }
            catch (Exception)
            {

                respuesta.Realizado = false;
                respuesta.Mensaje = "La imagen ingresada no se pudo procesar";
                return BadRequest(respuesta);
            }
        }

        #endregion

        #region Put

        /// <summary>
        /// Método para actualizar los datos de una propiedad
        /// </summary>
        /// <param name="property">Objeto con la información de la propiedad a actualizar</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RespuestaServicio<PropertyEntidad>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RespuestaServicio<PropertyEntidad>))]
        public async Task<ActionResult> ActualizarProperty(PropertyModel property)
        {
            ManejoArchivos manejoArchivos = new ManejoArchivos();
            RespuestaServicio<PropertyEntidad> respuesta = new RespuestaServicio<PropertyEntidad>();

            PropertyEntidad propertyEntidad = _mapper.Map<PropertyEntidad>(property);
            respuesta = await _servicio.ActualizarProperties(propertyEntidad);
            if (respuesta.Realizado)
            {
                return Ok(respuesta);
            }
            else
            {
                return BadRequest(respuesta);
            }
        }

        /// <summary>
        /// Método para realizar la actualización de una propiedad
        /// </summary>
        /// <param name="Id">Id de la propiedad</param>
        /// <param name="price">Valor a modificar</param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RespuestaServicio<PropertyEntidad>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RespuestaServicio<PropertyEntidad>))]
        public async Task<ActionResult> ActualizarPropertyPrice(int Id, int price)
        {
            RespuestaServicio<PropertyEntidad> respuesta = new RespuestaServicio<PropertyEntidad>();

            PropertyEntidad property = new PropertyEntidad { idProperty = Id, Price = price };

            respuesta = await _servicio.ActualizarPricePropiedad(property);
            if (respuesta.Realizado)
            {
                return Ok(respuesta);
            }
            else
            {
                return BadRequest(respuesta);
            }
        }

        #endregion

        #endregion

        #region Owner

        // GET: api/<InmobiliariaController>
        /// <summary>
        /// Método para consulta de las inmobiliarias
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("ListaOwner")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RespuestaServicio<List<OwnerEntidad>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RespuestaServicio<List<OwnerEntidad>>))]
        public async Task<RespuestaServicio<List<OwnerEntidad>>> ListaOwner()
        {
            return await _servicio.ListOwner();
        }

       

        /// <summary>
        /// Método para realizar la creación de un owner
        /// </summary>
        /// <param name="owner">Objeto con la informacion del owner que se quiere crear</param>
        /// <returns></returns>
        // POST api/<InmobiliariaController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RespuestaServicio<OwnerEntidad>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RespuestaServicio<OwnerEntidad>))]
        public async Task<RespuestaServicio<OwnerEntidad>> Post(OwnerEntidad owner)
        {
            if (ModelState.IsValid)
            {
                RespuestaImagen respuestaImagen = new ManejoArchivos().GuardarImagen(owner.Photo, "Owner");
                if (!respuestaImagen.Realizado)
                {
                    owner.Photo = respuestaImagen.direccionImagen;
                }

                return await _servicio.CreateOwner(owner);
            }
            else
            {
                RespuestaServicio<OwnerEntidad> respuesta = new RespuestaServicio<OwnerEntidad>
                {
                    Mensajes = ModelState.Values.Select(e => e.Errors).ToList().Select(e => new MensajesServicio { Mensaje = e.FirstOrDefault().ErrorMessage }).ToList()
                };
                return respuesta;
            }


        }

        // PUT api/<InmobiliariaController>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RespuestaServicio<OwnerEntidad>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RespuestaServicio<OwnerEntidad>))]
        public async Task<RespuestaServicio<OwnerEntidad>> Put(OwnerEntidad owner)
        {
            return await _servicio.EditOwner(owner);
        }

       
        #endregion


     

    }
}
