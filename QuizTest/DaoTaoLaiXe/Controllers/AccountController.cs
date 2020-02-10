using DaoTaoLaiXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace DaoTaoLaiXe.Controllers
{
    public class AccountController : Controller
    {
        QuizDBEntities db = new QuizDBEntities();

        [Route("login")]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            Session["UserLogged"] = db.QuanTriViens.FirstOrDefault(x => x.TaiKhoan == loginViewModel.UserName && x.MatKhau == loginViewModel.Password);
            if (Session["UserLogged"] == null)
            {
                ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu!");
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }
            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("logoff")]
        public ActionResult LogOff()
        {
            Session["UserLogged"] = null;
            return RedirectToAction("Login", "Account", new { area = "" });
        }
    }
}