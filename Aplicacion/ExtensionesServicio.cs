using Aplicacion.Interfaces;
using Aplicacion.Interfaces.Repositorio;
using Aplicacion.Servicios;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public static class  ExtensionesServicio
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IRepositorioService, RepositorioServicio>();
         
        }
    }
}
