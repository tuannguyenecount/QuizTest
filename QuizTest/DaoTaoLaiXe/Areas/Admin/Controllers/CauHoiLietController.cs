using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaoTaoLaiXe.Models;
namespace DaoTaoLaiXe.Areas.Admin.Controllers
{
    [AuthorizeFilter]
    public class CauHoiLietController : Controller
    {
        QuizDBEntities db = new QuizDBEntities();

        // GET: Admin/CauHoiLiet
        public ActionResult Index()
        {
            return View(db.CauHoiLiets.OrderBy(x => x.MaCauHoi).ToList());
        }

        // GET: Admin/CauHoiLiet/Details/5
        public ActionResult Details(int id)
        {
            CauHoiLiet cauHoiLiet = db.CauHoiLiets.Find(id);
            if(cauHoiLiet == null)
            {
                return Redirect("/pages/404");
            }
            return View(cauHoiLiet);
        }

        // GET: Admin/CauHoiLiet/Create
        public ActionResult Create()
        {
            return View(new CauHoiLiet());
        }

        // POST: Admin/CauHoiLiet/Create
        [HttpPost]
        public ActionResult Create(CauHoiLiet cauHoiLiet, DapAnCauHoiLiet dapan1, DapAnCauHoiLiet dapan2, DapAnCauHoiLiet dapan3, DapAnCauHoiLiet dapan4, HttpPostedFileBase file)
        {
            try
            {
                cauHoiLiet.DapAnCauHoiLiets = new List<DapAnCauHoiLiet>();
                if(db.CauHoiLiets.Count() == 60)
                {
                    ModelState.AddModelError("", "Câu Hỏi Liệt Không Thể Trên 60 Câu!");
                }
                if (db.CauHoiLiets.Any(x => x.MaCauHoi == cauHoiLiet.MaCauHoi))
                {
                    ModelState.AddModelError("MaCauHoi", "Mã câu hỏi này đã tồn tại. Bạn hãy kiểm tra lại.");
                }
                else
                {
                    if (!string.IsNullOrEmpty(dapan1.NoiDung))
                    {
                        cauHoiLiet.DapAnCauHoiLiets.Add(dapan1);
                    }
                    if (!string.IsNullOrEmpty(dapan2.NoiDung))
                    {
                        cauHoiLiet.DapAnCauHoiLiets.Add(dapan2);
                    }
                    if (!string.IsNullOrEmpty(dapan3.NoiDung))
                    {
                        cauHoiLiet.DapAnCauHoiLiets.Add(dapan3);
                    }
                    if (!string.IsNullOrEmpty(dapan4.NoiDung))
                    {
                        cauHoiLiet.DapAnCauHoiLiets.Add(dapan4);
                    }
                    if (cauHoiLiet.DapAnCauHoiLiets.Count > 0 && cauHoiLiet.DapAnCauHoiLiets.Any(x => x.DapAnDung == true) == false)
                    {
                        ModelState.AddModelError("", "Bạn cần chỉ định ít nhất 1 đáp án là đáp án đúng!");
                    }
                }
                if (ModelState.IsValid)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string fileName = DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(file.FileName);
                        cauHoiLiet.Hinh = fileName;
                        file.SaveAs(Server.MapPath("~/Content/images/" + fileName));
                    }
                    db.CauHoiLiets.Add(cauHoiLiet);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(cauHoiLiet);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(cauHoiLiet);
            }
        }

        // GET: Admin/CauHoiLiet/Edit/5
        public ActionResult Edit(int id)
        {
            CauHoiLiet cauHoi = db.CauHoiLiets.Find(id);
            if (cauHoi == null)
            {
                return Redirect("/pages/404");
            }
            return View(cauHoi);
        }

        // POST: Admin/CauHoiLiet/Edit/5
        [HttpPost]
        public ActionResult Edit(CauHoiLiet cauHoiLiet, DapAnCauHoiLiet dapan1, DapAnCauHoiLiet dapan2, DapAnCauHoiLiet dapan3, DapAnCauHoiLiet dapan4, HttpPostedFileBase file)
        {
            try
            {               
                List<DapAnCauHoiLiet> dapAns = new List<DapAnCauHoiLiet>();
                if (dapan1.MaDapAn != 0)
                {
                    dapAns.Add(dapan1);
                }
                if (dapan2.MaDapAn != 0)
                {
                    dapAns.Add(dapan2);
                }
                if (dapan3.MaDapAn != 0)
                {
                    dapAns.Add(dapan3);
                }
                if (dapan4.MaDapAn != 0)
                {
                    dapAns.Add(dapan4);
                }
                if (dapAns.Any(x => x.DapAnDung == true) == false)
                {
                    ModelState.AddModelError("", "Bạn cần chỉ định ít nhất 1 đáp án là đáp án đúng!");
                }
                if (ModelState.IsValid)
                {
                    foreach (DapAnCauHoiLiet dapAnItem in dapAns)
                    {
                        db.Entry(dapAnItem).State = System.Data.Entity.EntityState.Modified;
                    }
                    if (file != null && file.ContentLength > 0)
                    {
                        string fileName = DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(file.FileName);
                        cauHoiLiet.Hinh = fileName;
                        file.SaveAs(Server.MapPath("~/Content/images/" + fileName));
                    }
                    db.Entry(cauHoiLiet).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Edit", new { Id = cauHoiLiet.MaCauHoi, message = "Sửa thành công" });
                }
                cauHoiLiet.DapAnCauHoiLiets = db.CauHoiLiets.Find(cauHoiLiet.MaCauHoi).DapAnCauHoiLiets.ToList();
                return View(cauHoiLiet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                cauHoiLiet.DapAnCauHoiLiets = db.CauHoiLiets.Find(cauHoiLiet.MaCauHoi).DapAnCauHoiLiets.ToList();
                return View(cauHoiLiet);
            }
        }

        // POST: Admin/CauHoiLiet/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            CauHoiLiet cauHoiLiet = db.CauHoiLiets.Find(id);

            if (cauHoiLiet != null)
            {
                db.CauHoiLiets.Remove(cauHoiLiet);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddAnswer([Bind(Exclude = "SoThuTu")]DapAnCauHoiLiet dapAn)
        {
            CauHoiLiet cauHoiLiet = db.CauHoiLiets.Find(dapAn.MaCauHoi);
            if(cauHoiLiet.DapAnCauHoiLiets != null && cauHoiLiet.DapAnCauHoiLiets.Count >= 4)
            {
                ModelState.AddModelError("", "Câu hỏi này đã có đủ 4 đáp án. Không thể thêm đáp án khác được!");
            }
            else 
                dapAn.SoThuTu = (byte)(cauHoiLiet.DapAnCauHoiLiets.Count + 1);
            if(ModelState.IsValid)
            {
                db.DapAnCauHoiLiets.Add(dapAn);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = dapAn.MaCauHoi });
            }
            return View("Edit", cauHoiLiet);
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
                return View("Edit", dapAn.CauHoi);
            }
        }
    }
}
