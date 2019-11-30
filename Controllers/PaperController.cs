using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheResearcher.Models;

namespace TheResearcher.Controllers
{
    
    public class PaperController : Controller
    {
        // GET: Paper
        TheResearcherDBEntities _db = new TheResearcherDBEntities();
        public ActionResult Index()
        {
            tblUser tu = _db.tblUsers.Where(x => x.Username == User.Identity.Name).FirstOrDefault();
            List<tblPaper> tp = _db.tblPapers.Where(m=>m.UserID==tu.UserID).ToList();
            return View(tp);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tblPaper tp)
        {
            HttpPostedFileBase fup = Request.Files["Pdf"];
            tblUser tb = _db.tblUsers.Where(m => m.Username == User.Identity.Name).FirstOrDefault();
            
            if (fup != null)
            {
                tp.Pdf = fup.FileName;
                fup.SaveAs(Server.MapPath("~/PDFS/" + fup.FileName));
            }
            tp.UserID = tb.UserID;
            tp.Date = DateTime.Now.Date;
            _db.tblPapers.Add(tp);
            if (_db.SaveChanges() > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            tblPaper tb = _db.tblPapers.Where(u => u.PaperID == id).FirstOrDefault();
            return View(tb);
        }
        [HttpPost]
        public ActionResult Edit(tblPaper bd)
        {
            tblPaper tb = _db.tblPapers.Where(u => u.PaperID == bd.PaperID).FirstOrDefault();

            tb.Title = bd.Title;
            tb.Abstract = bd.Abstract;
            HttpPostedFileBase fup = Request.Files["pdf"];
            if (fup != null)
            {
                if (fup.FileName != "")
                {
                    System.IO.File.Delete(Server.MapPath("~/PDFS/" + bd.Pdf));

                    tb.Pdf = fup.FileName;
                    fup.SaveAs(Server.MapPath("~/PDFS/" + fup.FileName));


                }
                else
                {
                    tb.Pdf = bd.Pdf;
                }
            }
            if (_db.SaveChanges() > 0)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
        public ActionResult ForumView(int ID)
        {
            List<tblForum> tf = _db.tblForums.Where(m=>m.PaperID==ID).ToList();
            return PartialView("Forum",tf);
        }

        static int Id;
        public ActionResult ForumComment(int id)
        {
            Id = id;
            
            return PartialView("ForumComment");
        }
        [HttpPost]
        public ActionResult ForumComment(tblForum tf)
        {
            if (tf.Comment != null)
            {
                tf.PaperID = Id;
                tblUser tu = _db.tblUsers.Where(x => x.Username == User.Identity.Name).FirstOrDefault();
                tf.UserID = tu.UserID;
                _db.tblForums.Add(tf);
                _db.SaveChanges();
            }
            return RedirectToAction("Details", "Home", new { id=Id});
        }
    }
}