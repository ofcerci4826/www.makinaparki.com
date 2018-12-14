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
    public class UserController :Controller
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
            return RedirectToAction("Account", "User");
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [AuthControl]
        [HttpGet]
        public IActionResult Account()
        {
            return View();
        }

    }
}
