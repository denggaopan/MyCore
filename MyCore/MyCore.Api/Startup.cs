using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyCore.Entities;
using Microsoft.EntityFrameworkCore;
using MyCore.Repositories;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using MyCore.Api.Dtos.Language;

namespace MyCore.Api
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
            #region swagger
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info() { Title = "My Api", Version = "v1" });

                config.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "请输入带有Bearer的Token",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                config.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", Enumerable.Empty<string>()}
                });
            });
            #endregion

            #region AutoMapper
            Mapper.Initialize(config=>
            {
                config.CreateMap<Language, LanguageDto>();
                config.CreateMap<LanguageCreationDto, Language>();
            });
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //数据库连接
            var connectionString = Configuration["ConnectionStrings:Default"];
            services.AddDbContext<ApplicationDbContext>(o=>o.UseMySql(connectionString));

            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "v1 docs");
            });

            app.UseMvc();
        }
    }
}
