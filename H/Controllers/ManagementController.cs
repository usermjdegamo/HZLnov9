using H.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H.Controllers
{
    public class ManagementController : Controller
    {
        Database1Entities db = new Database1Entities();
        // GET: Management
        public ActionResult Category()
        {
            var data = db.Categories.ToList();
            List<Category> newdata = data;
            return View(newdata);
        }
        [HttpGet]
        public ActionResult CategoryAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CategoryAdd(Category ba)
        {
            db.Categories.Add(ba);
            db.SaveChanges();

            return View();
        }
        public ActionResult CategoryDetail(int id)
        {
            var db2 = db.Categories.Where(x => x.id == id).FirstOrDefault();
            ViewBag.categoryProfile = db2.imageLink;
            ViewBag.categoryName = db2.name;
            ViewBag.categoryDescription = db2.description;
            ViewBag.categoryID = db2.id;

            return View();
        }
        public ActionResult CategoryEdit(int id)
        {
            var db2 = db.Categories.Where(x => x.id == id).FirstOrDefault();
            ViewBag.categoryProfile = db2.imageLink;
            ViewBag.categoryName = db2.name;
            ViewBag.categoryDescription = db2.description;
            ViewBag.categoryID = db2.id;
            ViewBag.categoryUserID = db2.userID;

            return View();
        }
        [HttpPost]
        public ActionResult CategoryEdit(int id, Category ba)
        {
            var db2 = db.Categories.Where(x => x.id == id).FirstOrDefault();

            db2.description = ba.description;
            db2.name = ba.name;
            db2.imageLink = ba.imageLink;
            db2.userID = ba.userID;
            db.SaveChanges();

            return RedirectToAction("Category", "Management");
        }
        public ActionResult CategoryDelete(int id)
        {
            var db2 = db.Categories.Where(x => x.id == id).FirstOrDefault();
            db.Categories.Remove(db2);
            db.SaveChanges();

            return RedirectToAction("Category", "Management");
        }
        ///////////////////////////////////////////////
        public ActionResult Product()
        {
            var data = db.Products.ToList();
            List<Product> newdata = data;
            //IEnumerable<Product> newdata = data;
            return View(newdata);
        }
        [HttpGet]
        public ActionResult ProductAdd()
        {
            var list = db.Categories.ToList();
            ViewBag.categoryName = new SelectList(list, "name", "name");
            return View();
        }

        [HttpPost]
        public ActionResult ProductAdd(Product ba, string categoryName)
        {
            var list = db.Categories.ToList();
            ViewBag.categoryName = new SelectList(list, "name", "name");

            if (ModelState.IsValid)
            {
                var juju = db.Categories.Where(x => x.name == categoryName).FirstOrDefault();
                ba.categoryID = juju.id;
                db.Products.Add(ba);
                db.SaveChanges();
            }

            return View();
        }
        public ActionResult ProductDetail(int id)
        {
            var db2 = db.Products.Where(x => x.id == id).FirstOrDefault();
            ViewBag.ProductProfile = db2.imageLink;
            ViewBag.ProductName = db2.name;
            ViewBag.ProductDescription = db2.description;
            ViewBag.ProductID = db2.id;

            return View();
        }
        public ActionResult ProductEdit(int id)
        {
            var list = db.Categories.ToList();
            ViewBag.categoryName = new SelectList(list, "name", "name");

            var db2 = db.Products.Where(x => x.id == id).FirstOrDefault();
            ViewBag.ProductImageLink = db2.imageLink;
            ViewBag.ProductName = db2.name;
            ViewBag.ProductDescription = db2.description;
            ViewBag.ProductID = db2.id;
            ViewBag.ProductUserID = db2.userID;
            ViewBag.ProductPrice = db2.price;
            ViewBag.ProductCategoryName = db2.Category.name;

            return View();
        }
        [HttpPost]
        public ActionResult ProductEdit(int id, Product ba, string categoryName)
        {
            var list = db.Categories.ToList();
            ViewBag.categoryName = new SelectList(list, "name", "name");
            var lol = db.Categories.Where(x => x.name == categoryName).FirstOrDefault();

            var db2 = db.Products.Where(x => x.id == id).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db2.name = ba.name;
                db2.description = ba.description;
                db2.price = ba.price;
                db2.imageLink = ba.imageLink;
                db2.userID = ba.userID;
                if (categoryName != "")
                {
                    db2.categoryID = lol.id;
                }
                db.SaveChanges();
            }

            return RedirectToAction("Product", "Management");
        }
        public ActionResult ProductDelete(int id)
        {
            //var db2 = db.Categories.Where(x => x.id == id).FirstOrDefault();
            //db.Categories.Remove(db2);
            //db.SaveChanges();

            var db2 = db.Products.Where(x => x.id == id).FirstOrDefault();
            db.Products.Remove(db2);
            db.SaveChanges();

            return RedirectToAction("Product", "Management");
        }
        ////////////////////////////////
        public ActionResult Order()
        {
            int ahaha = Convert.ToInt32(Session["userID"].ToString());
            var data = db.Orders.Where(x => x.userID == ahaha && x.served == 0).ToList();
            //var data = db.Orders.ToList();
            List<Order> newdata = data;
            //IEnumerable<Product> newdata = data;
            return View(newdata);
        }

        public ActionResult OrderDone()
        {

            int ahaha = Convert.ToInt32(Session["userID"].ToString());
            int ha = Convert.ToInt32(Session["userRole"].ToString());

            var data = db.Orders.ToList();

            if (ha == 1)
            {
                data = db.Orders.Where(x => x.userID == ahaha && x.served == 1).ToList();
            }
            else if (ha == 0)
            {
                data = db.Orders.Where(x => x.served == 1).ToList();
            }

            List<Order> newdata = data;

            return View(newdata);


            //var data = db.Orders.Where(x => x.userID == ahaha && x.served == 1).ToList();
            //List<Order> newdata = data;
            //return View(newdata); asdasdas




            //int ahaha = Convert.ToInt32(Session["userID"].ToString());
            //int ha = Convert.ToInt32(Session["userRole"].ToString());

            //var data = db.Orders.Where(x => x.userID == ahaha && x.served == 1).ToList();
            //List<Order> newdata = data;
            //return View(newdata);
        }

        public ActionResult OrderAdd(int prodID, Order o)
        {
            int useridako = Convert.ToInt32(Session["userID"].ToString());
            var dhaha = db.Orders.Where(x => x.productID == prodID && x.userID == useridako && x.served == 0).FirstOrDefault();

            if(dhaha != null)
            {
                dhaha.quantity++;
                db.SaveChanges();
            }
            else
            {
                o.productID = prodID;
                o.userID = useridako;
                o.quantity = 1;
                o.served = 0;
                db.Orders.Add(o);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult OrderBuy(int ordID)
        {
            int useridako = Convert.ToInt32(Session["userID"].ToString());
            var dhaha = db.Orders.Where(x => x.id == ordID && x.userID == useridako).FirstOrDefault();
            if (dhaha != null)
            {
                dhaha.User.balance = dhaha.User.balance - dhaha.Product.price * dhaha.quantity;
                dhaha.served = 1;
                db.SaveChanges();
                //ordIDs
            }

            return RedirectToAction("Order", "Management");
        }

        public ActionResult OrderBuyBulk(string ara/*int[] id*/)
        {
            string haha = ara;
            int[] dakpan = haha.Split(',').Select(n => Convert.ToInt32(n)).ToArray();

            int useridako = Convert.ToInt32(Session["userID"].ToString());

            foreach (var i in dakpan)
            {
                int temp = Convert.ToInt32(i);
                var dhaha = db.Orders.Where(x => x.id == temp && x.userID == useridako).FirstOrDefault();
                if (dhaha != null)
                {
                    dhaha.User.balance = dhaha.User.balance - dhaha.Product.price * dhaha.quantity;
                    dhaha.served = 1;
                    db.SaveChanges();
                    //ordIDs
                }
            }

            return RedirectToAction("Order", "Management");
        }

        public ActionResult OrderDelete(int id)
        {
            var db2 = db.Orders.Where(x => x.id == id).FirstOrDefault();
            db.Orders.Remove(db2);
            db.SaveChanges();

            return RedirectToAction("Order", "Management");
        }
    }
}