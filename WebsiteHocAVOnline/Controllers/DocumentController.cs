using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteHocAVOnline.Models;

namespace WebsiteHocAVOnline.Controllers
{
    public class DocumentController : Controller
    {
        // GET: Document

        DBThiTrucTuyenEntities db = new DBThiTrucTuyenEntities();
        public ActionResult Index()
        {
            return View(db.TaiLieux.ToList());

        }
    }
}