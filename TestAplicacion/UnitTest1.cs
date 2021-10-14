using AccesoDatos;
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
                        services.AddScoped<IRepositorio, Repositorio>();

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
            //Assert.AreEqual(1, resultado.Datos.Count);
        }
    }
}