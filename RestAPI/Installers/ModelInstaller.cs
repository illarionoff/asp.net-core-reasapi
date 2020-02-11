using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace RestAPI.Installers
{
    public class ModelInstaller: IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSwaggerGen(x => x.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }));
        }
    }
}
