using BikeStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BikeStore.Controllers
{
    public class CommentsController : Controller
    {
        private StoreDbContext db = new StoreDbContext();
        //
        // GET: /Comments/

        public ActionResult Index(int ProductId)
        {
            var comments = db.Comments.Where(p =>p.ProductId == ProductId);
            return View(comments.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "Products", new { id = comment.ProductId });
            }

            return RedirectToAction("Details", "Products", comment.ProductId);
        }
    }
}
