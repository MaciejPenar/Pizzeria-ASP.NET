using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PizzeriaOnline.Models;

namespace PizzeriaOnline.Controllers
{
    public class ZamowieniaController : Controller
    {
        private PizzeriaOnlineBazaKopiaEntities db = new PizzeriaOnlineBazaKopiaEntities();

        // GET: Zamowienia
        public ActionResult Index()
        {
            return View(db.Zamowienia.ToList());
        }

        // GET: Zamowienia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zamowienia zamowienia = db.Zamowienia.Find(id);
            if (zamowienia == null)
            {
                return HttpNotFound();
            }
            return View(zamowienia);
        }

        // GET: Zamowienia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Zamowienia/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_zamowienia,Data_zamowienia,Numer_zamowienia")] Zamowienia zamowienia)
        {
            if (ModelState.IsValid)
            {
                db.Zamowienia.Add(zamowienia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(zamowienia);
        }

        // GET: Zamowienia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zamowienia zamowienia = db.Zamowienia.Find(id);
            if (zamowienia == null)
            {
                return HttpNotFound();
            }
            return View(zamowienia);
        }

        // POST: Zamowienia/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_zamowienia,Data_zamowienia,Numer_zamowienia")] Zamowienia zamowienia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zamowienia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zamowienia);
        }

        // GET: Zamowienia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zamowienia zamowienia = db.Zamowienia.Find(id);
            if (zamowienia == null)
            {
                return HttpNotFound();
            }
            return View(zamowienia);
        }

        // POST: Zamowienia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zamowienia zamowienia = db.Zamowienia.Find(id);
            db.Zamowienia.Remove(zamowienia);
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
