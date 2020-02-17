using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DaoTaoLaiXe.Models;
namespace DaoTaoLaiXe.Controllers
{
    public class HomeController : Controller
    {
        QuizDBEntities db = new QuizDBEntities();

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Review()
        {
            Dictionary<int, List<int>> cauHoiDapAnDung = new Dictionary<int, List<int>>();
            List<CauHoi> cauHois = db.CauHois.OrderBy(x => x.MaCauHoiMoi).ToList();
            cauHois.ForEach(x =>
            {
                cauHoiDapAnDung.Add(x.MaCauHoiMoi, x.DapAns.Where(y => y.DapAnDung == true).Select(y => y.MaDapAn).ToList());
            });
            ViewBag.cauHoiDapAnDung = cauHoiDapAnDung;
            return View(cauHois);
        }

        public ActionResult Practice()
        {
            List<CauHoi> cauhois = new List<CauHoi>();
            Random random = new Random();

            cauhois.AddRange(db.CauHois.Where(x => x.MaCauHoiMoi >= 1 && x.MaCauHoiMoi <= 21).OrderBy(x => x.MaCauHoiMoi).Skip(random.Next(0, 20)).Take(1).ToList()); // lấy 1 câu phần 1
            cauhois.AddRange(db.CauHois.Where(x => x.MaCauHoiMoi >= 22 && x.MaCauHoiMoi <= 131).OrderBy(x => x.MaCauHoiMoi).Skip(random.Next(0, 109)).Take(7).ToList()); // lấy 7 câu phần 1
            cauhois.AddRange(db.CauHois.Where(x => x.MaCauHoiMoi >= 132 && x.MaCauHoiMoi <= 145).OrderBy(x => x.MaCauHoiMoi).Skip(random.Next(0, 13)).Take(1).ToList()); // lấy 1 câu phần 1
            cauhois.AddRange(db.CauHois.Where(x => x.MaCauHoiMoi >= 146 && x.MaCauHoiMoi <= 175).OrderBy(x => x.MaCauHoiMoi).Skip(random.Next(0, 29)).Take(1).ToList()); // lấy 1 câu phần 2
            cauhois.AddRange(db.CauHois.Where(x => x.MaCauHoiMoi >= 176 && x.MaCauHoiMoi <= 200).OrderBy(x => x.MaCauHoiMoi).Skip(random.Next(0, 24)).Take(1).ToList()); // lấy 1 câu phần 3
            cauhois.AddRange(db.CauHois.Where(x => x.MaCauHoiMoi >= 201 && x.MaCauHoiMoi <= 255).OrderBy(x => x.MaCauHoiMoi).Skip(random.Next(0, 54)).Take(1).ToList()); // lấy 1 câu phần 4,5
            cauhois.AddRange(db.CauHois.Where(x => x.MaCauHoiMoi >= 256 && x.MaCauHoiMoi <= 355).OrderBy(x => x.MaCauHoiMoi).Skip(random.Next(0, 99)).Take(9).ToList()); // lấy 9 câu phần 6
            cauhois.AddRange(db.CauHois.Where(x => x.MaCauHoiMoi >= 356 && x.MaCauHoiMoi <= 450).OrderBy(x => x.MaCauHoiMoi).Skip(random.Next(0, 94)).Take(9).ToList()); // lấy 9 câu phần 7
            cauhois = cauhois.Where(x => x.DapAns != null && x.DapAns.Count > 0 && x.DapAns.Any(y => y.DapAnDung == true)).ToList();

            cauhois = cauhois.OrderBy(x => x.MaCauHoiMoi).ToList();
            Session["CauHois"] = cauhois;

            ViewBag.SelectAnswer = new List<int>();
            ViewBag.danhSachCauHoiTraLoiDung = new List<CauHoi>();
            ViewBag.cauHoiDapAnDung = new Dictionary<int, List<int>>();
            ViewBag.CountAnswer = 0;
            return View(Session["CauHois"]);
        }

        public ActionResult ViewResult()
        {
            return RedirectToAction("Practice");
        }

        [HttpPost]
        public async Task<ActionResult> ViewResult(FormCollection frm)
        {
            List<int> selectAnswer = new List<int>();
            (Session["CauHois"] as List<CauHoi>).ForEach(x =>
            {
                selectAnswer.AddRange(frm["selectAnswer_" + x.MaCauHoiMoi].Split(',').Select(y => int.Parse(y.Trim())).ToList());
            });

            List<CauHoi> danhSachCauHoiTraLoiDung = new List<CauHoi>();
            foreach (int answerId in selectAnswer)
            {
                DapAn dapAn = await db.DapAns.FindAsync(answerId);
                if (db.DapAns.Count(x => x.MaCauHoi == dapAn.MaCauHoi && x.DapAnDung == true) > 1) // Nếu câu trả lời cho câu hỏi dạng checkbox
                {
                    List<int> danhSachDapAnDung = db.DapAns.Where(x => x.MaCauHoi == dapAn.MaCauHoi && x.DapAnDung == true).Select(x => x.MaDapAn).ToList();
                    if (selectAnswer.ToList().Intersect(danhSachDapAnDung).Count() == danhSachDapAnDung.Count)
                    {
                        if (danhSachCauHoiTraLoiDung.Any(x => x.MaCauHoiMoi == dapAn.MaCauHoi) == false)
                            danhSachCauHoiTraLoiDung.Add(dapAn.CauHoi);
                    }
                }
                else  // Nếu câu trả lời cho câu hỏi dạng radio
                {
                    if (dapAn.DapAnDung == true)
                    {
                        if (danhSachCauHoiTraLoiDung.Any(x => x.MaCauHoiMoi == dapAn.MaCauHoi) == false)
                            danhSachCauHoiTraLoiDung.Add(dapAn.CauHoi);
                    }
                }

            }
            ViewBag.TongCauHoi = (Session["CauHois"] as List<CauHoi>).Count;
            ViewBag.TongCauDung = danhSachCauHoiTraLoiDung.Count;
            ViewBag.TongCauSai = ViewBag.TongCauHoi - ViewBag.TongCauDung;
            Session["frm"] = frm;
            return View();
        }

