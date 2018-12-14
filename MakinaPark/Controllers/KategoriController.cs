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
        public IActionResult Detay(string detay)
        {
            ViewData["Title"] = detay;
            ViewData["Baslik"] = detay;
            string detaylink = "";
            if (detay=="Kiralık")
            {
                detaylink = "Kiralik";
            }
            ViewData["detay"] = detaylink;
            var kategoriListesi = KategoriModel.Listesi();

            return View(kategoriListesi);
        }

        //public IActionResult Satilik()
        //{
        //    var kategoriListesi = KategoriModel.Listesi();

        //    return View(kategoriListesi);
        //}

        public IActionResult KategoriAlt(string tip,string slug)
        {
            ViewData["detay"] = tip;
            var kategoriAlt = new object();
            if (tip=="Satilik")
            {
                 kategoriAlt = KategoriModel.KategoriAltSatilikList(slug);
            }
            else if (tip=="Kiralik")
            {
                 kategoriAlt = KategoriModel.KategoriAltKiralikList(slug);
            } 
            return View("Detay",kategoriAlt);
        }

        public IActionResult KategoriMarka(string tip, string slug,string ustSlug)
        {
            ViewData["detay"] = tip;
            var kategoriAltMarka = new object();
            if (tip == "Satilik")
            {
                kategoriAltMarka = KategoriModel.KategoriMarkaSatilikList(slug, ustSlug);
            }
            else if (tip == "Kiralik")
            {
                kategoriAltMarka = KategoriModel.KategoriMarkaKiralikList(slug, ustSlug);
            }
             
            return View("Detay", kategoriAltMarka);
        }

        public IActionResult KategoriMarkaModel(string slug, string ustSlug)
        {
             
            var kategoriAlt = KategoriModel.KategoriMarkaKiralikList(slug, ustSlug);

            return View("Detay", kategoriAlt);
        }




        public IActionResult KategoriMarkaModel(string slug)
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
