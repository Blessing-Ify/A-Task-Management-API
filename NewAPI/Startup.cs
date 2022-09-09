using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NewAPI.ConfigExtention;
using NewAPI.Data;
using NewAPI.Model;
using NewAPI.Security;
using NewAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAPI
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
            services.AddControllers();
            services.AddDbContext<APIContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConn")));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<APIContext>();
            services.AddScoped<IJWTSecurity, JWTSecurity>();
            services.AddScoped<IUserService, UserService>();
            services.AddAutoMapper();
            
            //calling the method of the registereg configsettings that we created a class for just to avoid lengthiness 
            ConfigSettings.ConfigureSwagger(services);

            // for authentication own
            ConfigSettings.ConfigureAuthentication(services, Configuration);


            services.AddAuthorization(config =>
            {
                config.AddPolicy("DecaDevRole", policy => policy.RequireRole("Decadev"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API"));
        }
    }
}
