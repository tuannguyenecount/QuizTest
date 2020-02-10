using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaoTaoLaiXe.Models;
namespace DaoTaoLaiXe.Areas.Admin.Controllers
{
    public class SharedController : Controller
    {
        QuizDBEntities db = new QuizDBEntities();

        // GET: Admin/Shared
        public PartialViewResult _SidebarPartial()
        {
            return PartialView(db.ChuyenMucCauHois.OrderBy(x=>x.MaChuyenMuc).ToList());
        }
    }
}