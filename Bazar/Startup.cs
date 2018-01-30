using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;

using Bazar.Models.Repository;
using Bazar.Infrastructure;

namespace Bazar
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("Data:Bazar"));

            services.AddMvc();

            //app database
            services.AddDbContext<EfDbContext>(
                options => options.UseSqlServer(Configuration["Data:Bazar:ConnectionString"],
                b => b.MigrationsAssembly("Bazar")));
                
            //identity database
            services.AddDbContext<AppIdentityDbContext>(
                options => options.UseSqlServer(Configuration["Data:BazarIdentity:ConnectionString"],
                b => b.MigrationsAssembly("Bazar")));
            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(120);

                options.Cookies.ApplicationCookie.CookieName = "Bazar";
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                options.Cookies.ApplicationCookie.LoginPath = "/Account/Login";
                options.Cookies.ApplicationCookie.AutomaticAuthenticate = true;
            })
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddSession();

            //repositories
            services.AddTransient<SlideRepository>();
            services.AddTransient<ProductRepository>();
            services.AddTransient<ArticleRepository>();
            services.AddTransient<OrderRepository>();
            services.AddTransient<RestoranRepository>();

            //Mailing services
            services.AddTransient<EmailSettings>();
            services.AddTransient<EmailProcessor>();
            services.AddScoped<IViewRenderService, ViewRenderService>();

            services.AddRouting(options => options.LowercaseUrls = true);

            //Encoding symbols
            services.AddSingleton(HtmlEncoder.Create(allowedRanges: new[] { System.Text.Unicode.UnicodeRanges.All }));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();

            app.UseIdentity();
            app.UseSession();
            app.UseStaticFiles();

            app.UseMvc(routes => {

                routes.MapRoute(
                    name: null,
                    template: "blog/{linkCategory}/{linkUrl}",
                    defaults: new { controller = "Blog", action = "View" });

                routes.MapRoute(
                    name: "Product",
                    template: "product/{linkUrl}",
                    defaults: new { controller = "Product", action = "Index" });

                routes.MapRoute(
                    name: "BlogCategory",
                    template: "blog/{linkCategory}",
                    defaults: new { controller = "Blog", action = "Index" });
                   
                routes.MapRoute(
                    name: null,
                    template: "{area:exists}/{controller=Home}/{action=Index}");

                routes.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}",
                    defaults: new { controller = "Home", action = "Index"});

            });

            // Migrate and seed the database during startup.
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                serviceScope.ServiceProvider.GetService<EfDbContext>().Database.Migrate();
                serviceScope.ServiceProvider.GetService<AppIdentityDbContext>().Database.Migrate();
            }

            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
