using Accounts.API.Services.Web;
using Channels.API.Services;
using GlobalExceptionHandler.WebApi;
using Messages.API.Services.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Lacqr
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Account/Login/";
                options.Events.OnRedirectToLogin = (context) =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsDevPolicy", builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });
            services.AddTransient<AccountsManagerWeb>();
            services.AddTransient<MessagesManagerWeb>();
            services.AddTransient<ChannelsManager>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("CorsDevPolicy");
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();

            

            app.UseExceptionHandler().WithConventions(o =>
            {
                o.ForException<Exception>().ReturnStatusCode((int)HttpStatusCode.BadRequest);
                o.ContentType = "application/json";
                o.MessageFormatter(exception => JsonConvert.SerializeObject(new
                {
                    error = new
                    {
                        message = exception.Message,
                        statusCode = 400
                    },
                    exception
                }));
            });

            app.UseMvc();
            
        }
    }
}
