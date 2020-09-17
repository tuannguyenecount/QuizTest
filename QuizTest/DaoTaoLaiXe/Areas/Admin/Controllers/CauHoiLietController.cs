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

        private List<CauHoi> CauHoiLiets
        {
            get
            {
                return db.CauHois.Where(x => x.CauHoiLiet == true).ToList();
            }
        }

        // GET: Admin/CauHoiLiet
        public ActionResult Index()
        {
            List<CauHoi> cauHoiLiets = CauHoiLiets;
            Dictionary<int, List<DapAn>> dapAnCauHoiLietDictionarys = new Dictionary<int, List<DapAn>>();
            foreach (var cauHoiLiet in cauHoiLiets)
            {
                dapAnCauHoiLietDictionarys.Add(cauHoiLiet.MaCauHoi, db.DapAns.Where(x => x.MaCauHoi == cauHoiLiet.MaCauHoi).OrderBy(x => x.SoThuTu).ToList());
            }
            ViewBag.DapAnCauHoiLietDictionarys = dapAnCauHoiLietDictionarys;
            return View(cauHoiLiets);
        }

        // GET: Admin/CauHoiLiet/Details/5
        public ActionResult Details(int id)
        {
            CauHoi cauHoiLiet = CauHoiLiets.FirstOrDefault(x => x.MaCauHoi == id);
            if(cauHoiLiet == null)
            {
                return Redirect("/pages/404");
            }
            ViewBag.DapAnCauHoiLiets = db.DapAns.Where(x => x.MaCauHoi == cauHoiLiet.MaCauHoi).OrderBy(x => x.SoThuTu).ToList();
            return View(cauHoiLiet);
        }

        // GET: Admin/CauHoiLiet/Create
        public ActionResult Create()
        {
            return View(new CauHoi());
        }

        // POST: Admin/CauHoiLiet/Create
        [HttpPost]
        public async Task<ActionResult> Create(CauHoi cauHoiLiet, DapAn dapan1, DapAn dapan2, DapAn dapan3, DapAn dapan4, HttpPostedFileBase file)
        {
            try
            {
                cauHoiLiet.MaCauHoiMoi = cauHoiLiet.MaCauHoi;
                cauHoiLiet.MaChuyenMuc = 8;
                List<DapAn> DapAnCauHoiLiets = new List<DapAn>();
                if(CauHoiLiets.Count() > 60)
                {
                    ModelState.AddModelError("", "Câu Hỏi Liệt Không Thể Trên 60 Câu!");
                }
                if (CauHoiLiets.Any(x => x.MaCauHoi == cauHoiLiet.MaCauHoi))
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
                    db.CauHois.Add(cauHoiLiet);
                    int result = await db.SaveChangesAsync();
                    if(result > 0)
                    {
                        foreach (var dapAnCauHoiLiet in DapAnCauHoiLiets)
                        {
                            dapAnCauHoiLiet.MaCauHoi = cauHoiLiet.MaCauHoi;
                            db.DapAns.Add(dapAnCauHoiLiet);
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
            CauHoi cauHoiLiet = CauHoiLiets.FirstOrDefault(x => x.MaCauHoi == id);
            if (cauHoiLiet == null)
            {
                return Redirect("/pages/404");
            }
            ViewBag.DapAnCauHoiLiets = db.DapAns.Where(x => x.MaCauHoi == cauHoiLiet.MaCauHoi).OrderBy(x => x.SoThuTu).ToList();
            return View(cauHoiLiet);
        }

        // POST: Admin/CauHoiLiet/Edit/5
        [HttpPost]
        public ActionResult Edit(CauHoi cauHoiLiet, DapAn dapan1, DapAn dapan2, DapAn dapan3, DapAn dapan4, HttpPostedFileBase file)
        {
            try
            {
                cauHoiLiet.MaChuyenMuc = 8;
                List<DapAn> dapAns = new List<DapAn>();
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
                    foreach (DapAn dapAnItem in dapAns)
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
            CauHoi cauHoiLiet = CauHoiLiets.FirstOrDefault(x => x.MaCauHoi == id);

            if (cauHoiLiet != null)
            {
                db.CauHois.Remove(cauHoiLiet);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddAnswer([Bind(Exclude = "SoThuTu")]DapAn dapAn)
        {
            CauHoi cauHoiLiet = CauHoiLiets.FirstOrDefault(x => x.MaCauHoi == dapAn.MaCauHoi);           
            if(ModelState.IsValid)
            {
                db.DapAns.Add(dapAn);
                db.SaveChanges();
                return RedirectToAction("Edit", new { Id = cauHoiLiet.MaCauHoi });
            }
            return View("Edit", cauHoiLiet);
        }

        [HttpPost]
        public ActionResult DeleteAnswer(int Id)
        {
            DapAn dapAn = db.DapAns.Find(Id);
            CauHoi cauHoiLiet = CauHoiLiets.FirstOrDefault(x => x.MaCauHoi == dapAn.MaCauHoi);
            try
            {
                if (dapAn != null)
                {
                    db.DapAns.Remove(dapAn);
                    db.SaveChanges();
                }
                return RedirectToAction("Edit",new { Id = cauHoiLiet.MaCauHoi });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", cauHoiLiet);
            }
        }
    }
}
