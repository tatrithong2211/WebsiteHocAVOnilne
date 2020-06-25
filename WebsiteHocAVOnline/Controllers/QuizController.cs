using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteHocAVOnline.Models;

namespace WebsiteHocAVOnline.Controllers
{

    public class QuizController : Controller
    {
        
         DBThiTrucTuyenEntities db = new DBThiTrucTuyenEntities();

        public ActionResult ConfirmUser()
        {
            if (Session["Tai Khoan"] != null)
            {
                return RedirectToAction("SelectIndex");
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]

        public ActionResult SelectIndex()
        {
            List<Category> list = db.Categories.ToList();

            ViewBag.List = new SelectList(list, "ID_Category", "TênCategory");
            ViewBag.ListUser = db.TaiKhoans.ToList();

            return View();

        }
        [HttpPost]
        public ActionResult SelectIndex (int catGet)
        {
            List<Category> catList = db.Categories.ToList();

            ViewBag.ListUser = db.TaiKhoans.ToList();

            foreach (var item in catList)
            {
                if (item.ID_Category == catGet)
                {

                    List<CauHoi> li = db.CauHois.Where(model => model.ID_Category== item.ID_Category).ToList();

                    Queue<CauHoi> queque = new Queue<CauHoi>();

                    foreach (CauHoi a in li)
                    {

                        queque.Enqueue(a);

                    }

                    TempData["examid"] = item.ID_Category;

                    TempData["questions"] = queque;

                    TempData["loadQuestions"] = null;

                    TempData["score"] = 0;

                    TempData.Keep();

                    return RedirectToAction("Quiz");
                }
                else
                {
                    ViewBag.Error = "";
                }
            }

            return View();
        }
        [HttpGet]
        public ActionResult Quiz()
        {

            ViewBag.ListUser = db.TaiKhoans.ToList();
            CauHoi q = null;

            if (TempData["questions"] != null)
            {
                Queue<CauHoi> qlist = (Queue<CauHoi>)TempData["questions"];

                if (qlist.Count > 0)
                {
                    q = qlist.Peek();

                    if (TempData["loadQuestions"] != null)
                    {
                        qlist.Dequeue();
                        if (qlist.Count > 0)
                        {
                            q = qlist.Peek();
                        }
                        else
                        {
                            return RedirectToAction("EndExam");
                        }
                    }

                    TempData["questions"] = qlist;

                    TempData["DapAnDung"] = q.DanAnDung;

                    return View(q);
                }
                else
                {
                    return RedirectToAction("EndExam");
                }

            }
            else
            {
                return RedirectToAction("SelectIndex");
            }
        }
        [HttpPost]
        public ActionResult Quiz(CauHoi q)
        {
            q.DanAnDung = TempData["DapAnDung"].ToString();
            string dem = null;

            if (q.A != null)
            {

                dem = "A";

            }
            else if (q.B != null)
            {

                dem = "B";

            }
            else if (q.C != null)
            {

                dem = "C";

            }
            else if (q.D != null)
            {

                dem = "D";

            }

            if (dem != null)
            {
                if (dem.Equals(q.DanAnDung))
                {
                    TempData["score"] = Convert.ToInt32(TempData["score"]) + 10;
                }
            }

            TempData["loadQuestions"] = 0;

            TempData.Keep();

            return RedirectToAction("Quiz");
        }

        public ActionResult EndExam()

        {
            ViewBag.ListUser = db.TaiKhoans.ToList();

            return View();
        }
        [HttpGet]
        public ActionResult EditExam(int id)
        {
            ViewBag.ListUser = db.TaiKhoans.ToList();

            return View(db.CauHois.Where(model => model.ID_Category == id).ToList());
        }
    }
}