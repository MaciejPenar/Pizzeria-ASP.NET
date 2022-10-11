using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzeriaOnline.ViewModel
{
    public class MenuViewModel
    {
        public Guid Id_produktu { get; set; }
        public string Nazwa { get; set; }
        public decimal Cena { get; set; }
        public string Zdjecie { get; set; }
        public string Opis { get; set; }
        public string Kod { get; set; }
        public string Kategoria { get; set; }
    }
}