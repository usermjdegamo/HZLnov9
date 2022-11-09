using H.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H.App_Data
{
    public class HomeController : Controller
    {
        Database1Entities db = new Database1Entities();
        // GET: Home
        public ActionResult Index()
        {
            var data = db.Products.ToList();
            List<Product> newdata = data;
            if (Session["userID"] != null)
            {
                int asd = Convert.ToInt32(Session["userID"].ToString());
                
                Session["name"] = db.Users.Where(x => x.id == asd).FirstOrDefault().name;
                Session["userBalance"] = db.Users.Where(x => x.id == asd).FirstOrDefault().balance;
                return View(newdata);
            }
            return View(newdata);
        }
    }
}