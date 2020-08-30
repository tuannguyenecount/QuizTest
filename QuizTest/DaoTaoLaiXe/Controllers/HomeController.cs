using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DaoTaoLaiXe.Models;

namespace DaoTaoLaiXe.Controllers
{
    public class HomeController : Controller
    {
        QuizDBEntities db = new QuizDBEntities();
        private List<CauHoi> CauHoiLiets
        {
            get
            {
                return db.CauHois.Where(x => x.MaChuyenMuc == 8).ToList();
            }
        }

        public ViewResult Index()
        {
            return View();
        }

        [Route("600-cau-hoi-on-tap-sat-hach-lai-xe")]
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


        [Route("60-cau-hoi-diem-liet")]
        public ViewResult FierceQuestion()
        {
            Dictionary<int, List<int>> cauHoiDapAnDung = new Dictionary<int, List<int>>();
            List<CauHoi> cauHoiLiets = CauHoiLiets.OrderBy(x => x.MaCauHoi).ToList();
            cauHoiLiets.ForEach(x =>
            {
                List<DapAn> dapAnCauHoiLiets = db.DapAns.Where(y => y.MaCauHoi == x.MaCauHoi).ToList();
                cauHoiDapAnDung.Add(x.MaCauHoi, dapAnCauHoiLiets.Where(y => y.DapAnDung == true).Select(y => y.MaDapAn).ToList());
            });
            ViewBag.cauHoiDapAnDung = cauHoiDapAnDung;
            Dictionary<int, List<DapAn>> dapAnCauHoiLietDictionarys = new Dictionary<int, List<DapAn>>();
            foreach(var cauHoiLiet in cauHoiLiets)
            {
                dapAnCauHoiLietDictionarys.Add(cauHoiLiet.MaCauHoi, db.DapAns.Where(x => x.MaCauHoi == cauHoiLiet.MaCauHoi).OrderBy(x => x.SoThuTu).ToList());
            }
            ViewBag.DapAnCauHoiLietDictionarys = dapAnCauHoiLietDictionarys;
            return View(cauHoiLiets);
        }

        void ShuffleArray(ref List<CauHoi> lst)
        {
            Random r = new Random();
            for (int i = lst.Count - 1; i > 0; i--)
            {
                int j = (int)Math.Floor(r.NextDouble() * (i + 1));
                CauHoi temp = lst[i];
                lst[i] = lst[j];
                lst[j] = temp;
            }
        }

