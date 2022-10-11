using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzeriaOnline.ViewModel
{
    public class KoszykModel
    {
        public string Id_produktu { get; set; }
        public decimal Ilosc { get; set; }
        public decimal Cena_jednostkowa { get; set; }
        public decimal Suma { get; set; }
        public string Zdjecie { get; set; }
        public string Nazwa { get; set; }
    }
}