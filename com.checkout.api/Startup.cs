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
    public class Startup
    {
        #region Properties

        private IConfiguration Configuration { get; }

        #endregion

        #region Constuctor

        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        #endregion

        /// <summary>
        /// Called by the runtime to Add System Services to the container
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();

            services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions
                .ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve);

            services.AddScoped<EFRepository>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IMerchantService, MerchantService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IBankService, BankService>();

            services.AddDbContext<CKODBContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


        }

        /// <summary>
        /// Called by the runtime to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));


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