        string GetListIdQuestion(List<CauHoi> cauHois)
        {
            string result = "";
            foreach(CauHoi cauHoi in cauHois)
            {
                result += cauHoi.MaCauHoiMoi + ",";
            }
            if(result != "")
            {
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }

        void LayCauHoiChuong1(List<CauHoi> cauhois, int take)
        {
            List<CauHoi> lstToAdd = new List<CauHoi>();
            Random random = new Random();
            lstToAdd = db.CauHois.Where(x => x.SuDung == true && x.MaCauHoiMoi >= 1 && x.MaCauHoiMoi <= 166).ToList();
            if (lstToAdd.Count >= take)
            {
                ShuffleArray(ref lstToAdd);
                lstToAdd = lstToAdd.Skip(random.Next(0, lstToAdd.Count - take)).Take(take).ToList();
                cauhois.AddRange(lstToAdd);  
            }
        }

        void LayCauHoiChuong2(List<CauHoi> cauhois, int take)
        {
            List<CauHoi> lstToAdd = new List<CauHoi>();
            Random random = new Random();
            lstToAdd = db.CauHois.Where(x => x.SuDung == true && x.MaCauHoiMoi >= 167 && x.MaCauHoiMoi <= 192).ToList();
            if (lstToAdd.Count >= take)
            {
                ShuffleArray(ref lstToAdd);
                lstToAdd = lstToAdd.Skip(random.Next(0, lstToAdd.Count - take)).Take(take).ToList();
                cauhois.AddRange(lstToAdd);  
            }
        }

        void LayCauHoiChuong3(List<CauHoi> cauhois, int take)
        {
            List<CauHoi> lstToAdd = new List<CauHoi>();
            Random random = new Random();
            lstToAdd = db.CauHois.Where(x => x.SuDung == true && x.MaCauHoiMoi >= 193 && x.MaCauHoiMoi <= 213).ToList();
            if (lstToAdd.Count >= take)
            {
                ShuffleArray(ref lstToAdd);
                lstToAdd = lstToAdd.Skip(random.Next(0, lstToAdd.Count - take)).Take(take).ToList();
                cauhois.AddRange(lstToAdd);
            }
        }

        void LayCauHoiChuong4(List<CauHoi> cauhois, int take)
        {
            List<CauHoi> lstToAdd = new List<CauHoi>();
            Random random = new Random();
            lstToAdd = db.CauHois.Where(x => x.SuDung == true && x.MaCauHoiMoi >= 214 && x.MaCauHoiMoi <= 269).ToList();
            if (lstToAdd.Count >= take)
            {
                ShuffleArray(ref lstToAdd);
                lstToAdd = lstToAdd.Skip(random.Next(0, lstToAdd.Count - take)).Take(take).ToList();
                cauhois.AddRange(lstToAdd);
            }
        }

        void LayCauHoiChuong5(List<CauHoi> cauhois, int take)
        {
            List<CauHoi> lstToAdd = new List<CauHoi>();
            Random random = new Random();
            lstToAdd = db.CauHois.Where(x => x.SuDung == true && x.MaCauHoiMoi >= 270 && x.MaCauHoiMoi <= 304).ToList();
            if (lstToAdd.Count >= take)
            {
                ShuffleArray(ref lstToAdd);
                lstToAdd = lstToAdd.Skip(random.Next(0, lstToAdd.Count - take)).Take(take).ToList();
                cauhois.AddRange(lstToAdd);
            }
        }

        void LayCauHoiChuong6(List<CauHoi> cauhois, int take)
        {
            List<CauHoi> lstToAdd = new List<CauHoi>();
            Random random = new Random();
            lstToAdd = db.CauHois.Where(x => x.SuDung == true && x.MaCauHoiMoi >= 305 && x.MaCauHoiMoi <= 486).ToList();
            if (lstToAdd.Count >= take)
            {
                ShuffleArray(ref lstToAdd);
                lstToAdd = lstToAdd.Skip(random.Next(0, lstToAdd.Count - take)).Take(take).ToList();
                cauhois.AddRange(lstToAdd);
            }
        }

        void LayCauHoiChuong7(List<CauHoi> cauhois, int take)
        {
            List<CauHoi> lstToAdd = new List<CauHoi>();
            Random random = new Random();
            lstToAdd = db.CauHois.Where(x => x.SuDung == true && x.MaCauHoiMoi >= 487 && x.MaCauHoiMoi <= 600).ToList();
            if (lstToAdd.Count >= take)
            {
                ShuffleArray(ref lstToAdd);
                lstToAdd = lstToAdd.Skip(random.Next(0, lstToAdd.Count - take)).Take(take).ToList();
                cauhois.AddRange(lstToAdd);
            }
        }

        void LayCauHoiChuong8(List<CauHoi> cauhois, int take)
        {
            List<CauHoi> lstToAdd = new List<CauHoi>();
            Random random = new Random();
            lstToAdd = db.CauHois.Where(x => x.SuDung == true && x.MaChuyenMuc == 8).ToList();
            if (lstToAdd.Count >= take)
            {
                ShuffleArray(ref lstToAdd);
                lstToAdd = lstToAdd.Skip(random.Next(0, lstToAdd.Count - take)).Take(take).ToList();
                cauhois.AddRange(lstToAdd);
            }
        }

        private List<CauHoi> CreateContentB1() // 30 câu
        {
            List<CauHoi> cauhois = new List<CauHoi>();

            LayCauHoiChuong1(cauhois, 9);

            LayCauHoiChuong3(cauhois, 1);
            LayCauHoiChuong4(cauhois, 1);
            LayCauHoiChuong5(cauhois, 1);
            LayCauHoiChuong6(cauhois, 9);
            LayCauHoiChuong7(cauhois, 4);
            LayCauHoiChuong8(cauhois, 5);

            cauhois = cauhois.Where(x => x.DapAns != null && x.DapAns.Count > 0 && x.DapAns.Any(y => y.DapAnDung == true)).ToList();

            return cauhois;
        }

        private List<CauHoi> CreateContentB2() // 35 câu
        {
            List<CauHoi> cauhois = new List<CauHoi>();

            LayCauHoiChuong1(cauhois, 10);
            LayCauHoiChuong2(cauhois, 1);
            LayCauHoiChuong3(cauhois, 1);
            LayCauHoiChuong4(cauhois, 2);
            LayCauHoiChuong5(cauhois, 1);
            LayCauHoiChuong6(cauhois, 10);
            LayCauHoiChuong7(cauhois, 6);
            LayCauHoiChuong8(cauhois, 4);

            cauhois = cauhois.Where(x => x.DapAns != null && x.DapAns.Count > 0 && x.DapAns.Any(y => y.DapAnDung == true)).ToList();

            return cauhois;
        }

        private List<CauHoi> CreateContentC() // 40 câu
        {
            List<CauHoi> cauhois = new List<CauHoi>();

            LayCauHoiChuong1(cauhois, 10);
            LayCauHoiChuong2(cauhois, 1);
            LayCauHoiChuong3(cauhois, 1);
            LayCauHoiChuong4(cauhois, 2);
            LayCauHoiChuong5(cauhois, 1);

            LayCauHoiChuong6(cauhois, 14);

            Random random = new Random();
            int take = random.Next(1, 11);
            LayCauHoiChuong7(cauhois, take);
            LayCauHoiChuong8(cauhois, 11 - take);

            cauhois = cauhois.Where(x => x.DapAns != null && x.DapAns.Count > 0 && x.DapAns.Any(y => y.DapAnDung == true)).ToList();

            return cauhois;
        }

        private List<CauHoi> CreateContentDEFC() // 45 câu
        {
            List<CauHoi> cauhois = new List<CauHoi>();

            LayCauHoiChuong1(cauhois, 10);
            LayCauHoiChuong2(cauhois, 1);
            LayCauHoiChuong3(cauhois, 1);
            LayCauHoiChuong4(cauhois, 2);
            LayCauHoiChuong5(cauhois, 1);

            LayCauHoiChuong6(cauhois, 16);

            Random random = new Random();
            int take = random.Next(1, 14);
            LayCauHoiChuong7(cauhois, take);
            LayCauHoiChuong8(cauhois, 14 - take);

            cauhois = cauhois.Where(x => x.DapAns != null && x.DapAns.Count > 0 && x.DapAns.Any(y => y.DapAnDung == true)).ToList();

            return cauhois;
        }

        [Route("de-thi-sat-hach-lai-xe/{category}")]
        public ActionResult Practice(string category)
        {
            List<CauHoi> cauHois = new List<CauHoi>();

            switch(category)
            {
                case "B1": cauHois = CreateContentB1(); break;
                case "B2": cauHois = CreateContentB2(); break;
                case "C": cauHois = CreateContentC(); break;
                case "DEFC": cauHois = CreateContentDEFC(); break;
            }

            ViewBag.SelectAnswer = new List<int>();
            ViewBag.danhSachCauHoiTraLoiDung = new List<CauHoi>();
            ViewBag.cauHoiDapAnDung = new Dictionary<int, List<int>>();
            ViewBag.CountAnswer = 0;
            ViewBag.CauHois = GetListIdQuestion(cauHois);
            ViewBag.Category = category;
            return View(cauHois);
        }

        [Route("ket-qua-thi-sat-hach-lai-xe")]
        public ActionResult ViewResult(string Category)
        {
            ViewBag.Category = Category;
            return RedirectToAction("Practice",new { category = Category });
        }

        [Route("ket-qua-thi-sat-hach-lai-xe")]
        [HttpPost]
        public async Task<ActionResult> ViewResult(FormCollection frm)
        {
            ViewBag.Category = frm["Category"];
            List<int> selectAnswer = new List<int>();
            List<CauHoi> cauHois = new List<CauHoi>();
            foreach (string sMaCauHoi in frm["CauHois"].Split(','))
            {
                int maCauHoi = int.Parse(sMaCauHoi);
                CauHoi ch = await db.CauHois.FindAsync(maCauHoi);
                cauHois.Add(ch);
            }
            try
            {

                cauHois.ForEach(x =>
                {
                    if (frm["selectAnswer_" + x.MaCauHoiMoi] == null)
                    {
                        throw new Exception("selectAnswer_" + x.MaCauHoiMoi + " null");
                    }
                    selectAnswer.AddRange(frm["selectAnswer_" + x.MaCauHoiMoi].Split(',').Select(y => int.Parse(y.Trim())).ToList());
                });
            }
            catch
            {
                throw new Exception("Lỗi số 1");
            }

            List<CauHoi> danhSachCauHoiTraLoiDung = new List<CauHoi>();
            foreach (int answerId in selectAnswer)
            {
                DapAn dapAn = db.DapAns.Find(answerId);
                if(dapAn == null)
                {
                    throw new Exception("Đáp án " + answerId + " null");
                }
                try
                {
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
                catch
                {
                    throw new Exception("Lỗi số 2");
                }

            }
            try
            {
                ViewBag.TongCauHoi = cauHois.Count;
                ViewBag.TongCauDung = danhSachCauHoiTraLoiDung.Count;
            }
            catch
            {
                throw new Exception("Lỗi số 3");
            }
            ViewBag.TongCauSai = ViewBag.TongCauHoi - ViewBag.TongCauDung;
            Session["frm"] = frm;
            return View();
        }

        [Route("thi-thu-60-cau-hoi-diem-liet")]
        public ActionResult PracticeFierceQuestion()
        {
            List<CauHoi> cauhois = CauHoiLiets;
            ViewBag.SelectAnswer = new List<int>();
            ViewBag.danhSachCauHoiTraLoiDung = new List<CauHoi>();
            ViewBag.cauHoiDapAnDung = new Dictionary<int, List<int>>();
            ViewBag.CountAnswer = 0;
            ViewBag.DapAnsCauHoiLiet = new Dictionary<int, List<DapAn>>();
            foreach (CauHoi item in cauhois)
            {
                (ViewBag.DapAnsCauHoiLiet as Dictionary<int, List<DapAn>>).Add(item.MaCauHoi, db.DapAns.Where(x => x.MaCauHoi == item.MaCauHoi).OrderBy(x => x.SoThuTu).ToList());
            }
            return View(cauhois);
        }

        [Route("ket-qua-thi-thu-60-cau-hoi-diem-liet")]
        public ActionResult ViewResultPracticeFierceQuestion()
        {
            return RedirectToAction("PracticeFierceQuestion");
        }

        [Route("ket-qua-thi-thu-60-cau-hoi-diem-liet")]
        [HttpPost]
        public async Task<ActionResult> ViewResultPracticeFierceQuestion(FormCollection frm)
        {
            List<int> selectAnswer = new List<int>();
            List<CauHoi> cauHois = CauHoiLiets.OrderBy(x => x.MaCauHoi).ToList();
            try
            {
                cauHois.ForEach(x =>
                {
                    if (frm["selectAnswer_" + x.MaCauHoi] == null)
                    {
                        throw new Exception("selectAnswer_" + x.MaCauHoi + " null");
                    }
                    selectAnswer.AddRange(frm["selectAnswer_" + x.MaCauHoi].Split(',').Select(y => int.Parse(y.Trim())).ToList());
                });
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            List<CauHoi> danhSachCauHoiTraLoiDung = new List<CauHoi>();
            foreach (int answerId in selectAnswer)
            {
                DapAn dapAn = await db.DapAns.FindAsync(answerId);
                if (dapAn == null)
                {
                    throw new Exception("Đáp án " + answerId + " null");
                }
                try
                {
                    if (dapAn.DapAnDung == true)
                    {
                        if (danhSachCauHoiTraLoiDung.Any(x => x.MaCauHoi == dapAn.MaCauHoi) == false)
                        {
                            CauHoi cauHoiLiet = cauHois.FirstOrDefault(x => x.MaCauHoi == dapAn.MaCauHoi);
                            if(cauHoiLiet == null)
                            {
                                throw new Exception("cau hoi liet null");
                            }
                            danhSachCauHoiTraLoiDung.Add(cauHoiLiet);
                        }
                    }
                    
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            try
            {
                ViewBag.TongCauHoi = cauHois.Count;
                ViewBag.TongCauDung = danhSachCauHoiTraLoiDung.Count;
            }
            catch
            {
                throw new Exception("Lỗi số 3");
            }
            ViewBag.TongCauSai = ViewBag.TongCauHoi - ViewBag.TongCauDung;
            Session["frm"] = frm;
            return View();
        }

        [Route("xem-cau-tra-loi-sai")]
        public async Task<ActionResult> ViewWrongAnswer(string category)
        {
            ViewBag.Category = category;
            FormCollection frm = Session["frm"] as FormCollection;
            List<CauHoi> cauHois = new List<CauHoi>();
            foreach (string sMaCauHoi in frm["CauHois"].Split(','))
            {
                int maCauHoi = int.Parse(sMaCauHoi);
                CauHoi ch = await db.CauHois.FindAsync(maCauHoi);
                cauHois.Add(ch);
            }
            List<int> selectAnswer = new List<int>();
            Dictionary<int, List<int>> cauHoiDapAnDung = new Dictionary<int, List<int>>();
            cauHois.ForEach(x =>
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
            return View(cauHois);
        }

        [Route("xem-cau-tra-loi-cau-hoi-diem-liet-sai")]
        public async Task<ActionResult> ViewWrongAnswerFierceQuestion()
        {
            FormCollection frm = Session["frm"] as FormCollection;
            List<CauHoi> cauHois = CauHoiLiets.ToList();
            List<int> selectAnswer = new List<int>();
            Dictionary<int, List<int>> cauHoiDapAnDung = new Dictionary<int, List<int>>();
            cauHois.ForEach(x =>
            {
                selectAnswer.AddRange(frm["selectAnswer_" + x.MaCauHoi].Split(',').Select(y => int.Parse(y.Trim())).ToList());
                cauHoiDapAnDung.Add(x.MaCauHoi, db.DapAns.Where(y => y.MaCauHoi == x.MaCauHoi && y.DapAnDung == true).Select(y => y.MaDapAn).ToList());
            });

            List<CauHoi> danhSachCauHoiTraLoiDung = new List<CauHoi>();
            foreach (int answerId in selectAnswer)
            {
                DapAn dapAn = await db.DapAns.FindAsync(answerId);
                if (dapAn.DapAnDung == true)
                {
                    if (danhSachCauHoiTraLoiDung.Any(x => x.MaCauHoi == dapAn.MaCauHoi) == false)
                    {
                        CauHoi cauHoiLiet = cauHois.FirstOrDefault(x => x.MaCauHoi == dapAn.MaCauHoi);
                        danhSachCauHoiTraLoiDung.Add(cauHoiLiet);
                    }
                }
            }
            ViewBag.SelectAnswer = selectAnswer;
            ViewBag.danhSachCauHoiTraLoiDung = danhSachCauHoiTraLoiDung;
            ViewBag.cauHoiDapAnDung = cauHoiDapAnDung;
            ViewBag.CountAnswer = db.DapAns.ToList().Where(x => selectAnswer.Contains(x.MaDapAn)).Select(x => x.MaCauHoi).Distinct().Count();
            ViewBag.DapAnsCauHoiLiet = new Dictionary<int, List<DapAn>>();
            foreach (CauHoi item in cauHois)
            {
                (ViewBag.DapAnsCauHoiLiet as Dictionary<int, List<DapAn>>).Add(item.MaCauHoi, db.DapAns.Where(x => x.MaCauHoi == item.MaCauHoi).OrderBy(x => x.SoThuTu).ToList());
            }
            return View(cauHois);
        }


        [Route("gioi-thieu")]
        public ViewResult About()
        {
            return View(db.ThongTinChungs.First());
        }

        [Route("lien-he")]
        public ViewResult Contact()
        {
            return View(db.ThongTinChungs.First());
        }

    }
}