using MakinaPark.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakinaPark.Controllers
{
    public class KategoriController : Controller
    {
        public IActionResult Kiralik()
        {
            var kategoriListesi = KategoriModel.Listesi();

            return View(kategoriListesi);
        }

        public IActionResult Satilik()
        {
            var kategoriListesi = KategoriModel.Listesi();

            return View(kategoriListesi);
        }

        public IActionResult KategoriAltKiralik(string slug)
        {
            var kategoriAlt = KategoriModel.KategoriAltKiralikList(slug);

            return View("Kiralik",kategoriAlt);
        }

        public IActionResult KategoriMarkaKiralik(string slug,string ustSlug)
        {
            var kategoriAlt = KategoriModel.KategoriMarkaKiralikList(slug, ustSlug);

            return View("Kiralik", kategoriAlt);
        }






        public IActionResult KategoriAltSatilik(string slug)
        {
            //var kategoriAlt = KategoriModel.Detay(slug);
            //kategoriAlt
            return View();
        }

        public IActionResult IkinciEl()
        {
            return View();
        }
    }
}
