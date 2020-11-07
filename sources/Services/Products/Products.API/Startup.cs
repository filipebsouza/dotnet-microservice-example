using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Askmethat.Aspnet.JsonLocalizer.Extensions;
using Askmethat.Aspnet.JsonLocalizer.Localizer;
using Base.API.Filters;
using Base.Domain.Dtos;
using Base.Resources.Notifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Products.API.Infra.Contexts;
using Products.API.Infra.IoC;
using Products.API.Resources.Notifications;

namespace Products.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var teste = Configuration.GetSection<DevelopmentEnvDto>("DevelopmentEnv");

            AppSettings = new AppSettingsDto();
        }

        public IConfiguration Configuration { get; }
        public AppSettingsDto AppSettings { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(CultureFilter));
                options.Filters.Add(typeof(NotificationFilter));
            });

            services.AddSingleton<IStringLocalizerFactory, Base.Resources.Notifications.JsonStringLocalizerFactory>();
            services.AddSingleton<IStringLocalizer>(service => new JsonStringLocalizer("Resources/Notifications/Product"));
            services.AddLocalization(options => options.ResourcesPath = "Resources/Notifications");

            var teste = Configuration["DevelopmentEnv"];

            services.AddDbContext<ProductContext>(opt =>
                {
                    opt.UseInMemoryDatabase("ProductDB");
                    opt.EnableDetailedErrors();
                });

            ContextsServiceResolver.AddServices(services);
            DomainsServiceResolver.AddServices(services);
            QueriesServiceResolver.AddServices(services);

            services.AddScoped(typeof(IBaseNotificationsContext), typeof(BaseNotificationsContext));
            services.AddScoped(typeof(IProductNotifications), typeof(ProductNotifications));

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
