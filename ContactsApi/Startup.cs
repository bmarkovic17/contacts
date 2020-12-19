using System;
using System.IO;
using System.Reflection;
using ContactsApi.Database;
using ContactsApi.Repositories.Implementations;
using ContactsApi.Repositories.Interfaces;
using ContactsApi.Services.Implementations;
using ContactsApi.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ContactsApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Contacts API",
                    Version = "v1",
                    Description = "REST API for contacts administration",
                    Contact = new OpenApiContact
                    {
                        Name = "Boris Marković",
                        Email = "bmarkovic17@outlook.com"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDbContext<ContactContext>();

            services.AddScoped<IContactsService, ContactsService>();
            services.AddScoped<IContactsRepository, ContactsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ContactContext contactContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
            }

            contactContext.Database.Migrate();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
