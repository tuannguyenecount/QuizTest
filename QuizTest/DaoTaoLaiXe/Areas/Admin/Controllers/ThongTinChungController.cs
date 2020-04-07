using DaoTaoLaiXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaoTaoLaiXe.Areas.Admin.Controllers
{
    [AuthorizeFilter]
    public class ThongTinChungController : Controller
    {
        QuizDBEntities db = new QuizDBEntities();

        // GET: Admin/ThongTinChung/Edit
        public ActionResult Edit()
        {
            return View(db.ThongTinChungs.First());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ThongTinChung thongTinChung)
        {
            if(ModelState.IsValid)
            {
                db.Entry(thongTinChung).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Redirect("Edit");
            }
            return View(thongTinChung);
        }
    }
}