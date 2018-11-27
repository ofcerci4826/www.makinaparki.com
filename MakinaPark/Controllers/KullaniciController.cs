using MakinaPark.Models;
using Microsoft.AspNetCore.Mvc;
using MakinaPark.Models.Utils;
using Vegatro.NetCore;

namespace MakinaPark.Controllers
{
    public class KullaniciController : Controller
    {
        [HttpGet]
        public IActionResult Cikis()
        {
            HttpContext.Session.Remove("Kullanici");

            return RedirectToAction("Giris", "Kullanici");
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Giris(string eposta, string parola, string token)
        {
            if (string.IsNullOrEmpty(eposta)
                 || string.IsNullOrEmpty(parola))
                return Content(AppResponse.Return(400, ""));

            Kullanici kullanici = Kullanici.Giris(eposta, parola, Platform.WEB, token);

            if (string.IsNullOrEmpty(kullanici.Token))
                return Content(AppResponse.Return(297, "Eposta adresi veya parola hatalı"));

            return Content(AppResponse.Return(200, kullanici));
        }

    }
}