using GerenciadorDeBiblioteca.Data;
using GerenciadorDeBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Diagnostics;

namespace GerenciadorDeBiblioteca.Controllers
{
    public class HomeController : Controller
    {

        private readonly GerenciadorDeBibliotecaContext _context;

        public HomeController(GerenciadorDeBibliotecaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if(TempData["Name"] != null)
            {
                return View(TempData);
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

        public IActionResult Login()
        {
            
            return View();
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string LoginPass, string LoginUser)
        {
        
            if (ModelState.IsValid)
                {
                    var f_password = GetMD5(LoginPass);
                    var data = _context.User.Where(s => s.LoginUser.Equals(LoginUser) && s.LoginPass.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    TempData["Name"] = data.FirstOrDefault().Name;
                    TempData["User"] = data.FirstOrDefault().LoginUser;
                    TempData["idUsuario"] = data.FirstOrDefault().Id;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErroLogin"] = "Usuário ou Senha não encontrado!";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}