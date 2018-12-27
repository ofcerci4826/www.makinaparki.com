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

        }

        [HttpPost]
        public IActionResult IlceListesi(int refIl)
        {
            return Content(AppResponse.Return(200, Ilce.Listele(refIl)));

        }

        [HttpPost]
        public IActionResult VergiDairesiListesi(int refIl)
        {
            return Content(AppResponse.Return(200, VergiDairesi.Listele(refIl)));

        }

    }
}