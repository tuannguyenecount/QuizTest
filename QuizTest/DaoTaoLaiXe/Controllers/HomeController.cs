using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaoTaoLaiXe.Models;
namespace DaoTaoLaiXe.Controllers
{
    public class HomeController : Controller
    {
        QuizDBEntities db = new QuizDBEntities();

        public ActionResult Index()
        {
            List<CauHoi> cauhois = new List<CauHoi>();
            Random random = new Random();

            cauhois.AddRange(db.CauHois.Where(x => x.MaCauHoi >= 1 && x.MaCauHoi <= 21).OrderBy(x=>x.MaCauHoi).Skip(random.Next(0, 20)).Take(1).ToList()); // lấy 1 câu phần 1
            cauhois.AddRange(db.CauHois.Where(x => x.MaCauHoi >= 22 && x.MaCauHoi <= 131).OrderBy(x => x.MaCauHoi).Skip(random.Next(0, 113)).Take(7).ToList()); // lấy 7 câu phần 1
            cauhois.AddRange(db.CauHois.Where(x => x.MaCauHoi >= 132 && x.MaCauHoi <= 145).OrderBy(x => x.MaCauHoi).Skip(random.Next(0, 13)).Take(1).ToList()); // lấy 1 câu phần 1
            cauhois.AddRange(db.CauHois.Where(x => x.MaCauHoi >= 146 && x.MaCauHoi <= 175).OrderBy(x => x.MaCauHoi).Skip(random.Next(0, 29)).Take(1).ToList()); // lấy 1 câu phần 2
            cauhois.AddRange(db.CauHois.Where(x => x.MaCauHoi >= 176 && x.MaCauHoi <= 200).OrderBy(x => x.MaCauHoi).Skip(random.Next(0, 24)).Take(1).ToList()); // lấy 1 câu phần 3
            cauhois.AddRange(db.CauHois.Where(x => x.MaCauHoi >= 201 && x.MaCauHoi <= 255).OrderBy(x => x.MaCauHoi).Skip(random.Next(0, 54)).Take(1).ToList()); // lấy 1 câu phần 4,5
            cauhois.AddRange(db.CauHois.Where(x => x.MaCauHoi >= 256 && x.MaCauHoi <= 355).OrderBy(x => x.MaCauHoi).Skip(random.Next(0, 91)).Take(9).ToList()); // lấy 9 câu phần 6
            cauhois.AddRange(db.CauHois.Where(x => x.MaCauHoi >= 356 && x.MaCauHoi <= 450).OrderBy(x => x.MaCauHoi).Skip(random.Next(0, 86)).Take(9).ToList()); // lấy 9 câu phần 7

            return View(cauhois.OrderBy(x=>x.MaCauHoi).ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}