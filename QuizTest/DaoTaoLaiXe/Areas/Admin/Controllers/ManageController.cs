using DaoTaoLaiXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaoTaoLaiXe.Areas.Admin.Controllers
{
    [AuthorizeFilter]
    public class ManageController : Controller
    {
        QuizDBEntities db = new QuizDBEntities();

        // GET: Admin/Manage
        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            QuanTriVien quanTriVien = Session["UserLogged"] as QuanTriVien;
            if (changePasswordViewModel.OldPassword != quanTriVien.MatKhau)
            {
                ModelState.AddModelError("OldPassword", "Mật khẩu hiện tại không đúng!");
            }
            if (ModelState.IsValid)
            {
                quanTriVien.MatKhau = changePasswordViewModel.NewPassword;
                db.Entry(quanTriVien).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                Session["UserLogged"] = quanTriVien;
                return RedirectToAction("Index", "Dashboard");
            }
            return View(changePasswordViewModel);
        }
    }
}