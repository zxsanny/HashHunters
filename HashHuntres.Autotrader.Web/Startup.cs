using Autofac;
using HashHunters.Autotrader.Core.Interfaces;
using HashHunters.Autotrader.Repository;
using HashHunters.Autotrader.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HashHuntres.Autotrader.Web
{
    public class Startup
    {
        IContainer Container { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddLogging();
            
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ServicesModule());

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
            var connStr = configuration.GetConnectionString("hhadmin");

            builder.RegisterModule(new RepositoryModule(connStr));

            Container = builder.Build();

            var securityService = Container.Resolve<ISecurityService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = securityService.GetTokenValidationParameters());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });


            //var repository = new OHLCRedisRepository();

            //repository.WriteTicker("ADA/BTC", DateTime.Now, 2093);


        }
    }
}
