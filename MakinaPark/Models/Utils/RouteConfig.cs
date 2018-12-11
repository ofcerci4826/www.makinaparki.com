﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;


namespace MakinaPark.Models.Utils
{
    public class RouteConfig
    {
        public static IRouteBuilder Use(IRouteBuilder routes)
        {
            //eg sample for defining Custom route
            //routeBuilder.MapRoute("blog", "blog",
            //    defaults: new { controller = "Home", action = "Index" });

            routes.MapRoute(
                  name: "KiralikKategorilerAltKiralik",
                  template: "Kategoriler-Alt-Kiralik/{slug?}",
                  defaults: new { controller = "Kategori", action = "KategoriAltKiralik" });
            routes.MapRoute(
              name: "KiralikKategorilerAltSatilik",
              template: "Kategoriler-Alt-Satilik/{slug?}",
              defaults: new { controller = "Kategori", action = "KategoriAltSatilik" });
            routes.MapRoute(
                name: "KiralikKategoriler",
                template: "Kategoriler-Kiralik",
                defaults: new { controller = "Kategori", action = "Kiralik" });
            routes.MapRoute(
                name: "SatilikKategoriler",
                template: "Kategoriler-Satilik",
                defaults: new { controller = "Kategori", action = "Satilik" });

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
            
            return routes;
        }


    }
}