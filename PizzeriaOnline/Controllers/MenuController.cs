using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzeriaOnline.Models;
using PizzeriaOnline.ViewModel;

namespace PizzeriaOnline.Controllers
{
    public class MenuController : Controller
    {
        private PizzeriaOnlineBazaKopiaEntities objPizzeriaBazaEntities;
        private List<KoszykModel> listOfKoszykModels;
        private PizzeriaOnlineBazaKopiaEntities db = new PizzeriaOnlineBazaKopiaEntities();
        public MenuController()
        {
            objPizzeriaBazaEntities = new PizzeriaOnlineBazaKopiaEntities();
            listOfKoszykModels = new List<KoszykModel>();
        }
        // GET: Menu


        public ActionResult NumerZamowienia()
        {
            return View(db.Zamowienia.ToList());
        }

        public ActionResult DodajAdres()
        {
            return View();
        }

        // POST: Adres/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DodajAdres([Bind(Include = "Id_adresu,Numer_zamowienia,Miejscowosc,Kod_pocztowy,Ulica,Nr_domu")] Adres adres)
        {
            if (ModelState.IsValid)
            {
                db.Adres.Add(adres);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adres);
        }


        public ActionResult Index()
        {
            IEnumerable<MenuViewModel> listOfMenuViewModels = (from objProdukt in objPizzeriaBazaEntities.Produkt
                                                               join
                                                                 objCate in objPizzeriaBazaEntities.Kategorie
                                                                 on objProdukt.Id_kategori equals objCate.Id_kategori
                                                               select new MenuViewModel()
                                                               {
                                                                   Zdjecie = objProdukt.Zdjecie,
                                                                   Nazwa = objProdukt.Nazwa,
                                                                   Opis = objProdukt.Opis,
                                                                   Cena = objProdukt.Cena,
                                                                   Id_produktu = objProdukt.Id_produktu,
                                                                   Kategoria = objCate.Nazwa,
                                                                   Kod = objCate.Kod

                                                               }
                                                                   ).ToList();
            return View(listOfMenuViewModels);
        }

        [HttpPost]
       // [Authorize(Roles = "Admin, Uzytkownik")]
        public JsonResult Index(string Id_produktu)
        {

            KoszykModel objKoszykModel = new KoszykModel();
            Produkt objProdukt = objPizzeriaBazaEntities.Produkt.Single(model => model.Id_produktu.ToString() == Id_produktu);
            if (Session["KoszykLicznik"] != null)
            {
                listOfKoszykModels = Session["KoszykPozycja"] as List<KoszykModel>;
            }
            if (listOfKoszykModels.Any(model => model.Id_produktu == Id_produktu))
            {
                objKoszykModel = listOfKoszykModels.Single(model => model.Id_produktu == Id_produktu);
                objKoszykModel.Ilosc = objKoszykModel.Ilosc + 1;
                objKoszykModel.Suma = objKoszykModel.Ilosc * objKoszykModel.Cena_jednostkowa;
            }
            else
            {
                objKoszykModel.Id_produktu = Id_produktu;
                objKoszykModel.Zdjecie = objProdukt.Zdjecie;
                objKoszykModel.Nazwa = objProdukt.Nazwa;
                objKoszykModel.Ilosc = 1;
                objKoszykModel.Suma = objProdukt.Cena;
                objKoszykModel.Cena_jednostkowa = objProdukt.Cena;
                listOfKoszykModels.Add(objKoszykModel);
            }

            Session["KoszykLicznik"] = listOfKoszykModels.Count;
            Session["KoszykPozycja"] = listOfKoszykModels;

            return Json(new { Success = true, Counter = listOfKoszykModels.Count }, JsonRequestBehavior.AllowGet);
        }

       // [Authorize(Roles = "Admin, Uzytkownik")]
        public ActionResult Koszyk()
        {
            listOfKoszykModels = Session["KoszykPozycja"] as List<KoszykModel>;
            return View(listOfKoszykModels);
        }


        [HttpPost]
       // [Authorize(Roles = "Admin, Uzytkownik")]
        public ActionResult AddZamowienia()
        {
            int Id_zamowienia = 1;
            listOfKoszykModels = Session["KoszykPozycja"] as List<KoszykModel>;
            Zamowienia zamowieniaObj = new Zamowienia()
            {
                Data_zamowienia = DateTime.Now,
                Numer_zamowienia = String.Format("{0:ddmmyyyyHHmmsss}", DateTime.Now)
            };
            objPizzeriaBazaEntities.Zamowienia.Add(zamowieniaObj);
            objPizzeriaBazaEntities.SaveChanges();
            Id_zamowienia = zamowieniaObj.Id_zamowienia;

            foreach (var item in listOfKoszykModels)
            {
                Models.ZamowieniaProdukt objZamowieniaProdukt = new Models.ZamowieniaProdukt();
                objZamowieniaProdukt.Suma = item.Suma;
                objZamowieniaProdukt.Id_produktu = item.Id_produktu;
                objZamowieniaProdukt.Id_zamowienia = Id_zamowienia;
                objZamowieniaProdukt.Ilosc = item.Ilosc;
                objZamowieniaProdukt.Cena_jednostkowa = item.Cena_jednostkowa;
                objPizzeriaBazaEntities.ZamowieniaProdukt.Add(objZamowieniaProdukt);
                objPizzeriaBazaEntities.SaveChanges();
            }
            Session["KoszykPozycja"] = null;
            Session["KoszykLicznik"] = null;
            return RedirectToAction("NumerZamowienia");
            //  return Json(data: "Złożono zamowienie", JsonRequestBehavior.AllowGet);
        }
    }
}

