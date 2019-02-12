using MakinaPark.Models;
using MakinaPark.Models.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakinaPark.Controllers
{
    public class KategoriController : Controller
    {

        public IActionResult KategoriListele()
        {
            return Content(AppResponse.Return(200, Kategori.Listele()));
        }

        public IActionResult KategoriAltListele(int refKategori)
        {
            return Content(AppResponse.Return(200, KategoriAlt.Listele(refKategori)));
        }

        public IActionResult KategoriMarkaListele(int refKategoriAlt)
        {
            return Content(AppResponse.Return(200, KategoriMarka.Listele(refKategoriAlt)));
        }

        public IActionResult KategoriModelListele(int refKategoriMarka)
        {
            return Content(AppResponse.Return(200, KategoriModel.Listele(refKategoriMarka)));
        }

        public IActionResult Kiralik()
        {
            var kategoriListesi = _KategoriModel.Listesi();

            return View(kategoriListesi);
        }

        public IActionResult Satilik()
        {
            var kategoriListesi = _KategoriModel.Listesi();

            return View(kategoriListesi);
        }

        public IActionResult KategoriAltKiralik(string slug)
        {
            //var kategoriAlt = KategoriModel.Detay(slug);

            //return View(kategoriAlt);
            return View();
        }
        public IActionResult KategoriAltSatilik(string slug)
        {
            //var kategoriAlt = KategoriModel.Detay(slug);

            //return View(kategoriAlt);
            return View();
        }

        public IActionResult IkinciEl()
        {
            return View();
        }
    }
}
