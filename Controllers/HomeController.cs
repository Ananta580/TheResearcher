using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheResearcher.Models;

namespace TheResearcher.Controllers
{
    public class HomeController : Controller
    {
        TheResearcherDBEntities _db = new TheResearcherDBEntities();
        public ActionResult Index()
        {
            List<tblPaper> tp = _db.tblPapers.ToList();

            return View(tp);
        }
        public ActionResult Details(int id)
        {
            tblPaper tb = _db.tblPapers.Where(u => u.PaperID == id).FirstOrDefault();
            return View(tb);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Search()
        {
            return PartialView("_Search");
        }
        [HttpPost]
        public ActionResult Search(tblForum sr)
        {
            List<tblPaper> lst = _db.tblPapers.Where(x => x.Title.Contains(sr.Comment) || x.Abstract.Contains(sr.Comment) || x.tblUser.Name.Contains(sr.Comment)).ToList();
            return View("Search",lst);
        }
    }
}