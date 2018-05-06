using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BikeStore.Models;

namespace BikeStore.Controllers
{
    [Authorize]
    public class VendorsController : Controller
    {
        private StoreDbContext db = new StoreDbContext();
       
        //
        // GET: /Vendors/

        public ActionResult Index()
        {
            return View(db.Vendors.ToList());
        }

        //
        // GET: /Vendors/Details/5

        public ActionResult Details(int id = 0)
        {
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        //
        // GET: /Vendors/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Vendors/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                db.Vendors.Add(vendor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vendor);
        }

        //
        // GET: /Vendors/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        //
        // POST: /Vendors/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendor);
        }

        //
        // GET: /Vendors/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        //
        // POST: /Vendors/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vendor vendor = db.Vendors.Find(id);
            db.Vendors.Remove(vendor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}