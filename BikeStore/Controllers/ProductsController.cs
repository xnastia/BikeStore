using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BikeStore.Models;

using PagedList.Mvc;
using PagedList;

namespace BikeStore.Controllers
{
    public class ProductsController : Controller
    {
        const int pageSize = 3;
        private StoreDbContext db = new StoreDbContext();

        //
        // GET: /Products/

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? CategoryId, int? MinPrice, int? MaxPrice, int? page)
          
    {
        var products = db.Products.Include(p => p.Category).Include(p => p.Vendor).Include(p => p.Comments);


        ViewBag.CurrentSort = sortOrder; 
        ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }
            if (CategoryId != null)
            {
                products = products.Where(s => s.CategoryId == CategoryId);
                ViewBag.CategoryId = CategoryId;
            }
            if (MinPrice != null)
            {
                products = products.Where(s => s.Price >= MinPrice);
                ViewBag.MinPrice = MinPrice;
            }
            if (MaxPrice != null)
            {
                products = products.Where(s => s.Price <= MaxPrice);
                ViewBag.MaxPrice = MaxPrice;
            }
            int pageNumber = (page ?? 1);

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "price":
                    products = products.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(s => s.Price);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }

            
            //products.Where
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Products/Details/5

        public ActionResult Details(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Comments = db.Comments.Where(c => c.ProductId == id);
            ViewBag.ProductId = id ;
            return View(product);
        }

        //
        // GET: /Products/Create

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.VendorId = new SelectList(db.Vendors, "Id", "Name");
            return View();
        }

        //
        // POST: /Products/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.VendorId = new SelectList(db.Vendors, "Id", "Name", product.VendorId);
            return View(product);
        }

        //
        // GET: /Products/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.VendorId = new SelectList(db.Vendors, "Id", "Name", product.VendorId);
            return View(product);
        }

        //
        // POST: /Products/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.VendorId = new SelectList(db.Vendors, "Id", "Name", product.VendorId);
            return View(product);
        }

        //
        // GET: /Products/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Products/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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