using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RLS.PortfolioGeneration.NotificationService.Repositories;
using RLS.PortfolioGeneration.NotificationService.SignalR;
using Swashbuckle.AspNetCore.Swagger;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.PlatformAbstractions;

namespace RLS.PortfolioGeneration.NotificationService
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
            services.AddMvc();
            services.AddCors(service =>
            {
                service.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "TPI Flow Notification Service", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "RLS.PortfolioGeneration.NotificationService.xml");
                c.IncludeXmlComments(xmlPath);
            });

            var appConfig = new NotificationServiceConfiguration();
            Configuration.Bind(appConfig);
            

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(appConfig.Auth0SecretKey)),
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = appConfig.Auth0Tenant,
                    ValidateIssuer = true,
                    
                    ValidAudience = appConfig.Auth0ClientId,
                    ValidateAudience = true,
                    
                    LifetimeValidator = (before, expires, token, param) => expires > DateTime.UtcNow,
                    ValidateLifetime = true
                };

                // We have to hook the OnMessageReceived event in order to allow the JWT authentication handler to read the access
                // token from the query string when a WebSocket or Server-Sent Events request comes in.
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/NotificationServiceHub")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            // Used by SignalR to determine the user id from the claims provided in the token
            services.AddSingleton<IUserIdProvider, NameIdentifierUserIdProvider>();

            services.AddSingleton<InMemoryNotificationRepository>();
            services.Configure<NotificationServiceConfiguration>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            
            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationServiceHub>("/NotificationServiceHub");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TPI Flow Notification Service v1");
            });
        }
    }
}
