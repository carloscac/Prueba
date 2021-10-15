using ApiInmobiliaria;
using Aplicacion.Interfaces.Repositorio;
using Entidades.Entidades;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TestAplicacion.Repositorio;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace TestAplicacion
{
    [TestFixture]
    public class Tests
    {

        private static WebApplicationFactory<Startup> _factory;

        [OneTimeSetUp]
        public void IniciarObjetos()
        {
            _factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting("https_port", "44355").UseEnvironment("Testing");
                    builder.ConfigureServices(services =>
                    {
                        services.AddScoped<IRepositorio, RepositorioMok>();

                    });
                });
        }

        [Test]
        public async Task GetAllByOwnwer()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/Inmobiliaria/ConsultaPropiedadesPorFiltro?idOwner=2");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            RespuestaServicio<List<PropertyEntidad>> resultado = Newtonsoft.Json.JsonConvert.DeserializeObject<RespuestaServicio<List<PropertyEntidad>>>(result);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(true, resultado.Realizado, "Se obtuvo resultado");
            Assert.IsNull(resultado.Mensajes);
            Assert.IsNotNull(resultado.ObjetoRespuesta);
            Assert.AreEqual(true, resultado.ObjetoRespuesta[0].IdOwner == 2, "No se obtuvo el resultado deseado");
            Assert.IsNotNull(resultado.ObjetoRespuesta[0].idProperty);
            //Assert.AreEqual(1, resultado.Datos.Count);
        }

        [Test]
        public async Task GetAllPropertiesForProperty()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/Inmobiliaria/ConsultaPropiedadesPorFiltro?idProperty=2");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            RespuestaServicio<List<PropertyEntidad>> resultado = Newtonsoft.Json.JsonConvert.DeserializeObject<RespuestaServicio<List<PropertyEntidad>>>(result);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(true, resultado.Realizado, "Se obtuvo resultado");
            Assert.IsNull(resultado.Mensajes);
            Assert.IsNotNull(resultado.ObjetoRespuesta);
            Assert.AreEqual(1, resultado.ObjetoRespuesta[0].IdOwner, "No se obtuvo el resultado deseado");
            Assert.IsNotNull(resultado.ObjetoRespuesta[0].idProperty);
            Assert.AreEqual(1, resultado.ObjetoRespuesta.Count);
        }

        [Test]
        public async Task GetAllPropertiesForYear()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/Inmobiliaria/ConsultaPropiedadesPorFiltro?year=2001");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            RespuestaServicio<List<PropertyEntidad>> resultado = Newtonsoft.Json.JsonConvert.DeserializeObject<RespuestaServicio<List<PropertyEntidad>>>(result);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(true, resultado.Realizado, "Se obtuvo resultado");
            Assert.IsNull(resultado.Mensajes);
            Assert.IsNotNull(resultado.ObjetoRespuesta);
            Assert.AreEqual(true, resultado.ObjetoRespuesta[0].IdOwner == 1, "No se obtuvo el resultado deseado");
            Assert.IsNotNull(resultado.ObjetoRespuesta[0].idProperty);
            //Assert.AreEqual(1, resultado.Datos.Count);
        }

        [Test]
        public async Task InsertarProperty()
        {
            var client = _factory.CreateClient();

            PropertyEntidad property = new PropertyEntidad
            {
                Addres = "Calle 654",
                CodeInternal = "52564",
                IdOwner = 1,
                NameProperty = "Casa nueva",
                Price = 258000000,
                YearProperty = 2001
            };
            var content = new StringContent(JsonConvert.SerializeObject(property), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Inmobiliaria/RegistrarProperty", content);
            var result = await response.Content.ReadAsStringAsync();
            RespuestaServicio<PropertyEntidad> resultado = Newtonsoft.Json.JsonConvert.DeserializeObject<RespuestaServicio<PropertyEntidad>>(result);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(true, resultado.Realizado, "No se realizó la creación de la propiedad");
        }

        [Test]
        public async Task ActualizarProperty()
        {
            var client = _factory.CreateClient();

            PropertyEntidad property = new PropertyEntidad
            {
                Addres = "Calle siempre viva",
                CodeInternal = "856456",
                NameProperty = "Prueba Casa",
                Price = 258000000,
                YearProperty = 2001,
                idProperty = 5,
                IdOwner = 2
            };
            var content = new StringContent(JsonConvert.SerializeObject(property), Encoding.UTF8, "application/json");

            //var response = await client.PostAsync("/api/Inmobiliaria/ActualizarProperty", content);
            var response = await client.PutAsync("/api/Inmobiliaria/ActualizarProperty", content);
            var result = await response.Content.ReadAsStringAsync();
            RespuestaServicio<PropertyEntidad> resultado = Newtonsoft.Json.JsonConvert.DeserializeObject<RespuestaServicio<PropertyEntidad>>(result);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(true, resultado.Realizado, "No se realizó la actualización de la propiedad");
        }

    }
}