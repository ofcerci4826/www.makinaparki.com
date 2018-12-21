using MakinaPark.Models;
using MakinaPark.Models.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vegatro.NetCore;
using Vegatro.NetCore.Filters;

namespace MakinaPark.Controllers
{
    public class GenelController : Controller
    {
 

        [HttpPost]
        public IActionResult IlListesi()
        {

         


            return Content(AppResponse.Return(200, Sehir.Listele()));

            //return RedirectToAction("Ozet", "User");
        }
    }
}