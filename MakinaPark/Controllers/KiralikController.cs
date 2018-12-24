using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakinaPark.Models;
using MakinaPark.Models.Utils;
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



        [AuthControl]
        [HttpPost]
        public IActionResult KiralikMakinaTalepKaydet(int refIl, int refIlce, int refKategori,
            int refKategoriAlt, int refKategoriMarka, int refKategoriMarkaModel, string Baslik, string Aciklama,
            int refOdemeTipi, string OdemeTipiDiger, int KacAdet, int KiralamaSure, int refKiralamaSureTipi,
            string  IsBaslangic, int refOperatorVarmi)
        {
            return Content(AppResponse.Return(KiralikTalep.KiralikMakinaTalepOlustur(refIl, refIlce, refKategori, refKategoriAlt, refKategoriMarka,
                refKategoriMarkaModel, Baslik, Aciklama, refOdemeTipi, OdemeTipiDiger, KacAdet, KiralamaSure, refKiralamaSureTipi, IsBaslangic, refOperatorVarmi)));


        }

    }
}