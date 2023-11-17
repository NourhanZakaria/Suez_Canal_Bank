using suezCanalBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace suezCanalBank.Controllers
{
    public class UserController : Controller
    {

        UserLoginEntities db = new UserLoginEntities();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View("userSignUp");
        }

        [HttpPost]
        public ActionResult SignUp(user userInfo)
        {
            if (db.users.Any(info => info.userName == userInfo.userName))
            {
                ViewBag.Notification = "this account has already exist";
                return View("userSignUp");
            }
            else
            {

                db.users.Add(userInfo);
               // Session["userName"] = userInfo.userName.ToString();
                db.SaveChanges();

                

                return RedirectToAction("Login", "Home");

            }


        }





    }
}