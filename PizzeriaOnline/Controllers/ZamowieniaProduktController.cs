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
    public class ZamowieniaProduktController : Controller
    {
        private PizzeriaOnlineBazaKopiaEntities db = new PizzeriaOnlineBazaKopiaEntities();

        // GET: ZamowieniaProdukt
        public ActionResult Index()
        {
            return View(db.ZamowieniaProdukt.ToList());
        }

        // GET: ZamowieniaProdukt/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZamowieniaProdukt zamowieniaProdukt = db.ZamowieniaProdukt.Find(id);
            if (zamowieniaProdukt == null)
            {
                return HttpNotFound();
            }
            return View(zamowieniaProdukt);
        }

        // GET: ZamowieniaProdukt/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ZamowieniaProdukt/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_zamowieniaProdukt,Id_zamowienia,Id_produktu,Ilosc,Cena_jednostkowa,Suma")] ZamowieniaProdukt zamowieniaProdukt)
        {
            if (ModelState.IsValid)
            {
                db.ZamowieniaProdukt.Add(zamowieniaProdukt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(zamowieniaProdukt);
        }

        // GET: ZamowieniaProdukt/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZamowieniaProdukt zamowieniaProdukt = db.ZamowieniaProdukt.Find(id);
            if (zamowieniaProdukt == null)
            {
                return HttpNotFound();
            }
            return View(zamowieniaProdukt);
        }

        // POST: ZamowieniaProdukt/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_zamowieniaProdukt,Id_zamowienia,Id_produktu,Ilosc,Cena_jednostkowa,Suma")] ZamowieniaProdukt zamowieniaProdukt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zamowieniaProdukt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zamowieniaProdukt);
        }

        // GET: ZamowieniaProdukt/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZamowieniaProdukt zamowieniaProdukt = db.ZamowieniaProdukt.Find(id);
            if (zamowieniaProdukt == null)
            {
                return HttpNotFound();
            }
            return View(zamowieniaProdukt);
        }

        // POST: ZamowieniaProdukt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZamowieniaProdukt zamowieniaProdukt = db.ZamowieniaProdukt.Find(id);
            db.ZamowieniaProdukt.Remove(zamowieniaProdukt);
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
