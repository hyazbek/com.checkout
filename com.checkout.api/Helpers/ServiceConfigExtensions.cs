using System;
using com.checkout.application.Interfaces;
using com.checkout.application.services;
using com.checkout.data.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace com.checkout.api

{
    public static class ServiceConfigExtensions
    {
        public static IServiceCollection AddSystemServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Checkout API", 
                    Version = "v1",
                    Description = "ASP.Net Core 3 API for the Checkout challenge",
                    Contact = new OpenApiContact
                    {
                        Name = "Chad Bonthuys",
                        Email = "chad@atomixdev.com",
                        Url = new Uri("https://www.linkedin.com/in/chadbonthuys/"),
                    },
                });
            });

            services.AddScoped<EFRepository>();
            services.AddScoped<RepositoryService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IMerchantService,MerchantService>();
            services.AddScoped<ITransactionService, TransactionService>();
            ;

            return services;
        }
    }
}