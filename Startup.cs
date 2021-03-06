using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using my_books.Data;
using my_books.Data.Services;
using my_books.Exceptions;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace my_books
{
    public class Startup
    {
        public string ConnectionStrings { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionStrings = configuration.GetConnectionString("Connection");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(ConnectionStrings));
            services.AddTransient<BooksService>();
            services.AddTransient<PublisherService>();
            services.AddTransient<AuthorService>();
            services.AddTransient<LogsService>();

            // services.AddApiVersioning(); // without default version
            //services.AddApiVersioning(config =>
            //{
            //    // set default version
            //    // this make swagger not work because all route must be uniqe
            //    config.AssumeDefaultVersionWhenUnspecified = true;
            //    config.DefaultApiVersion = new ApiVersion(1, 0);

            //    // set multiple version for one controller and access to action methods by request header
            //    config.ApiVersionReader = new HeaderApiVersionReader("custom-version-header");
            //});

             services.AddApiVersioning(); // without default version
            services.AddApiVersioning(config =>
            {
                // set default version
                // this make swagger not work because all route must be uniqe
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.DefaultApiVersion = new ApiVersion(1, 0);

                // set multiple version for one controller and access to action methods by request header
                config.ApiVersionReader = new HeaderApiVersionReader("custom-version-header");
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "my_books", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "my_books v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.ConfigureBuildInExceptionHandler(loggerFactory);
            //app.ConfigureCustomExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
