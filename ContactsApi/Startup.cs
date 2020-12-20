using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using ContactsApi.Database;
using ContactsApi.Repositories.Implementations;
using ContactsApi.Repositories.Interfaces;
using ContactsApi.Services.Implementations;
using ContactsApi.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ContactsApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) =>
            _configuration = configuration;

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

            services.AddDbContext<AddressBookContext>(dbContextOptionsBuilder =>
                dbContextOptionsBuilder
                    .UseNpgsql(_configuration.GetConnectionString("DbConnection"))
                    .UseSnakeCaseNamingConvention());

            services.AddScoped<IAddressBookDatabase, AddressBookDatabase>();
            services.AddScoped<IContactsRepository, ContactsRepository>();
            services.AddScoped<IContactDataRepository, ContactDataRepository>();
            services.AddScoped<IAddressBookService, AddressBookService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AddressBookContext addressBookContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
            }

            addressBookContext.Database.Migrate();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
