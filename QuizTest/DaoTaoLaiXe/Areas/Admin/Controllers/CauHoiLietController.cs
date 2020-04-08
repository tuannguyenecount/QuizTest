using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            List<CauHoiLiet> cauHoiLiets = db.CauHoiLiets.OrderBy(x => x.MaCauHoi).ToList();
            Dictionary<int, List<DapAnCauHoiLiet>> dapAnCauHoiLietDictionarys = new Dictionary<int, List<DapAnCauHoiLiet>>();
            foreach (var cauHoiLiet in cauHoiLiets)
            {
                dapAnCauHoiLietDictionarys.Add(cauHoiLiet.MaCauHoi, db.DapAnCauHoiLiets.Where(x => x.MaCauHoi == cauHoiLiet.MaCauHoi).OrderBy(x => x.SoThuTu).ToList());
            }
            ViewBag.DapAnCauHoiLietDictionarys = dapAnCauHoiLietDictionarys;
            return View(cauHoiLiets);
        }

        // GET: Admin/CauHoiLiet/Details/5
        public ActionResult Details(int id)
        {
            CauHoiLiet cauHoiLiet = db.CauHoiLiets.Find(id);
            if(cauHoiLiet == null)
            {
                return Redirect("/pages/404");
            }
            ViewBag.DapAnCauHoiLiets = db.DapAnCauHoiLiets.Where(x => x.MaCauHoi == cauHoiLiet.MaCauHoi).OrderBy(x => x.SoThuTu).ToList();
            return View(cauHoiLiet);
        }

        // GET: Admin/CauHoiLiet/Create
        public ActionResult Create()
        {
            return View(new CauHoiLiet());
        }

        // POST: Admin/CauHoiLiet/Create
        [HttpPost]
        public async Task<ActionResult> Create(CauHoiLiet cauHoiLiet, DapAnCauHoiLiet dapan1, DapAnCauHoiLiet dapan2, DapAnCauHoiLiet dapan3, DapAnCauHoiLiet dapan4, HttpPostedFileBase file)
        {
            try
            {
                List<DapAnCauHoiLiet> DapAnCauHoiLiets = new List<DapAnCauHoiLiet>();
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
                        DapAnCauHoiLiets.Add(dapan1);
                    }
                    if (!string.IsNullOrEmpty(dapan2.NoiDung))
                    {
                        DapAnCauHoiLiets.Add(dapan2);
                    }
                    if (!string.IsNullOrEmpty(dapan3.NoiDung))
                    {
                        DapAnCauHoiLiets.Add(dapan3);
                    }
                    if (!string.IsNullOrEmpty(dapan4.NoiDung))
                    {
                        DapAnCauHoiLiets.Add(dapan4);
                    }
                    if (DapAnCauHoiLiets.Count > 0 && DapAnCauHoiLiets.Any(x => x.DapAnDung == true) == false)
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
                    int result = await db.SaveChangesAsync();
                    if(result > 0)
                    {
                        foreach (var dapAnCauHoiLiet in DapAnCauHoiLiets)
                        {
                            dapAnCauHoiLiet.MaCauHoi = cauHoiLiet.MaCauHoi;
                            db.DapAnCauHoiLiets.Add(dapAnCauHoiLiet);
                            await db.SaveChangesAsync();
                        }
                    }
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
            CauHoiLiet cauHoiLiet = db.CauHoiLiets.Find(id);
            if (cauHoiLiet == null)
            {
                return Redirect("/pages/404");
            }
            ViewBag.DapAnCauHoiLiets = db.DapAnCauHoiLiets.Where(x => x.MaCauHoi == cauHoiLiet.MaCauHoi).OrderBy(x => x.SoThuTu).ToList();
            return View(cauHoiLiet);
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
                    return RedirectToAction("Edit", new { Id = cauHoiLiet.ID, message = "Sửa thành công" });
                }
                return View(cauHoiLiet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
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
            CauHoiLiet cauHoiLiet = db.CauHoiLiets.FirstOrDefault(x => x.MaCauHoi == dapAn.MaCauHoi);           
            if(ModelState.IsValid)
            {
                db.DapAnCauHoiLiets.Add(dapAn);
                db.SaveChanges();
                return RedirectToAction("Edit", new { Id = cauHoiLiet.ID });
            }
            return View("Edit", cauHoiLiet);
        }

        [HttpPost]
        public ActionResult DeleteAnswer(int Id)
        {
            DapAnCauHoiLiet dapAn = db.DapAnCauHoiLiets.Find(Id);
            CauHoiLiet cauHoiLiet = db.CauHoiLiets.FirstOrDefault(x => x.MaCauHoi == dapAn.MaCauHoi);
            try
            {
                if (dapAn != null)
                {
                    db.DapAnCauHoiLiets.Remove(dapAn);
                    db.SaveChanges();
                }
                return RedirectToAction("Edit",new { Id = cauHoiLiet.ID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", cauHoiLiet);
            }
        }
    }
}
