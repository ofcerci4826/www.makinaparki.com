using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakinaPark.Models;
using Microsoft.AspNetCore.Mvc;

namespace MakinaPark.Controllers
{
    public class MakinaController : Controller
    {
        public IActionResult Kiralik()
        {
            //var makinaListesi = Makina.Listesi();

            return View();
        }

        public IActionResult KiralikDetay(string slug)
        {
            var makina = Makina.Detay(slug);

            return View(makina);
        }

        public IActionResult IkinciEl()
        {
            return View();
        }

        public IActionResult Parkim()
        {
            return View();
        }


        public IActionResult Ekle()
        {
            return View();
        }
    }
}