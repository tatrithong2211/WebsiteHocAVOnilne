using System;
using System.Collections.Generic;
using System.Linq;
using Facebook;
using System.Web;
using System.Web.Mvc;
using BotDetect.Web.Mvc;
using WebsiteHocAVOnline.Models;
using WebsiteHocAVOnline.Common;
using System.Configuration;
using System.Web.Security;
namespace WebsiteHocAVOnline.Controllers
{
    public class AccountController : Controller
    {
        // GET : Account
        DBThiTrucTuyenEntities db = new DBThiTrucTuyenEntities();

        private Uri RedirectUri
    {
        get
        {
            var uriBuilder = new UriBuilder(Request.Url);
            uriBuilder.Query = null;
            uriBuilder.Fragment = null;
            uriBuilder.Path = Url.Action("FacebookCallback");
            return uriBuilder.Uri;
        }
    }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]

        [CaptchaValidationActionFilter("Mã Captcha", "Đăng Ký Captcha", "Sai Captcha!")]

        public ActionResult Register(RegisterModel kGet)
        {
            List<TaiKhoan> adList = db.TaiKhoans.OrderByDescending(model => model.IDTaiKhoan).ToList();

            TaiKhoan kVar = new TaiKhoan();

            kVar.TenDangNhap = kGet.TenDangNhap;

            kVar.MatKhau = Encryptor.MD5Hash(kGet.MatKhau);

            kVar.Hoten = kGet.Hoten;

            kVar.DiaChi = kGet.DiaChi;

            kVar.SDT = kGet.SDT;

            db.TaiKhoans.Add(kVar);

            db.SaveChanges();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel taiKhoan)
        {
            TaiKhoan taiKhoan1 = db.TaiKhoans.Where(m => m.TenDangNhap == taiKhoan.TenDangNhap && m.MatKhau == taiKhoan.MatKhau).FirstOrDefault();
         
            if (taiKhoan1 != null)
            {
                Session["TAI KHOAN"] = taiKhoan1.IDTaiKhoan;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Sai tài khoản hoặc mật khẩu !!";
            }
            return View();
        }

        public ActionResult LoginFaceBook()
        {
            var fb = new FacebookClient();

            var LoginUrl = fb.GetLoginUrl(new
            {
                client_id = "258535651881098",

                client_Secret = "6d1619ae01686ea228f6cba56cbf6d55",

                Redirect_uri = RedirectUri.AbsoluteUri,

                response_type = "code",

                Scope = "email",


        }) ;

            return Redirect(LoginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "258535651881098",
                client_secret ="6d1619ae01686ea228f6cba56cbf6d55",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;

            // Store the access token in the session
            Session["AccessToken"] = accessToken;

            // update the facebook client with the access token so 
            // we can make requests on behalf of the user
            fb.AccessToken = accessToken;

            // Get the user's information
            dynamic me = fb.Get("me?fields=first_name,last_name,id,email");
            string email = me.email;

            // Set the auth cookie
            FormsAuthentication.SetAuthCookie(email, false);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();

            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }
    }
}