using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PizzeriaOnline.Models;
using PizzeriaOnline.ViewModel;

namespace PizzeriaOnline.Controllers
{
    public class ProduktyController : Controller
    {
        private PizzeriaOnlineBazaKopiaEntities objPizzeriaBazaEntities;
        public ProduktyController()
        {
            objPizzeriaBazaEntities = new PizzeriaOnlineBazaKopiaEntities();
        }

        private PizzeriaOnlineBazaKopiaEntities db = new PizzeriaOnlineBazaKopiaEntities();

        // GET: Produkty
        public ActionResult Index()
        {
            return View(db.Produkt.ToList());
        }

        // GET: Produkty/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produkt produkt = db.Produkt.Find(id);
            if (produkt == null)
            {
                return HttpNotFound();
            }
            return View(produkt);
        }

        /*       // GET: Produkty/Create
               public ActionResult Create()
               {
                   return View();
               }

               // POST: Produkty/Create
               // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
               // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
               [HttpPost]
               [ValidateAntiForgeryToken]
               public ActionResult Create([Bind(Include = "Id_produktu,Id_kategori,Kod,Nazwa,Opis,Cena,Zdjecie")] Produkt produkt)
               {
                   if (ModelState.IsValid)
                   {
                       produkt.Id_produktu = Guid.NewGuid();
                       db.Produkts.Add(produkt);
                       db.SaveChanges();
                       return RedirectToAction("Index");
                   }

                   return View(produkt);
               }
       */
        public ActionResult Create()
        {
            ProduktViewModel objProduktViewModel = new ProduktViewModel();
            objProduktViewModel.KategorieSelectListItems = (from objCat in objPizzeriaBazaEntities.Kategorie
                                                            select new SelectListItem()
                                                            {
                                                                Text = objCat.Nazwa,
                                                                Value = objCat.Id_kategori.ToString(),
                                                                Selected = true
                                                            });
            return View(objProduktViewModel);
        }

        [HttpPost]
        public JsonResult Create(ProduktViewModel objProduktViewModel)
        {

            string NewImage = Guid.NewGuid() + Path.GetExtension(objProduktViewModel.Zdjecie.FileName);
            objProduktViewModel.Zdjecie.SaveAs(Server.MapPath("~/Images/" + NewImage));

            Produkt objProdukt = new Produkt();
            objProdukt.Zdjecie = "~/Images/" + NewImage;
            objProdukt.Id_kategori = objProduktViewModel.Id_kategori;
            objProdukt.Opis = objProduktViewModel.Opis;
            objProdukt.Kod = objProduktViewModel.Kod;
            objProdukt.Id_produktu = Guid.NewGuid();
            objProdukt.Nazwa = objProduktViewModel.Nazwa;
            objProdukt.Cena = objProduktViewModel.Cena;

            objPizzeriaBazaEntities.Produkt.Add(objProdukt);
            objPizzeriaBazaEntities.SaveChanges();

            return Json(new { Success = true, Message = "Produkt został dodany" }, JsonRequestBehavior.AllowGet);

            //   return Json("HHHH", JsonRequestBehavior.AllowGet);
        }



        // GET: Produkty/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produkt produkt = db.Produkt.Find(id);
            if (produkt == null)
            {
                return HttpNotFound();
            }
            return View(produkt);
        }

        // POST: Produkty/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_produktu,Id_kategori,Kod,Nazwa,Opis,Cena,Zdjecie")] Produkt produkt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produkt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produkt);
        }

        // GET: Produkty/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produkt produkt = db.Produkt.Find(id);
            if (produkt == null)
            {
                return HttpNotFound();
            }
            return View(produkt);
        }

        // POST: Produkty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Produkt produkt = db.Produkt.Find(id);
            db.Produkt.Remove(produkt);
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
