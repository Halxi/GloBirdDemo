using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GloBirdDemo2.Models;

namespace GloBirdDemo2.Controllers
{
    public class CallNotesController : Controller
    {
        private GloBirdModel db = new GloBirdModel();

        // GET: CallNotes
        public ActionResult Index()
        {
            var callNotes = db.CallNotes.Include(c => c.ChildCallNote).Include(c => c.Customer);
            return View(callNotes.ToList());
        }

        // GET: CallNotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CallNote callNote = db.CallNotes.Find(id);
            if (callNote == null)
            {
                return HttpNotFound();
            }
            return View(callNote);
        }

        // GET: CallNotes/Create
        public ActionResult Create()
        {
            List<SelectListItem> list = new SelectList(db.CallNotes, "CallNoteId", "CallNoteId").ToList();
            list.Insert(0, (new SelectListItem { Text = " ", Value = null }));
            ViewBag.ParentCallNoteId = new SelectList(list, "Value", "Text", null);
            // ViewBag.ParentCallNoteId = new SelectList(db.CallNotes, "CallNoteId", "CallNoteId");
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Username");
            return View();
        }

        // POST: CallNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CallNoteId,Text,DateTime,CustomerId,ParentCallNoteId")] CallNote callNote)
        {
            if (ModelState.IsValid)
            {
                callNote.DateTime = DateTime.Now;
                db.CallNotes.Add(callNote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<SelectListItem> list = new SelectList(db.CallNotes, "CallNoteId", "CallNoteId", callNote.ParentCallNoteId).ToList();
            list.Insert(0, (new SelectListItem { Text = " ", Value = null }));
            ViewBag.ParentCallNoteId = new SelectList(list, "Value", "Text", null);

            //ViewBag.ParentCallNoteId = new SelectList(db.CallNotes, "CallNoteId", "Text", callNote.ParentCallNoteId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Username", callNote.CustomerId);
            return View(callNote);
        }

        // GET: CallNotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CallNote callNote = db.CallNotes.Find(id);
            if (callNote == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> list = new SelectList(db.CallNotes, "CallNoteId", "CallNoteId", callNote.ParentCallNoteId).ToList();
            list.Insert(0, (new SelectListItem { Text = " ", Value = null }));
            ViewBag.ParentCallNoteId = new SelectList(list, "Value", "Text", callNote.ParentCallNoteId);


            //ViewBag.ParentCallNoteId = new SelectList(db.CallNotes, "CallNoteId", "CallNoteId", callNote.ParentCallNoteId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Username", callNote.CustomerId);
            return View(callNote);
        }

        // POST: CallNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CallNoteId,Text,DateTime,CustomerId,ParentCallNoteId")] CallNote callNote)
        {
            if (ModelState.IsValid)
            {
                callNote.DateTime = DateTime.Now;
                db.Entry(callNote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentCallNoteId = new SelectList(db.CallNotes, "CallNoteId", "CallNoteId", callNote.ParentCallNoteId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Username", callNote.CustomerId);
            return View(callNote);
        }

        // GET: CallNotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CallNote callNote = db.CallNotes.Find(id);
            if (callNote == null)
            {
                return HttpNotFound();
            }
            return View(callNote);
        }

        // POST: CallNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CallNote callNote = db.CallNotes.Find(id);
            db.CallNotes.Remove(callNote);
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
    }
}
