using Aplicacion.Interfaces;
using Aplicacion.Interfaces.Repositorio;
using Aplicacion.Validaciones;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class RepositorioServicio : IRepositorioService
    {
        private IRepositorio _repositorio;

        public RepositorioServicio(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        #region Owner

        /// <summary>
        /// Método para la inserción de un propietario
        /// </summary>
        /// <param name="owner">Objeto con la información del propietario que se quiere crear en el sistema</param>
        /// <returns>Obejto con la información del propietario que se creo.</returns>
        public async Task<RespuestaServicio<OwnerEntidad>> CreateOwner(OwnerEntidad owner)
        {
            RespuestaServicio<OwnerEntidad> respuesta = new Entidades.Entidades.RespuestaServicio<OwnerEntidad>();
            try
            {

                respuesta = new RespuestaServicio<OwnerEntidad>
                {
                    Mensaje = "Se realizo el registro del propietario de manera exitosa",
                    Realizado = true,
                    ObjetoRespuesta = await _repositorio.CreateOwner(owner)
                };
            }
            catch (Exception Ex)
            {
                respuesta = new RespuestaServicio<OwnerEntidad>
                {
                    Mensaje = "No se pudo realizar el registro del propietario",
                    Realizado = false,
                    Mensajes = new List<MensajesServicio> { new MensajesServicio { IdMensaje = "1", Mensaje = Ex.Message } }
                };
            }
            return respuesta;
        }

        /// <summary>
        /// Método para la consulta de propietarios
        /// </summary>
        /// <returns></returns>
        public async Task<RespuestaServicio<List<OwnerEntidad>>> ListOwner()
        {
            RespuestaServicio<List<OwnerEntidad>> respuesta = new RespuestaServicio<List<OwnerEntidad>>();
            try
            {

                respuesta = new RespuestaServicio<List<OwnerEntidad>>
                {
                    Mensaje = "Se entrega el listado de propietarios",
                    Realizado = true,
                    ObjetoRespuesta = await _repositorio.ListOwner()
                };
            }
            catch (Exception Ex)
            {
                respuesta = new RespuestaServicio<List<OwnerEntidad>>
                {
                    Mensaje = "No se pudo realizar la consulta de propietarios",
                    Realizado = false,
                    Mensajes = new List<MensajesServicio> { new MensajesServicio { IdMensaje = "1", Mensaje = Ex.Message } }
                };
            }
            return respuesta;
        }

        /// <summary>
        /// Método para realizar la actualización de la información de un propietario
        /// </summary>
        /// <param name="owner">Objeto con la información del propietario que se quiere acutalizar</param>
        /// <returns></returns>
        public async Task<RespuestaServicio<OwnerEntidad>> EditOwner(OwnerEntidad owner)
        {
            RespuestaServicio<OwnerEntidad> respuesta = new RespuestaServicio<OwnerEntidad>();
            try
            {
                if (owner.LstProperties.Where(e => e.idProperty != 0).Count() == 0)
                {
                    owner.LstProperties = new List<PropertyEntidad>();
                }

                respuesta = new RespuestaServicio<OwnerEntidad>
                {
                    Mensaje = "Se realizó la actualización del propietario de forma exitosa",
                    Realizado = true,
                    ObjetoRespuesta = await _repositorio.EditOwner(owner)
                };
            }
            catch (Exception Ex)
            {
                respuesta = new RespuestaServicio<OwnerEntidad>
                {
                    Mensaje = "No se pudo realizar la actualización del propietario",
                    Realizado = false,
                    Mensajes = new List<MensajesServicio> { new MensajesServicio { IdMensaje = "1", Mensaje = Ex.Message } }
                };
            }
            return respuesta;
        }

        #endregion

        #region Propiedades

     
        /// <summary>
        /// Método para consultar propiedades por Owner
        /// </summary>
        /// <param name="idOwner">Identificador del Owner que se quiere consultar</param>
        /// <returns>Listado de propiedades</returns>
        public async Task<RespuestaServicio<List<PropertyEntidad>>> ListaPropiedadesPorIdOwner(int idOwner)
        {
            RespuestaServicio<List<PropertyEntidad>> respuesta = new RespuestaServicio<List<PropertyEntidad>>();
            try
            {
                List<PropertyEntidad> properties = await _repositorio.ListaPropiedadesXIdOwner(idOwner);
                respuesta = new RespuestaServicio<List<PropertyEntidad>>
                {
                    Mensaje = (properties.Count == 0) ? "El owner no tiene propiedades registradas" : "Se entrega listado de las propiedades creadas en el sistema",
                    Realizado = true,
                    ObjetoRespuesta = properties
                };
            }
            catch (Exception Ex)
            {
                respuesta = new RespuestaServicio<List<PropertyEntidad>>
                {
                    Mensaje = "No se pudo realizar la consulta de propiedades",
                    Realizado = false,
                    Mensajes = new List<MensajesServicio> { new MensajesServicio { IdMensaje = "1", Mensaje = Ex.Message } }
                };
            }
            return respuesta;
        }

        /// <summary>
        /// Método para realizar el registro de una property
        /// </summary>
        /// <param name="property">Objeto con la información necesaria para realizar el registro</param>
        /// <returns>Retorna respuesta indicando si se crea o no la property</returns>
        public async Task<RespuestaServicio<PropertyEntidad>> RegistraPropiedad(PropertyEntidad property)
        {

            RespuestaServicio<PropertyEntidad> respuesta = new RespuestaServicio<PropertyEntidad>();
            PropertyValidation validationRules = new PropertyValidation();
            try
            {
                var resultVal = validationRules.Validate(property);
                if (!resultVal.IsValid)
                {
                    respuesta.Mensajes = new List<MensajesServicio>();
                    respuesta.Mensaje = "Ocurrieron errores al validar la información ingresada";
                    respuesta.Realizado = false;
                    respuesta.ObjetoRespuesta = null;
                    resultVal.Errors.ForEach(x => respuesta.Mensajes.Add(new MensajesServicio { Mensaje = x.ErrorMessage, IdMensaje = x.ErrorCode}));
                    return respuesta;
                }
                bool bOwnerCreado = await _repositorio.ValidarOwner(property.IdOwner);
                if (!bOwnerCreado)
                    return new RespuestaServicio<PropertyEntidad>
                    {
                        Mensaje = "El owner no se encuentra creado en la aplicación",
                        Realizado = false,
                        ObjetoRespuesta = null
                    };

                PropertyEntidad properties = await _repositorio.RegistraPropiedad(property);
                if (properties == null)
                {
                    respuesta = new RespuestaServicio<PropertyEntidad>
                    {
                        Mensaje = "No se pudo realizar la creación de la propiedad",
                        Realizado = false,
                        ObjetoRespuesta = null
                    };
                }
                else
                {
                    respuesta = new RespuestaServicio<PropertyEntidad>
                    {
                        Mensaje = "Se realizó la creación de la propiedad",
                        Realizado = true,
                        ObjetoRespuesta = properties
                    };
                }

            }
            catch (Exception Ex)
            {
                respuesta = new RespuestaServicio<PropertyEntidad>
                {
                    Mensaje = "No se pudo realizar el registro de la propiedad",
                    Realizado = false,
                    Mensajes = new List<MensajesServicio> { new MensajesServicio { IdMensaje = "1", Mensaje = Ex.Message } }
                };
            }
            return respuesta;
        }

        /// <summary>
        /// Método para realizar el registro de imagenes asociados a una property
        /// </summary>
        /// <param name="propertyImagen">Objeto con la información de la imagen que se quiere registrar</param>
        /// <returns></returns>
        public async Task<RespuestaServicio<PropertyImageEntidad>> RegistraImagenPropiedad(PropertyImageEntidad propertyImagen)
        {

            RespuestaServicio<PropertyImageEntidad> respuesta = new RespuestaServicio<PropertyImageEntidad>();
            try
            {
                PropertyEntidad property = await _repositorio.ConsultaPropertiesPorID(propertyImagen.IdProperty);
                if (property == null)
                    return new RespuestaServicio<PropertyImageEntidad>
                    {
                        Mensaje = "La propiedad asociada a la imagen no existe",
                        Realizado = false,
                        ObjetoRespuesta = null
                    };

                PropertyImageEntidad properties = await _repositorio.RegistrarImagenProperty(propertyImagen);
                if (properties == null)
                {
                    respuesta = new RespuestaServicio<PropertyImageEntidad>
                    {
                        Mensaje = "No se pudo realizar el registro de la imagen",
                        Realizado = false,
                        ObjetoRespuesta = null
                    };
                }
                else
                {
                    respuesta = new RespuestaServicio<PropertyImageEntidad>
                    {
                        Mensaje = "Se realizó el registro de la imagen",
                        Realizado = true,
                        ObjetoRespuesta = properties
                    };
                }

            }
            catch (Exception Ex)
            {
                respuesta = new RespuestaServicio<PropertyImageEntidad>
                {
                    Mensaje = "No se pudo realizar el registro de la imagen",
                    Realizado = false,
                    Mensajes = new List<MensajesServicio> { new MensajesServicio { IdMensaje = "1", Mensaje = Ex.Message } }
                };
            }
            return respuesta;
        }


        /// <summary>
        /// Método para realizar la actualización de una propiedad
        /// </summary>
        /// <param name="property">Objeto con la información de la propiedad que se quiere actualizar</param>
        /// <returns></returns>
        public async Task<RespuestaServicio<PropertyEntidad>> ActualizarProperties(PropertyEntidad property)
        {

            RespuestaServicio<PropertyEntidad> respuesta = new RespuestaServicio<PropertyEntidad>();
            try
            {
                PropertyEntidad propertyBD = await _repositorio.ConsultaPropertiesPorID(property.idProperty);
                if (propertyBD == null)
                    return new RespuestaServicio<PropertyEntidad>
                    {
                        Mensaje = "La propiedad que se quiere actualizar no existe",
                        Realizado = false,
                        ObjetoRespuesta = null
                    };
               


                PropertyEntidad properties = await _repositorio.ActualizarPropiedad(property);
                if (properties == null)
                {
                    respuesta = new RespuestaServicio<PropertyEntidad>
                    {
                        Mensaje = "No se pudo realizar la actualización de la propiedad",
                        Realizado = false,
                        ObjetoRespuesta = null
                    };
                }
                else
                {
                    respuesta = new RespuestaServicio<PropertyEntidad>
                    {
                        Mensaje = "Se realizó la actualización de la propiedad",
                        Realizado = true,
                        ObjetoRespuesta = properties
                    };
                }

            }
            catch (Exception Ex)
            {
                respuesta = new RespuestaServicio<PropertyEntidad>
                {
                    Mensaje = "No se pudo realizar la actualización de la propiedad",
                    Realizado = false,
                    Mensajes = new List<MensajesServicio> { new MensajesServicio { IdMensaje = "1", Mensaje = Ex.Message } }
                };
            }
            return respuesta;
        }

        /// <summary>
        /// Métódo para actualizar el precio de la propiedad
        /// </summary>
        /// <param name="property">Objeto con la información de la propiedad que se quiere actualizar</param>
        /// <returns></returns>
        public async Task<RespuestaServicio<PropertyEntidad>> ActualizarPricePropiedad(PropertyEntidad property)
        {

            RespuestaServicio<PropertyEntidad> respuesta = new RespuestaServicio<PropertyEntidad>();
            try
            {
                PropertyEntidad propertyBD = await _repositorio.ConsultaPropertiesPorID(property.idProperty);
                if (propertyBD == null)
                    return new RespuestaServicio<PropertyEntidad>
                    {
                        Mensaje = "La propiedad que se quiere actualizar no existe",
                        Realizado = false,
                        ObjetoRespuesta = null
                    };
          
                PropertyEntidad properties = await _repositorio.ActualizarPricePropiedad(property);
                if (properties == null)
                {
                    respuesta = new RespuestaServicio<PropertyEntidad>
                    {
                        Mensaje = "No se pudo realizar la actualización de la propiedad",
                        Realizado = false,
                        ObjetoRespuesta = null
                    };
                }
                else
                {
                    respuesta = new RespuestaServicio<PropertyEntidad>
                    {
                        Mensaje = "Se realizó la actualización de la propiedad",
                        Realizado = true,
                        ObjetoRespuesta = properties
                    };
                }

            }
            catch (Exception Ex)
            {
                respuesta = new RespuestaServicio<PropertyEntidad>
                {
                    Mensaje = "No se pudo realizar la actualización de la propiedad",
                    Realizado = false,
                    Mensajes = new List<MensajesServicio> { new MensajesServicio { IdMensaje = "1", Mensaje = Ex.Message } }
                };
            }
            return respuesta;
        }

        /// <summary>
        /// Método para consultar properties aplicando filtros ingresados por el usuario
        /// </summary>
        /// <param name="year">Dato inrgesado por el cliente</param>
        /// <param name="idOwner">Identificador de Owner</param>
        /// <param name="idProperty">Identificador de propiedad</param>
        /// <returns>Lista de propiedades</returns>
        public async Task<RespuestaServicio<List<PropertyEntidad>>> ListaPropiedadesPorFiltro(int? year, int? idOwner, int? idProperty)
        {

            RespuestaServicio<List<PropertyEntidad>> respuesta = new RespuestaServicio<List<PropertyEntidad>>();
            try
            {
               

                List<PropertyEntidad> properties = await _repositorio.ListadoPropertiesFiltradas(year, idOwner, idProperty);
                if (properties == null)
                {
                    respuesta = new RespuestaServicio<List<PropertyEntidad>>
                    {
                        Mensaje = "No se octubo información con los filtros realizados",
                        Realizado = false,
                        ObjetoRespuesta = null
                    };
                }
                else
                {
                    respuesta = new RespuestaServicio<List<PropertyEntidad>>
                    {
                        Mensaje = "Resultado del filtro realizado",
                        Realizado = true,
                        ObjetoRespuesta = properties
                    };
                }

            }
            catch (Exception Ex)
            {
                respuesta = new RespuestaServicio<List<PropertyEntidad>>
                {
                    Mensaje = "No se pudo realizar la actualización de la propiedad",
                    Realizado = false,
                    Mensajes = new List<MensajesServicio> { new MensajesServicio { IdMensaje = "1", Mensaje = Ex.Message } }
                };
            }
            return respuesta;
        }

        #endregion

    }
}
