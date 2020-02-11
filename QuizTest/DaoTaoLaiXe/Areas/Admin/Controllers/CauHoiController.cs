using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaoTaoLaiXe.Models;
namespace DaoTaoLaiXe.Areas.Admin.Controllers
{
    [AuthorizeFilter]
    public class CauHoiController : Controller
    {
        QuizDBEntities db = new QuizDBEntities();

        // GET: Admin/CauHoi
        public ActionResult Index(int MaChuyenMuc)
        {
            ChuyenMucCauHoi chuyenMuc = db.ChuyenMucCauHois.Find(MaChuyenMuc);
            ViewBag.ChuyenMucCauHoi = chuyenMuc; 
            return View(chuyenMuc.CauHois.ToList());
        }

        // GET: Admin/CauHoi/Details/5
        public ActionResult Details(int id)
        {
            CauHoi cauHoi = db.CauHois.Find(id);
            if(cauHoi == null)
            {
                return HttpNotFound();
            }
            return View(cauHoi);
        }

        // GET: Admin/CauHoi/Create
        public ActionResult Create(int? MaChuyenMuc)
        {
            ViewBag.MaChuyenMuc = new SelectList(db.ChuyenMucCauHois.OrderBy(x => x.MaChuyenMuc).ToList(), "MaChuyenMuc", "TenChuyenMuc", MaChuyenMuc);
            return View(new CauHoi());
        }

        // POST: Admin/CauHoi/Create
        [HttpPost]
        public ActionResult Create(CauHoi cauHoi, DapAn dapan1, DapAn dapan2, DapAn dapan3, DapAn dapan4, HttpPostedFileBase file)
        {
            try
            {
                cauHoi.DapAns = new List<DapAn>();
                cauHoi.DapAns.Add(dapan1);
                cauHoi.DapAns.Add(dapan2);
                cauHoi.DapAns.Add(dapan3);
                if(!string.IsNullOrEmpty(dapan4.NoiDung))
                {
                    cauHoi.DapAns.Add(dapan4);
                }
                if(cauHoi.DapAns.Any(x=>x.DapAnDung == true) == false)
                {
                    ModelState.AddModelError("", "Bạn cần chỉ định ít nhất 1 đáp án là đáp án đúng!");
                }
                if(file != null && file.ContentLength > 0)
                {
                    string fileName = DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(file.FileName);
                    cauHoi.Hinh = fileName;
                    file.SaveAs(Server.MapPath("~/Content/images/" + fileName));
                }
                if (ModelState.IsValid)
                {
                    db.CauHois.Add(cauHoi);
                    db.SaveChanges();
                    return RedirectToAction("Index",new { MaChuyenMuc = cauHoi.MaChuyenMuc });
                }
                return View(cauHoi);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.MaChuyenMuc = new SelectList(db.ChuyenMucCauHois.OrderBy(x => x.MaChuyenMuc).ToList(), "MaChuyenMuc", "TenChuyenMuc", cauHoi.MaChuyenMuc);
                return View(cauHoi);
            }
        }

        // GET: Admin/CauHoi/Edit/5
        public ActionResult Edit(int id)
        {
            CauHoi cauHoi = db.CauHois.Find(id);
            if (cauHoi == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaChuyenMuc = new SelectList(db.ChuyenMucCauHois.OrderBy(x => x.MaChuyenMuc).ToList(), "MaChuyenMuc", "TenChuyenMuc", cauHoi.MaChuyenMuc);
            return View(cauHoi);
        }

        // POST: Admin/CauHoi/Edit/5
        [HttpPost]
        public ActionResult Edit(CauHoi cauHoi, DapAn dapan1, DapAn dapan2, DapAn dapan3, DapAn dapan4, HttpPostedFileBase file)
        {
            try
            {
                List<DapAn> dapAns = new List<DapAn>();
                dapAns.Add(dapan1);
                dapAns.Add(dapan2);
                dapAns.Add(dapan3);
                if (cauHoi.DapAns.Count > 3)
                {
                    dapAns.Add(dapan4);
                }
                if (dapAns.Any(x => x.DapAnDung == true) == false)
                {
                    ModelState.AddModelError("", "Bạn cần chỉ định ít nhất 1 đáp án là đáp án đúng!");
                }
                else
                {
                    foreach(DapAn dapAnItem in dapAns)
                    {
                        db.Entry(dapAnItem).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(file.FileName);
                    cauHoi.Hinh = fileName;
                    file.SaveAs(Server.MapPath("~/Content/images/" + fileName));
                }
               // cauHoi.DapAns = dapAns;
                if (ModelState.IsValid)
                {
                    db.Entry(cauHoi).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { MaChuyenMuc = cauHoi.MaChuyenMuc });
                }
                ViewBag.MaChuyenMuc = new SelectList(db.ChuyenMucCauHois.OrderBy(x => x.MaChuyenMuc).ToList(), "MaChuyenMuc", "TenChuyenMuc", cauHoi.MaChuyenMuc);
                cauHoi.DapAns = db.CauHois.Find(cauHoi.MaCauHoi).DapAns.ToList();
                return View(cauHoi);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.MaChuyenMuc = new SelectList(db.ChuyenMucCauHois.OrderBy(x => x.MaChuyenMuc).ToList(), "MaChuyenMuc", "TenChuyenMuc", cauHoi.MaChuyenMuc);
                cauHoi.DapAns = db.CauHois.Find(cauHoi.MaCauHoi).DapAns.ToList();
                return View(cauHoi);
            }
        }

        // POST: Admin/CauHoi/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            CauHoi cauHoi = db.CauHois.Find(id);
            int maChuyenMuc = 1;

            if (cauHoi != null)
            {
                db.CauHois.Remove(cauHoi);
                maChuyenMuc = cauHoi.MaChuyenMuc ?? 1;
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { MaChuyenMuc = maChuyenMuc });
        }

        [HttpPost]
        public ActionResult AddAnswer([Bind(Exclude = "SoThuTu")]DapAn dapAn)
        {
            CauHoi cauHoi = db.CauHois.Find(dapAn.MaCauHoi);
            if(cauHoi.DapAns != null && cauHoi.DapAns.Count >= 4)
            {
                ModelState.AddModelError("", "Câu hỏi này đã có đủ 4 đáp án. Không thể thêm đáp án khác được!");
            }
            else 
                dapAn.SoThuTu = (byte)(cauHoi.DapAns.Count + 1);
            if(ModelState.IsValid)
            {
                db.DapAns.Add(dapAn);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = dapAn.MaCauHoi });

            }
            ViewBag.MaChuyenMuc = new SelectList(db.ChuyenMucCauHois.OrderBy(x => x.MaChuyenMuc).ToList(), "MaChuyenMuc", "TenChuyenMuc", cauHoi.MaChuyenMuc);
            return View("Edit", cauHoi);
        }

        [HttpPost]
        public ActionResult DeleteAnswer(int Id)
        {
            DapAn dapAn = db.DapAns.Find(Id);
            try
            {
                if (dapAn != null)
                {
                    db.DapAns.Remove(dapAn);
                    db.SaveChanges();
                }
                return RedirectToAction("Edit", new { id = dapAn.MaCauHoi });
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.MaChuyenMuc = new SelectList(db.ChuyenMucCauHois.OrderBy(x => x.MaChuyenMuc).ToList(), "MaChuyenMuc", "TenChuyenMuc", dapAn.CauHoi.MaChuyenMuc);
                return View("Edit", dapAn.CauHoi);
            }
        }
    }
}
