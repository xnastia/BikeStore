using BikeStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Text;

namespace BikeStore.Controllers
{
    public class CartController : Controller
    {
        private StoreDbContext db = new StoreDbContext();
public ViewResult Index(Cart cart)
        {
            return View(cart);
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId)
        {
            Product product = db.Products.FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", "Products");
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId)
        {
            Product product = db.Products.FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, Order order)
        {
            if(cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if(ModelState.IsValid)
            {
                StringBuilder details =new StringBuilder();
                foreach (var line in cart.Lines){
                    details.Append(line.ToString());
                }
                details.Append(string.Format("Total - {0}", cart.ComputeTotalValue()));
                order.OrderDetails = details.ToString();

                db.Orders.Add(order);
                db.SaveChanges();
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(order);
            }
        }
    }

    }
