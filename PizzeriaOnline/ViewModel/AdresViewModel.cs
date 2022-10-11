using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PizzeriaOnline.ViewModel
{
    public class AdresViewModel
    {
        public int Id_adresu { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Numer zamówienia ")]
        public string Numer_zamowienia { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Miejscowość ")]
        public string Miejscowosc { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Kod pocztowy ")]
        public string Kod_pocztowy { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Ulica ")]
        public string Ulica { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Numer domu ")]
        public string Nr_domu { get; set; }
    }
}