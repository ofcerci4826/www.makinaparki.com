using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Vegatro.Core;
using Vegatro.NetCore.Utils;

namespace MakinaPark
{
    public class Startup
    {
        private IConfigurationRoot ConfigurationStatic;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Kayıt işlemimizi gerçekleştiriyoruz, AddMvc() den önce eklediğinizden emin olunuz.
            services.AddLocalization(options =>
            {
                // Resource (kaynak) dosyalarımızı ana dizin altında "Resources" klasorü içerisinde tutacağımızı belirtiyoruz.
                options.ResourcesPath = "Resources";
            });

            services.Configure<RequestLocalizationOptions>(
            opts =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                          new CultureInfo("tr-TR"),
                          new CultureInfo("en-US")
                };
            
                opts.DefaultRequestCulture = new RequestCulture("tr-TR");
                opts.DefaultRequestCulture.Culture.NumberFormat.NumberDecimalSeparator = ",";
                opts.DefaultRequestCulture.Culture.DateTimeFormat.DateSeparator = ".";
                opts.DefaultRequestCulture.Culture.NumberFormat.NumberGroupSeparator = ".";
                opts.DefaultRequestCulture.Culture.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
                // Formatting numbers, dates, etc.
                opts.SupportedCultures = supportedCultures;
                // UI strings that we have localized.
                opts.SupportedUICultures = supportedCultures;
            });
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.Configure<FormOptions>(x => x.ValueCountLimit = 2048);
            services.AddMvc()
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                    .AddDataAnnotationsLocalization()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();

            services.AddCors();

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = ".MakinaPark.Session";
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.IdleTimeout = TimeSpan.FromHours(4);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            // STANDART ROUTİNG MEKANİZMASI İLE DİL DESTEĞİ KULLANMAK

            //var supportedCultures = new List<CultureInfo>
            //{
            //    new CultureInfo("tr-TR"),
            //    new CultureInfo("en-US"),
            //};


            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    SupportedCultures = supportedCultures,
            //    SupportedUICultures = supportedCultures,
            //    DefaultRequestCulture = new RequestCulture("tr-TR")
            //});

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            Vegatro.NetCore.Utils.ActionContext.Configure(app.ApplicationServices.GetService<IActionContextAccessor>());
            ContextObject.Configure(app.ApplicationServices.GetService<IHttpContextAccessor>());

            ConfigurationStatic = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            Config.GetFunc = key => ConfigurationStatic[key];

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "kiralik-makinalar",
                    template: "kiralik-makinalar",
                    defaults: new { controller = "Makina", action = "Kiralik" });

                routes.MapRoute(
                   name: "kiralik-makina-detay",
                   template: "kiralik-makinalar/{slug?}",
                   defaults: new { controller = "Makina", action = "KiralikDetay" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
