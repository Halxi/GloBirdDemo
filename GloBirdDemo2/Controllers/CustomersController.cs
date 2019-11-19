using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using GloBirdDemo2.Models;

namespace GloBirdDemo2.Controllers
{
    public class CustomersController : Controller
    {
        private GloBirdModel db = new GloBirdModel();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        [HttpPost]
        public ActionResult Index(Customer obj)
        {
            //retrieve the data from table
            var customerList = db.Customers.ToList();

            string jsondata = new JavaScriptSerializer().Serialize(customerList);
            string path = Server.MapPath("~/App_Data/");
            // Write that JSON to txt file,  
            System.IO.File.WriteAllText(path + "output.json", jsondata);

            return RedirectToAction("Index"); ;
        }



        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        public JsonResult IsUserExists(string UserName)
        {
            //check if any of the UserName matches the UserName specified in the Parameter using the ANY extension method.  
            return Json(!db.Customers.Any(x => x.Username == UserName), JsonRequestBehavior.AllowGet);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,Username,FirstName,LastName,PhoneNo,DOB")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.DOB = customer.DOB.Date;
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.DOB = customer.DOB;
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,Username,FirstName,LastName,PhoneNo,DOB")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            foreach (CallNote callNote in db.CallNotes)
            {
                if (callNote.CustomerId == id)
                {
                    db.CallNotes.Remove(callNote);
                }
            }
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Search()
        {
            return View(db.Customers.ToList());
        }

        [HttpPost]
        public ActionResult Search(String firstName, String lastName)
        {
            var customers = from c in db.Customers select c;
            if (!String.IsNullOrEmpty(firstName) || !String.IsNullOrEmpty(lastName))
            {
                customers = customers.Where(u => (u.FirstName.Contains(firstName) && u.LastName.Contains(lastName)));
            }
            return View(customers.ToList());
        }

    }
}
