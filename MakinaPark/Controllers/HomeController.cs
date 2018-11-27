using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MakinaPark.Models;
using Vegatro.NetCore.Filters;

namespace MakinaPark.Controllers
{
    public class HomeController : Controller
    {
        //[AuthControl(IdKey = "musteriId", Type = "musteri")]
        public IActionResult Index(long musteriId)
        {
            return View();
        }

        public IActionResult Deneme()
        {
            return View();
        }
  
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
