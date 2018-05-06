using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BikeStore.Models;
using System.Web.Security;

namespace BikeStore.Controllers
{
    public class AccountController : Controller
    {
        private StoreDbContext db = new StoreDbContext();
        public ViewResult Login()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Products");
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel lf)
        {
            if(ModelState.IsValid)
            {
                string  hashedPassword = BikeStore.Models.User.CalculateHash(lf.Password);
                var user = db.Users.Where(u => u.Email == lf.Email && u.Password == hashedPassword).FirstOrDefault();
                    
                if(user != null)
                {
                    int timeout = 525600; // 525600 min = 1 year
                    var ticket = new FormsAuthenticationTicket(lf.Email, true, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);
                    Session["UserId"] = user.Id;
                    return RedirectToAction("Index", "Products");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

    }
}
