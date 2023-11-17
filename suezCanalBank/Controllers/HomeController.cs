using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using suezCanalBank.Models;

namespace suezCanalBank.Controllers
{
    public class HomeController : Controller
    {
        UserLoginEntities db = new UserLoginEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }



        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(user info)
        {
            user login = db.users.Where(user => user.userName.Equals(info.userName) && user.userPassword.Equals(info.userPassword)).FirstOrDefault();

            if (login != null)
            {
                Session["userName"] = info.userName.ToString();

                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.Notification = "There is a problem,your email or password is not correct";
            }
            return View();
        }



        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Customer customer)
        {

            if (db.Customers.Any(info => info.Name == customer.Name))
            {
                ViewBag.Notification = "this account has already exist";
                return View();
            }
            else
            {

                db.Customers.Add(customer);
                db.SaveChanges();

                //Session["userId"] = customer.userId.ToString();
                //Session["userName"] = customer.userName.ToString();
                //Session["userType"] = customer.userType.ToString();
          
                return RedirectToAction("Index", "Home");

            }

        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }





        [HttpGet]
        public ActionResult Edit(int id)
        {
            Customer userInfo = db.Customers.Single(Info => Info.Id == id);
            return View(userInfo);
        }

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(customer);
           
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            Customer userInfo = db.Customers.Single(Info => Info.Id == id);
            return View(userInfo);
        }

         [HttpPost]
         [ActionName("Delete")]
         public ActionResult confirmDelete(int id)
         {

             Customer customer = db.Customers.Single(Info => Info.Id == id);

             db.Customers.Remove(customer);
             db.SaveChanges();

             return RedirectToAction("Index", "Home");




         }
          [HttpGet]
      public ActionResult Details(int id)
        {
            Customer userInfo = db.Customers.Single(Info => Info.Id == id);
            return View(userInfo);
        }




        public JsonResult GetSuspiciousCustomers(bool isSuspicious)
        {
            // Assume db is your DbContext
            var suspiciousCustomers = db.Customers.Where(c => c.suspiciousUsers == isSuspicious).ToList();
            return Json(suspiciousCustomers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult check()
        {
            return View();
        }


        public JsonResult checkEgyption(string nationalId)
        {
            

            Customer customer = db.Customers.Where(c => c.NationalId == nationalId).FirstOrDefault();

            if (customer != null)
            {
                // Customer found, return JSON result
                return Json(customer, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // Customer not found, return an appropriate response
                return Json(new { error = "Customer not found" }, JsonRequestBehavior.AllowGet);
            }
            

            
        }


        public JsonResult checkForeigner(string passport)
        {


            Customer customer = db.Customers.Where(c => c.PassportNumber == passport).FirstOrDefault();

            if (customer != null)
            {
                // Customer found, return JSON result
                return Json(customer, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // Customer not found, return an appropriate response
                return Json(new { error = "Customer not found" }, JsonRequestBehavior.AllowGet);
            }



        }

        public ActionResult egyptionSuspicious()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Suspicious_Egyption(Customer _customerPar)
        {

            Customer customer = db.Customers.Where(c => c.NationalId == _customerPar.NationalId).
                Where(c => c.Name == _customerPar.Name).Where(c => c.ResidentAddress == _customerPar.ResidentAddress).
                Where(c => c.CustomerType == "Egyption").FirstOrDefault();

            if (customer != null)
            {
                bool isSuspicious = customer.suspiciousUsers;
                if (isSuspicious == true)
                {
                    var responseObject = new
                    {
                        Customer = customer,                  
                        Result = "Yes"
                    };
                    return Json(responseObject, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var responseObject = new
                    {
                        Customer = customer,
                        Result = "No"
                    };
                    return Json(responseObject, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                // Customer not found, return an appropriate response
                return Json(new { error = "Customer not found" }, JsonRequestBehavior.AllowGet);
            }



        }


        [HttpPost]
        public JsonResult Suspicious_Foreigner(Customer _customerPar)
        {

            Customer customer = db.Customers.Where(c => c.PassportNumber == _customerPar.PassportNumber).
                Where(c => c.Name == _customerPar.Name).Where(c => c.ResidentAddress == _customerPar.ResidentAddress).
                Where(c => c.BirthDate == _customerPar.BirthDate).
                Where(c => c.BirthPlace == _customerPar.BirthPlace).
                Where(c => c.occupation == _customerPar.occupation).
                Where(c => c.CustomerType == "Foreigner").FirstOrDefault();

            if (customer != null)
            {
                bool isSuspicious = customer.suspiciousUsers;
                if (isSuspicious == true)
                {
                    var responseObject = new
                    {
                        Customer = customer,
                        Result = "Yes"
                    };
                    return Json(responseObject, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var responseObject = new
                    {
                        Customer = customer,
                        Result = "No"
                    };
                    return Json(responseObject, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                // Customer not found, return an appropriate response
                return Json(new { error = "Customer not found" }, JsonRequestBehavior.AllowGet);
            }



        }



    }
}