// <copyright file="Startup.cs" company="MNX Global Logistics">
// Copyright (c) MNX Global Logistics. All rights reserved.
// </copyright>
// <summary>Startup Calss.</summary>
namespace DevOps.Api
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using MNX.ConnectEcho.Common.Models;
    using MNX.ConnectEcho.Common.Models.Dto;
    using Serilog;

    /// <summary>
    /// Startup cs file : Executs first when we run the application.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        /// <summary>
        /// Gets initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">set of application configuration properties.</param>
        /// <value>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">set of application configuration properties.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">service collection params.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                             .AllowAnyMethod()
                             .AllowAnyHeader();
            }));

            services.AddMvc(o =>
            {
            }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PagingDto>(lifetime: ServiceLifetime.Scoped)).ConfigureApiBehaviorOptions(options =>
            {
            });

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddControllers();

            services.AddLogging(logBuilder => logBuilder.AddSerilog(dispose: true));

            var appMessages = Configuration.GetSection("Messages").Get<Dictionary<string, ErrorCodeMessageVM>>();
            services.AddSingleton<AppMessages>(s => new AppMessages(appMessages));
            services.AddControllers();
            services.AddSwaggerGen();
            services.ResolveDependency();

            services.AddSwaggerGen(config =>
            {
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                       {
                         new OpenApiSecurityScheme
                         {
                           Reference = new OpenApiReference
                           {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer",
                           },
                         },
                         new string[] { }
                       },
                });
            });

            // ===== Add Jwt Authentication ========
            AddJWTAuthentication(services);
        }

        /// <summary>
        /// JWT Authentication Logic.
        /// </summary>
        /// <param name="services">collection of Services.</param>
        private void AddJWTAuthentication(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = "mnx",
                        ValidAudience = "mnx",
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"])),
                        ClockSkew = TimeSpan.Zero, // remove delay of token when expire
                    };
                });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">class for application request.</param>
        /// <param name="env">provides information about web hosting environment.</param>
        /// <param name="loggerFactory">ILoggerFactory.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddSerilog();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "DevOps.Api V1");
            });

            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(x => x
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .SetIsOriginAllowed(origin => true) // allow any origin
                      .AllowCredentials());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
