using H.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H.App_Data
{
    public class AccountController : Controller
    {
        Database1Entities db = new Database1Entities();
        // GET: Account
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User users)
        {
            //var data = db.Users.Where(x => x.password == users.password).FirstOrDefault();
            //if(data != null)
            //{
            //    return Content("<script language='javascript' type='text/javascript'>alert('This password has been already used by '+data.username);</script>");
            //}

            if (ModelState.IsValid)
            {
                users.roleID = 1;
                users.balance = 0;
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                return View();
            }
        }

        public ActionResult SignUpAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUpAdmin(User users)
        {
            //var data = db.Users.Where(x => x.password == users.password).FirstOrDefault();
            //if(data != null)
            //{
            //    return Content("<script language='javascript' type='text/javascript'>alert('This password has been already used by '+data.username);</script>");
            //}

            if (ModelState.IsValid)
            {
                users.roleID = 0;
                users.balance = 99999;
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                return View();
            }
        }

        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(User users)
        {
            var data = db.Users.Where(x => x.username == users.username && x.password == users.password).FirstOrDefault();
            if(data != null)
            {
                Session["userID"] = data.id;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
            
        }
        public ActionResult SignOut()
        {
            Session.Clear();
            return RedirectToAction("SignIn", "Account");
        }
        public ActionResult Profile_()
        {
            if (Session["userID"] != null)
            {
                int asd = Convert.ToInt32(Session["userID"].ToString());
                var db2 = db.Users.Where(x => x.id == asd).FirstOrDefault();
                ViewBag.userID = db2.id;
                ViewBag.username = db2.username;
                ViewBag.password = db2.password;
                ViewBag.name = db2.name;
                ViewBag.gender = db2.gender;
                ViewBag.imageLink = db2.imageLink;
                ViewBag.userBalance = db2.balance;
            }

            return View();
        }
        [HttpPost]
        public ActionResult Profile_(User ba)
        {
            var db2 = db.Users.Where(x => x.id == ba.id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                db2.username = ba.username;
                db2.password = ba.password;
                db2.name = ba.name;
                db2.gender = ba.gender;
                db2.imageLink = ba.imageLink;
                db2.balance = ba.balance;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}