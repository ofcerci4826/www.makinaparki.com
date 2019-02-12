using MakinaPark.Models;
using MakinaPark.Models.Utils;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Vegatro.NetCore;
using Vegatro.NetCore.Filters;

namespace MakinaPark.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string eposta, string parola)
        {
            if (string.IsNullOrEmpty(eposta)
                 || string.IsNullOrEmpty(parola))
                return Content(AppResponse.Return(400, ""));

            Kullanici kullanici = Kullanici.Giris(eposta, parola, Platform.WEB, "");

            if (string.IsNullOrEmpty(kullanici.Token))
                return Content(AppResponse.Return(297, "Eposta adresi veya parola hatalı"));

            //return Content(AppResponse.Return(200, kullanici));

            return RedirectToAction("Ozet","User");
        }

        [HttpGet]
        public IActionResult Cikis()
        {
            HttpContext.Session.Remove("Kullanici");

            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(Kullanici param)
        {
            if (!param.IsValid())
                return Content(AppResponse.Return(400));
            var app = AppResponse.Return(Kullanici.Olustur(param));
            string[] dizi = app.Split();
            //return RedirectToAction("Login", "User",new {eposta="sdf",parola= });
            string jsonString = app;
            Result obj = JsonDeserialize<Result>(jsonString);

            if (obj.Status != "200")
            {
                return Content(AppResponse.Return(int.Parse(obj.Status)));
            }

            Kullanici kullanici = Kullanici.Giris(param.Eposta, param.Parola, Platform.WEB, "");
            if (string.IsNullOrEmpty(kullanici.Token))
                return Content(AppResponse.Return(297, "Eposta adresi veya parola hatalı"));

            //return RedirectToAction("Login", "User", new { eposta = obj.Eposta, parola = obj.Parola});
            //return Content(app);
            return RedirectToAction("Index", "Home");
        }
        private static T JsonDeserialize<T>(string jsonString)
        {
            MemoryStream stream1 = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            T obj = (T)ser.ReadObject(stream1);
            return obj;

        }
        [AuthControl]
        [HttpGet]
        public IActionResult Account()
        {
            return View();
        }


        [AuthControl]
        [HttpGet]
        public IActionResult Favorilerim()
        {
            return View();
        }

        [AuthControl]
        [HttpGet]
        public IActionResult Ozet()
        {
            return View();
        }

        [AuthControl]
        [HttpPost]
        public IActionResult BilgilerimiGuncelle(string  AdSoyad, string  Eposta, string  Telefon)
        {
            return Content(AppResponse.Return(Kullanici.BilgilerimiGuncelle(AdSoyad, Eposta, Telefon)));


        }
    }
}
