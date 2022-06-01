using System.Threading.Tasks;
using com.checkout.application.Interfaces;
using com.checkout.application.services;
using com.checkout.data;
using com.checkout.data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Newtonsoft.Json;

namespace com.checkout.api
{
    public class Startup_old
    {

        #region Properties

        private IConfiguration _configuration { get; }

        #endregion

        #region Constuctor

        public Startup_old(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSystemServices();

            services.AddDbContext<CKODBContext>(options =>
            {
                options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=CKODB;Trusted_Connection = True; MultipleActiveResultSets = true");
            });
            //services.AddDbContext<CKODBContext>(options =>
            //{
            //    options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            //});
        }

        /// <summary>
        /// Called by the runtime to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Checkout API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}