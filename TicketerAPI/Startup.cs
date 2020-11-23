using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketerAPI.Data;
using TicketerAPI.Models;
using Newtonsoft.Json.Serialization;

namespace TicketerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {      
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<TicketerContext>(opt => {
               opt.UseSqlServer(Configuration.GetConnectionString("awsConnection"));
            });         

            services.AddScoped<IUsuarioRepo, UsuarioRepo>();
            services.AddScoped<IServicioRepo, ServicioRepo>();
            services.AddScoped<IRolRepo, RolRepo>();
            services.AddScoped<ITicketRepo, TicketRepo>();
            services.AddScoped<ITicketPrioridadRepo, TicketPrioridadRepo>();
            services.AddScoped<ITicketStatusRepo, TicketStatusRepo>();
            services.AddScoped<IClienteRepo, ClienteRepo>();
            
            services.AddCors(options => {
               options.AddPolicy("default", builder => {
                   builder.AllowAnyOrigin();
                   builder.AllowAnyMethod();
                   builder.AllowAnyHeader();
               });
            });

            services
                .AddControllers()
                .AddNewtonsoftJson(s => {
                    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
            
            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "TicketerAPI", Version = "v1" });
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicketerAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors("default");

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
