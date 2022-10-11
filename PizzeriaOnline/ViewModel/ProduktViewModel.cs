using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzeriaOnline.ViewModel
{
    public class ProduktViewModel
    {
        public Guid Id_produktu { get; set; }
        public int Id_kategori { get; set; }
        public string Kod { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public decimal Cena { get; set; }
        public HttpPostedFileBase Zdjecie { get; set; }

        public IEnumerable<SelectListItem> KategorieSelectListItems { get; set; }
    }
}