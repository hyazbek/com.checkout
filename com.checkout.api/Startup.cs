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

        private IConfiguration _configuration { get; }

        #endregion

        #region Constuctor

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        /// <summary>
        /// Called by the runtime to Add System Services to the container
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions
                .ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve);
            //services.AddSystemServices();
            services.AddScoped<EFRepository>();
            services.AddScoped<RepositoryService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IMerchantService, MerchantService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IBankService, BankService>();
            services.AddDbContext<CKODBContext>(options =>
             options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CKO API", Version = "v1" });
            });


        }

        /// <summary>
        /// Called by the runtime to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CKO API v1"));
            }
            //var context = serviceProvider.GetService<CKODBContext>();
            //context.Database.EnsureDeleted();
            //context.LoadTestData(context);

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