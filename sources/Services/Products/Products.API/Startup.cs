using System;
using System.IO;
using System.Reflection;
using Common.API.Filters;
using Common.Resources.Notifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Products.API.Infra.Contexts;
using Products.API.Infra.IoC;

namespace Products.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;            
        }

        public IConfiguration Configuration { get; }        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(Common.API.Filters.CultureFilter));
                options.Filters.Add(typeof(NotificationFilter));
            })
            .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

            services.AddSingleton<IStringLocalizerFactory, Common.Resources.Notifications.JsonStringLocalizerFactory>();
            services.AddSingleton<IStringLocalizer>(service => new JsonStringLocalizer("Resources/Notifications/Product"));
            services.AddLocalization(options => options.ResourcesPath = "Resources/Notifications");

            services.AddDbContext<ProductContext>(opt =>
                {
                    opt.UseInMemoryDatabase("ProductDB");
                    opt.EnableDetailedErrors();
                });

            ContextsServiceResolver.AddServices(services);
            DomainsServiceResolver.AddServices(services);
            QueriesServiceResolver.AddServices(services);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Products API",
                    Description = "Products API",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Filipe Bezerra de Souza",
                        Url = new Uri("https://www.filipe.dev")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStringLocalizer localizer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.RoutePrefix = "swagger";
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Example");
            });
        }
    }
}
