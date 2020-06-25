using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteHocAVOnline.Models;
using System.IO;
using System.Web.UI.WebControls;

namespace WebsiteHocAVOnline.Areas.ADmin.Controllers
{
    public class DocumentController : Controller
    {
        // GET: ADmin/Document
        DBThiTrucTuyenEntities db = new DBThiTrucTuyenEntities();
        public ActionResult Index(int page = 1, int pagesize = 8)
        {
            Document taiLieu = new Document();

            var model = taiLieu.NumberPagePagination(page, pagesize);

            return View(model);
        }


        [HttpGet]
        public ActionResult AddDoc()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDoc(TaiLieu docGet, HttpPostedFileBase imgfile)
        {
            TaiLieu doc = new TaiLieu();
            try
            {
                doc.TenTaiLieu = docGet.TenTaiLieu;

                doc.DownLoad = docGet.DownLoad;

                doc.Image = uploadImg(imgfile);

                db.TaiLieux.Add(doc);

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {

            }

            return View();
        }

        public string uploadImg(HttpPostedFileBase imgfile)
        {
            string path = "-1";

            try
            {
                if (imgfile != null && imgfile.ContentLength > 0)
                {

                    string extension = Path.GetExtension(imgfile.FileName);

                    if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".png") || extension.ToLower().Equals(".jpeg"))
                    {
                        Random random = new Random();

                        path = Path.Combine(Server.MapPath("~/Assets/Dcimg/img"), Path.GetFileName(imgfile.FileName));

                        imgfile.SaveAs(path);

                        path = Path.GetFileName(imgfile.FileName);

                    }

                }
                else
                {

                }
            }
            catch (Exception)
            {

            }

            return path;

        }


        [HttpGet]
        public ActionResult EditDocument(int id)
        {

            var lst = db.TaiLieux.Where(model => model.IDTaiLieu == id).FirstOrDefault();

            return View(lst);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditDocument(TaiLieu leGet)
        {

            try
            {

                var leList = db.TaiLieux.Where(model => model.IDTaiLieu == leGet.IDTaiLieu).FirstOrDefault();

                leList.IDTaiLieu = leGet.IDTaiLieu;

                leList.DownLoad= leGet.DownLoad;

                leList.Image = leGet.Image;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}