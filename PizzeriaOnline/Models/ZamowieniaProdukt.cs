//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PizzeriaOnline.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ZamowieniaProdukt
    {
        public int Id_zamowieniaProdukt { get; set; }
        public int Id_zamowienia { get; set; }
        public string Id_produktu { get; set; }
        public decimal Ilosc { get; set; }
        public decimal Cena_jednostkowa { get; set; }
        public decimal Suma { get; set; }
    }
}