        public async Task<ActionResult> ViewWrongAnswer()
        {
            FormCollection frm = Session["frm"] as FormCollection;
            List<int> selectAnswer = new List<int>();
            Dictionary<int, List<int>> cauHoiDapAnDung = new Dictionary<int, List<int>>();
            (Session["CauHois"] as List<CauHoi>).ForEach(x =>
            {
                selectAnswer.AddRange(frm["selectAnswer_" + x.MaCauHoiMoi].Split(',').Select(y => int.Parse(y.Trim())).ToList());
                cauHoiDapAnDung.Add(x.MaCauHoiMoi, x.DapAns.Where(y => y.DapAnDung == true).Select(y => y.MaDapAn).ToList());
            });

            List<CauHoi> danhSachCauHoiTraLoiDung = new List<CauHoi>();
            foreach (int answerId in selectAnswer)
            {
                DapAn dapAn = await db.DapAns.FindAsync(answerId);
                if (db.DapAns.Count(x => x.MaCauHoi == dapAn.MaCauHoi && x.DapAnDung == true) > 1) // Nếu câu trả lời cho câu hỏi dạng checkbox
                {
                    List<int> danhSachDapAnDung = db.DapAns.Where(x => x.MaCauHoi == dapAn.MaCauHoi && x.DapAnDung == true).Select(x => x.MaDapAn).ToList();
                    if (selectAnswer.ToList().Intersect(danhSachDapAnDung).Count() == danhSachDapAnDung.Count)
                    {
                        if (danhSachCauHoiTraLoiDung.Any(x => x.MaCauHoiMoi == dapAn.MaCauHoi) == false)
                            danhSachCauHoiTraLoiDung.Add(dapAn.CauHoi);
                    }
                }
                else  // Nếu câu trả lời cho câu hỏi dạng radio
                {
                    if (dapAn.DapAnDung == true)
                    {
                        if (danhSachCauHoiTraLoiDung.Any(x => x.MaCauHoiMoi == dapAn.MaCauHoi) == false)
                            danhSachCauHoiTraLoiDung.Add(dapAn.CauHoi);
                    }
                }

            }
            ViewBag.SelectAnswer = selectAnswer;
            ViewBag.danhSachCauHoiTraLoiDung = danhSachCauHoiTraLoiDung;
            ViewBag.cauHoiDapAnDung = cauHoiDapAnDung;
            ViewBag.CountAnswer = db.DapAns.ToList().Where(x => selectAnswer.Contains(x.MaDapAn)).Select(x => x.MaCauHoi).Distinct().Count();
            return View(Session["CauHois"]);
        }

        //[HttpPost]
        //public async Task<ActionResult> ViewResult(FormCollection frm)
        //{
        //    List<int> selectAnswer = new List<int>();
        //    Dictionary<int, List<int>> cauHoiDapAnDung = new Dictionary<int, List<int>>();
        //    (Session["CauHois"] as List<CauHoi>).ForEach(x =>
        //    {
        //        selectAnswer.AddRange(frm["selectAnswer_" + x.MaCauHoiMoi].Split(',').Select(y => int.Parse(y)).ToList());
        //        cauHoiDapAnDung.Add(x.MaCauHoiMoi, x.DapAns.Where(y => y.DapAnDung == true).Select(y => y.MaDapAn).ToList());
        //    });

        //    List<CauHoi> danhSachCauHoiTraLoiDung = new List<CauHoi>();
        //    foreach(int answerId in selectAnswer)
        //    {
        //        DapAn dapAn = await db.DapAns.FindAsync(answerId);
        //        if(db.DapAns.Count(x=>x.MaCauHoiMoi == dapAn.MaCauHoi && x.DapAnDung == true) > 1) // Nếu câu trả lời cho câu hỏi dạng checkbox
        //        {
        //            List<int> danhSachDapAnDung = db.DapAns.Where(x => x.MaCauHoiMoi == dapAn.MaCauHoi && x.DapAnDung == true).Select(x => x.MaDapAn).ToList();
        //            if (selectAnswer.ToList().Intersect(danhSachDapAnDung).Count() == danhSachDapAnDung.Count)
        //            {
        //                if (danhSachCauHoiTraLoiDung.Any(x => x.MaCauHoiMoi == dapAn.MaCauHoi) == false)
        //                    danhSachCauHoiTraLoiDung.Add(dapAn.CauHoi);
        //            }
        //        }
        //        else  // Nếu câu trả lời cho câu hỏi dạng radio
        //        {
        //            if (dapAn.DapAnDung == true)
        //            {
        //                if (danhSachCauHoiTraLoiDung.Any(x => x.MaCauHoiMoi == dapAn.MaCauHoi) == false)
        //                    danhSachCauHoiTraLoiDung.Add(dapAn.CauHoi);
        //            }
        //        }

        //    }
        //    ViewBag.SelectAnswer = selectAnswer;
        //    ViewBag.danhSachCauHoiTraLoiDung = danhSachCauHoiTraLoiDung;
        //    ViewBag.cauHoiDapAnDung = cauHoiDapAnDung;
        //    ViewBag.CountAnswer = db.DapAns.ToList().Where(x => selectAnswer.Contains(x.MaDapAn)).Select(x => x.MaCauHoiMoi).Distinct().Count();
        //    return View(Session["CauHois"]);
        //}


    }
}