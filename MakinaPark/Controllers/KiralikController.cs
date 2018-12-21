using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vegatro.NetCore.Filters;

namespace MakinaPark.Controllers
{
    public class KiralikController : Controller
    {
        public IActionResult Makina()
        {
            return View();
        }

        [AuthControl]
        [HttpGet]
        public IActionResult KiralikMakinaTalepOlustur()
        {
            return View();
        }
    }
}