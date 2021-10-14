using ApiInmobiliaria.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiInmobiliaria.Imagenes
{
    public class ManejoArchivos
    {
        /// <summary>
        /// Método para guardar imagenes en la aplicación y devuelve la ruta para ser almacenada en base de datos
        /// </summary>
        /// <param name="sImagen">Imagen en base 64</param>
        /// <param name="Tipo">Identifica si la imagen es de un Owner o una Property</param>
        /// <returns>String con la ruta de la image</returns>
        public RespuestaImagen GuardarImagen(string sImagen, string Tipo) {
            RespuestaImagen respuesta = new RespuestaImagen();
            try
            {

                string[] archivos = sImagen.Split(",");
                string extension = archivos[0].Replace("data:image/", ".").Replace(";base64", string.Empty);

                //string pa = new Microsoft.AspNetCore.Http.PathString("~/Adjuntos");
              
                var originalDirectory = new DirectoryInfo(string.Format("{0}Adjuntos", AppDomain.CurrentDomain.BaseDirectory));
                string pathString = System.IO.Path.Combine(originalDirectory.ToString(), Tipo.ToString());
                



                var fileName1 = Guid.NewGuid().ToString() + extension;
                bool isExists = System.IO.Directory.Exists(pathString);

                if (!isExists)
                    System.IO.Directory.CreateDirectory(pathString);

                string path = string.Format("{0}/{1}", pathString, fileName1);

                Byte[] bytes = Convert.FromBase64String(archivos[1]);
                File.WriteAllBytes(path, bytes);
                respuesta.direccionImagen = path;
                respuesta.Realizado = true;

            }
            catch (Exception Ex)
            {
                respuesta.Realizado = false;
            }
            return respuesta;
        }
    }
}
