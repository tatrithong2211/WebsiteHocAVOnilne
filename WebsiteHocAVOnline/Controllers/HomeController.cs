using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteHocAVOnline.Models;

namespace WebsiteHocAVOnline.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        DBThiTrucTuyenEntities db = new DBThiTrucTuyenEntities();

        public ActionResult Index()
        {
            ViewBag.ListUser = db.TaiKhoans.ToList();

            return View(db.Learnings.ToList());
        }

        public ActionResult MenuLesson()
        {
            return PartialView(db.Categories.ToList());
        }
        public ActionResult Lesson(int id)
        {
            ViewBag.ListUser = db.TaiKhoans.ToList();

            var ID = db.Categories.SingleOrDefault(model => model.ID_Category == id);

            ViewBag.name = ID.TênCategory;

            List<Learning> lst = db.Learnings.Where(model => model.ID_Category == id).ToList();

            return View(lst);
        }

        public ViewResult DetailLesson(int id)
        {
            ViewBag.ListUser = db.TaiKhoans.ToList();

            ViewBag.List = db.Learnings.ToList();

            ViewBag.ListCategory = db.Categories.ToList();

            Learning learning = db.Learnings.SingleOrDefault(model => model.ID_BaiHoc == id);

            return View(learning);
        }

        public ViewResult Introduce()
        {
            return View();
        }
    }
}