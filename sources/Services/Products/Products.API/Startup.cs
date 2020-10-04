using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Askmethat.Aspnet.JsonLocalizer.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

        JsonLocalizationOptions _jsonLocalizationOptions;
        List<CultureInfo> _supportedCultures;
        RequestCulture _defaultRequestCulture;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            ContextsServiceResolver.AddServices(services);
            DomainsServiceResolver.AddServices(services);
            QueriesServiceResolver.AddServices(services);

            services.AddDbContext<ProductContext>(opt => opt.UseInMemoryDatabase("ProductDB"));

            var jsonLocalizationOptions = Configuration.GetSection(nameof(JsonLocalizationOptions));

            _jsonLocalizationOptions = jsonLocalizationOptions.Get<JsonLocalizationOptions>();
            _defaultRequestCulture = new RequestCulture(_jsonLocalizationOptions.DefaultCulture,
                _jsonLocalizationOptions.DefaultUICulture);
            _supportedCultures = _jsonLocalizationOptions.SupportedCultureInfos.ToList();

            _ = services.AddJsonLocalization(options =>
            {
                options.ResourcesPath = _jsonLocalizationOptions.ResourcesPath;
                options.UseBaseName = _jsonLocalizationOptions.UseBaseName;
                options.CacheDuration = _jsonLocalizationOptions.CacheDuration;
                options.SupportedCultureInfos = _jsonLocalizationOptions.SupportedCultureInfos;
                options.FileEncoding = _jsonLocalizationOptions.FileEncoding;
                options.IsAbsolutePath = _jsonLocalizationOptions.IsAbsolutePath;
            });

            _ = services.Configure<RequestLocalizationOptions>(options =>
           {
               options.DefaultRequestCulture = _defaultRequestCulture;
               // Formatting numbers, dates, etc.
               options.SupportedCultures = _supportedCultures;
               // UI strings that we have localized.
               options.SupportedUICultures = _supportedCultures;
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            _ = app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = _defaultRequestCulture,
                // Formatting numbers, dates, etc.
                SupportedCultures = _supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = _supportedCultures
            });

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
