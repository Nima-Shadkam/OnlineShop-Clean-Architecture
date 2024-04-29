using Infrastructure.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace API.Installers
{
    public static class DIInstaller
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("API",
                  builder =>
                  {
                      builder.WithOrigins("*");
                      builder.WithHeaders("*");
                      builder.WithMethods("*");
                  });
            });

            return services;
        }



        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "OnlineShop App API",
                    Description = "OnlineShop App API - Version01",
                    //TermsOfService = new Uri(""),
                    License = new OpenApiLicense
                    {
                        Name = "OnlineShop",
                        //Url = new Uri(""),
                    }
                });

               

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            return services;
        }
    }
}
