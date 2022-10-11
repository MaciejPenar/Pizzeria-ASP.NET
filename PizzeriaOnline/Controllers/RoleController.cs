using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzeriaOnline.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            ViewBag.StatusMessage = "To jest strona dostępna dla wszystkich.";
            return View();
        }

        [Authorize(Roles = "Uzytkownik")]
        public ActionResult OnlyUser()
        {
            ViewBag.StatusMessage = "To jest strona dostępna tylko dla zalogowanych uzytkowników";
        return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult OnlyAdmin()
        {
            ViewBag.StatusMessage = "To jest strona dostępna tylko dla administratora.";
                return View();
        }


    }
}