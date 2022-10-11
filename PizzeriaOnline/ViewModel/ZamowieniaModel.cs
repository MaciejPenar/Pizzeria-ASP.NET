using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzeriaOnline.ViewModel
{
    public class ZamowieniaModel
    {
        public int Id_zamowienia { get; set; }
        public DateTime Data_zamowienia { get; set; }
        public string Numer_zamowienia { get; set; }
    }
}