using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lillekauplus.Models;

namespace Lillekauplus.Controllers
{
    public class StoreController : Controller
    {
        private LilledContext db = new LilledContext();

        // GET: Store
        public ActionResult Index()
        {
            return View(db.Lilleds.ToList());
        }

        // GET: Store/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lilled lilled = db.Lilleds.Find(id);
            if (lilled == null)
            {
                return HttpNotFound();
            }
            return View(lilled);
        }

        // GET: Store/Create
        [Authorize(Roles="Admin")]
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Store/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public ActionResult Create([Bind(Include = "Id,Nimetus,Muuja,Hind")] Lilled lilled)
        {
            if (ModelState.IsValid)
            {
                db.Lilleds.Add(lilled);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lilled);
        }

        // GET: Store/Edit/5
        [Authorize(Roles = "Admin")]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lilled lilled = db.Lilleds.Find(id);
            if (lilled == null)
            {
                return HttpNotFound();
            }
            return View(lilled);
        }

        // POST: Store/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public ActionResult Edit([Bind(Include = "Id,Nimetus,Muuja,Hind")] Lilled lilled)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lilled).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lilled);
        }

        // GET: Store/Delete/5
        [Authorize(Roles = "Admin")]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lilled lilled = db.Lilleds.Find(id);
            if (lilled == null)
            {
                return HttpNotFound();
            }
            return View(lilled);
        }

        // POST: Store/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public ActionResult DeleteConfirmed(int id)
        {
            Lilled lilled = db.Lilleds.Find(id);
            db.Lilleds.Remove(lilled);
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
