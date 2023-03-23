using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Management_Debt.Context;

namespace Management_Debt.Controllers
{
    public class DebtorsController : Controller
    {
        private ManagementDebtEntities db = new ManagementDebtEntities();

        // GET: Debtors
        public ActionResult Index()
        {
            return View(db.Debtors.ToList());
        }

        // GET: Debtors/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Debtor debtor = db.Debtors.Find(id);
            if (debtor == null)
            {
                return HttpNotFound();
            }
            return View(debtor);
        }

        // GET: Debtors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Debtors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "did,nid,address,birthday,email")] Debtor debtor)
        {
            if (ModelState.IsValid)
            {
                db.Debtors.Add(debtor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(debtor);
        }

        // GET: Debtors/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Debtor debtor = db.Debtors.Find(id);
            if (debtor == null)
            {
                return HttpNotFound();
            }
            return View(debtor);
        }

        // POST: Debtors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "did,nid,address,birthday,email")] Debtor debtor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(debtor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(debtor);
        }

        // GET: Debtors/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Debtor debtor = db.Debtors.Find(id);
            if (debtor == null)
            {
                return HttpNotFound();
            }
            return View(debtor);
        }

        // POST: Debtors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Debtor debtor = db.Debtors.Find(id);
            db.Debtors.Remove(debtor);
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
