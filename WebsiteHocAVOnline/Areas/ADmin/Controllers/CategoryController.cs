using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteHocAVOnline.Models;

namespace WebsiteHocAVOnline.Areas.ADmin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: ADmin/Category
        DBThiTrucTuyenEntities db = new DBThiTrucTuyenEntities();

        #region Category_baihoc
        public ActionResult CategoryIndex()
        {
            return View(db.Categories.ToList());
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category catGet)
        {
            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("Login");
            }

            List<Category> list = db.Categories.OrderByDescending(model => model.ID_Category).ToList();

            Category catVar = new Category(); 

            catVar.TênCategory = catGet.TênCategory;

            catVar.ID_Category = Convert.ToInt32(Session["ADMIN"].ToString());

            db.Categories.Add(catVar);

            db.SaveChanges();

            return RedirectToAction("CategoryIndex");
        }

        [HttpGet]
        public ActionResult DelCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DelCategory(int id)
        {
            Category _id = db.Categories.Find(id);

            db.Categories .Remove(_id);

            db.SaveChanges();

            return RedirectToAction("CategoryIndex");
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("Login");
            }

            return View(db.Categories.Where(model => model.ID_Category == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult EditCategory(Category catGet)
        {
            Category catList = db.Categories.Where(model => model.ID_Category == catGet.ID_Category).SingleOrDefault();

            catList.TênCategory = catGet.TênCategory;

            db.SaveChanges();

            return RedirectToAction("CategoryIndex");
        }

#endregion
    }
}