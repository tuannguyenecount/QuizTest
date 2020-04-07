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

        [Route("450-cau-hoi-on-tap-sat-hach-lai-xe")]
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
            List<CauHoiLiet> cauHoiLiets = db.CauHoiLiets.OrderBy(x => x.MaCauHoi).ToList();
            cauHoiLiets.ForEach(x =>
            {
                cauHoiDapAnDung.Add(x.MaCauHoi, x.DapAnCauHoiLiets.Where(y => y.DapAnDung == true).Select(y => y.MaDapAn).ToList());
            });
            ViewBag.cauHoiDapAnDung = cauHoiDapAnDung;
            return View(cauHoiLiets);
        }

        void Swap(CauHoi a, CauHoi b)
        {
            CauHoi temp = a;
            a = b;
            b = temp;
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

        [Route("de-thi-sat-hach-lai-xe")]
        public ActionResult Practice()
        {
            List<CauHoi> cauhois = new List<CauHoi>();
            Random random = new Random();

            List<CauHoi> lstToAdd = new List<CauHoi>();

            lstToAdd = db.CauHois.Where(x => x.SuDung == true && x.MaCauHoiMoi >= 1 && x.MaCauHoiMoi <= 21).ToList();
            ShuffleArray(ref lstToAdd);
            lstToAdd = lstToAdd.Skip(random.Next(0, lstToAdd.Count - 1)).Take(1).ToList();
            cauhois.AddRange(lstToAdd); // lấy 1 câu phần 1

            lstToAdd = db.CauHois.Where(x => x.SuDung == true && x.MaCauHoiMoi >= 22 && x.MaCauHoiMoi <= 131).ToList();
            ShuffleArray(ref lstToAdd);
            lstToAdd = lstToAdd.Skip(random.Next(0, lstToAdd.Count - 7)).Take(7).ToList();
            cauhois.AddRange(lstToAdd); // lấy 7 câu phần 1

            lstToAdd = db.CauHois.Where(x => x.SuDung == true && x.MaCauHoiMoi >= 132 && x.MaCauHoiMoi <= 145).ToList();
            ShuffleArray(ref lstToAdd);
            lstToAdd = lstToAdd.Skip(random.Next(0, lstToAdd.Count - 1)).Take(1).ToList();
            cauhois.AddRange(lstToAdd); // lấy 1 câu phần 1

            lstToAdd = db.CauHois.Where(x => x.SuDung == true && x.MaCauHoiMoi >= 146 && x.MaCauHoiMoi <= 175).ToList();
            ShuffleArray(ref lstToAdd);
            lstToAdd = lstToAdd.Skip(random.Next(0, lstToAdd.Count - 1)).Take(1).ToList();          
            cauhois.AddRange(lstToAdd); // lấy 1 câu phần 2

            lstToAdd = db.CauHois.Where(x => x.SuDung == true && x.MaCauHoiMoi >= 176 && x.MaCauHoiMoi <= 200).ToList();
            ShuffleArray(ref lstToAdd);
            lstToAdd = lstToAdd.Skip(random.Next(0, lstToAdd.Count - 1)).Take(1).ToList();
            cauhois.AddRange(lstToAdd); // lấy 1 câu phần 3

            lstToAdd = db.CauHois.Where(x => x.SuDung == true && x.MaCauHoiMoi >= 201 && x.MaCauHoiMoi <= 255).ToList();
            ShuffleArray(ref lstToAdd);
            lstToAdd = lstToAdd.Skip(random.Next(0, lstToAdd.Count - 1)).Take(1).ToList(); 
            cauhois.AddRange(lstToAdd); // lấy 1 câu phần 4,5

            lstToAdd = db.CauHois.Where(x => x.SuDung == true && x.MaCauHoiMoi >= 256 && x.MaCauHoiMoi <= 355).ToList();
            ShuffleArray(ref lstToAdd);
            lstToAdd = lstToAdd.Skip(random.Next(0, lstToAdd.Count - 9)).Take(9).ToList();
            cauhois.AddRange(lstToAdd); // lấy 9 câu phần 6

            lstToAdd = db.CauHois.Where(x => x.SuDung == true && x.MaCauHoiMoi >= 356 && x.MaCauHoiMoi <= 450).ToList();
            ShuffleArray(ref lstToAdd);
            lstToAdd = lstToAdd.Skip(random.Next(0, lstToAdd.Count - 9)).Take(9).ToList();          
            cauhois.AddRange(lstToAdd); // lấy 9 câu phần 7

            cauhois = cauhois.Where(x => x.DapAns != null && x.DapAns.Count > 0 && x.DapAns.Any(y => y.DapAnDung == true)).ToList();
            
            //Session["CauHois"] = cauhois;

            ViewBag.SelectAnswer = new List<int>();
            ViewBag.danhSachCauHoiTraLoiDung = new List<CauHoi>();
            ViewBag.cauHoiDapAnDung = new Dictionary<int, List<int>>();
            ViewBag.CountAnswer = 0;
            ViewBag.CauHois = GetListIdQuestion(cauhois);
            return View(cauhois);
        }

        [Route("ket-qua-thi-sat-hach-lai-xe")]
        public ActionResult ViewResult()
        {
            return RedirectToAction("Practice");
        }

        [Route("ket-qua-thi-sat-hach-lai-xe")]
        [HttpPost]
        public async Task<ActionResult> ViewResult(FormCollection frm)
        {
            List<int> selectAnswer = new List<int>();
            List<CauHoi> cauHois = new List<CauHoi>();
            foreach(string sMaCauHoi in frm["CauHois"].Split(','))
            {
                int maCauHoi = int.Parse(sMaCauHoi);
                CauHoi ch = await db.CauHois.FindAsync(maCauHoi);
                cauHois.Add(ch);
            }
            try
            {

                cauHois.ForEach(x =>
                {
                    if(frm["selectAnswer_" + x.MaCauHoiMoi] == null)
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

        [Route("xem-cau-tra-loi-sai")]
        public async Task<ActionResult> ViewWrongAnswer()
        {
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