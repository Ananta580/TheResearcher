using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TheResearcher.Models;
using TheResearcher.Models.ViewModel;

namespace TheResearcher.Controllers
{
    
    public class AccountController : Controller
    {
        TheResearcherDBEntities db = new TheResearcherDBEntities();
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        public ActionResult Login(LoginModel l,string ReturnUrl ="")
        {
            var user = db.tblUsers.Where(x => x.Username == l.username && x.Password == l.password);
            if(user!=null)
            {
                FormsAuthentication.SetAuthCookie(l.username, l.rememberme);
                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid User");
            }
            return View("Login");
        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}