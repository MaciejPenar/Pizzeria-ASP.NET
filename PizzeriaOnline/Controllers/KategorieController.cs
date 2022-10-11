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
    public class KategorieController : Controller
    {
        private PizzeriaOnlineBazaKopiaEntities db = new PizzeriaOnlineBazaKopiaEntities();

        // GET: KategorieViewModel
        public ActionResult Index()
        {
            return View(db.Kategorie.ToList());
        }

        // GET: KategorieViewModel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorie kategorie = db.Kategorie.Find(id);
            if (kategorie == null)
            {
                return HttpNotFound();
            }
            return View(kategorie);
        }

        // GET: KategorieViewModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KategorieViewModel/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_kategori,Kod,Nazwa")] Kategorie kategorie)
        {
            if (ModelState.IsValid)
            {
                db.Kategorie.Add(kategorie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kategorie);
        }

        // GET: KategorieViewModel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorie kategorie = db.Kategorie.Find(id);
            if (kategorie == null)
            {
                return HttpNotFound();
            }
            return View(kategorie);
        }

        // POST: KategorieViewModel/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_kategori,Kod,Nazwa")] Kategorie kategorie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kategorie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategorie);
        }

        // GET: KategorieViewModel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorie kategorie = db.Kategorie.Find(id);
            if (kategorie == null)
            {
                return HttpNotFound();
            }
            return View(kategorie);
        }

        // POST: KategorieViewModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategorie kategorie = db.Kategorie.Find(id);
            db.Kategorie.Remove(kategorie);
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
