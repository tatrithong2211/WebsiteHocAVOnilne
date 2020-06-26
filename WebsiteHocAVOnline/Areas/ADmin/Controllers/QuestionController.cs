using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteHocAVOnline.Models;

namespace WebsiteHocAVOnline.Areas.ADmin.Controllers
{
    public class QuestionController : Controller
    {
        // GET: ADmin/Question
        DBThiTrucTuyenEntities db = new DBThiTrucTuyenEntities();
        public ActionResult Index(int page = 1, int pageSize = 8)
        {
            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("Login");
            }

            Question cauhoi = new Question();

            var model = cauhoi.NumberPagePagination(page, pageSize);

            return View(model);
        }

        public ActionResult ViewQuestion(int id)
        {
            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("Login");
            }

            return View(db.CauHois.SingleOrDefault(model => model.IDCauHoi == id));
        }

        public ActionResult SortQuestion()
        {

            List<Category> list = db.Categories.ToList();

            ViewBag.List = new SelectList(list, "ID_Category", "TênCategory");

            return View();
        }

        [HttpGet]
        public ActionResult AddQuestion()
        {
            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("Login");
            }

            List<Category> list = db.Categories.ToList();

            ViewBag.List = new SelectList(list, "ID_Category", "TênCategory");

            return View();
        }

        [HttpPost]
        public ActionResult AddQuestion(CauHoi queGet)
        {

            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("Login");
            }

            List<Category> list = db.Categories.ToList();

            ViewBag.List = new SelectList(list, "ID_Category", "TênCategory");

            try
            {
                db.CauHois.Add(queGet);

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DelQuestion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DelQuestion(int id)
        {
            CauHoi _id = db.CauHois.Find(id);

            db.CauHois.Remove(_id);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditQuestion(int id)
        {

            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("Login");
            }

            List<Category> clist = db.Categories.ToList();

            ViewBag.List = new SelectList(clist, "ID_Category", "TênCategory");

            var lst = db.CauHois.Where(model => model.IDCauHoi == id).FirstOrDefault();

            return View(lst);
        }

        [HttpPost]
        public ActionResult EditQuestion(CauHoi qGet)
        {

            List<Category> list = db.Categories.ToList();

            ViewBag.List = new SelectList(list, "ID_Category", "TênCategory");

            var qList = db.CauHois.Where(model => model.IDCauHoi == qGet.IDCauHoi).FirstOrDefault();

            qList.IDCauHoi = qGet.IDCauHoi;

            qList.CauHoi1 = qGet.CauHoi1;

            qList.A = qGet.A;

            qList.B = qGet.B;

            qList.C = qGet.C;

            qList.D = qGet.D;

            qList.DanAnDung = qGet.DanAnDung;

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}