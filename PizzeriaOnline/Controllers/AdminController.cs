using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PizzeriaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static PizzeriaOnline.Controllers.ManageController;



namespace PizzeriaOnline.Controllers
{
    public class AdminController : Controller
    {
        // Dostęp do użytkowników, baz danych
        public UserManager<ApplicationUser> UserManager { get; set; }
        public ApplicationDbContext context { get; set; }

        // Listy przechowujące użytkowników i dostępne w systemie role
        public static List<AdminUserViewModel> usrList = new List<AdminUserViewModel>();
        public static List<SelectListItem> roleList = new List<SelectListItem>();
        
        // zmienne pomocnicze
        public static string AdmUsrName { get; set; }
        public static string AdmUsrEmail { get; set; }
        public static string AdmUsrRole { get; set; }
        public static string AdmUsrSrch { get; set; }
        public static string AdmRankSrch { get; set; }

        public AdminController()
        {
            context = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        }

        [Authorize(Roles = "Admin")]
        [ActionName("Index")]
        public async Task<ActionResult> ShowUserDetails(AdminUserViewModel model)
        {
            usrList.Clear();
            IList<ApplicationUser> users = context.Users.ToList();
            foreach (var user in users)
            {
                var roles = await UserManager.GetRolesAsync(user.Id);
                model.UserName = user.UserName;
                foreach (var role in roles)
                {
                    model.RankName = role;
                    switch (role)
                    {
                        case "Admin":
                            model.RankId = "1";
                            break;
                        case "Pracownik":
                            model.RankId = "2";
                            break;
                        case "Uzytkownik":
                            model.RankId = "3";
                            break;
                    }
                }
                model.UserId = user.Id;
                model.UserFullName = user.UserName;
                usrList.Add(new AdminUserViewModel()
                {
                    UserName = model.UserName,
                    RankName =
                model.RankName,
                    UserId = model.UserId,
                    RankId = model.RankId,
                    UserFullName =
                model.UserFullName
                });
                model.RankName = null;
            }
            return PartialView("ShowUserDetails");
        }

        //wyswietlanie listy

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index(AdminUserViewModel model)
        {
            await ShowUserDetails(model);
            return View();
        }


        //Edycja uzytkownika

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditUser()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(string id, AdminEditViewModel model)
        {
            try
            {
                var user = UserManager.FindById(id);
                model.Email = user.Email;
                var roles = await UserManager.GetRolesAsync(user.Id);
                model.UserName = user.UserName;
                foreach (var role in roles)
                {
                    model.RankName = role;
                }
                AdmUsrName = model.UserName;
                AdmUsrEmail = model.Email;
                AdmUsrRole = model.RankName;
                return RedirectToAction("EditUser");
            }
            catch
            {
                return View();
            }
        }


        // zwrócenie listy rol w systemie

        public IEnumerable<SelectListItem> GetUserRoles(string usrrole)
        {
            var roles = context.Roles.OrderBy(x => x.Name).ToList();
            List<AdminRoleViewModel> rList = new List<AdminRoleViewModel>();
            rList.Add(new AdminRoleViewModel() { Role = "Admin", RoleId = "1" });
            rList.Add(new AdminRoleViewModel() { Role = "Pracownik", RoleId = "2" });
            rList.Add(new AdminRoleViewModel() { Role = "Uzytkownik", RoleId = "3" });
            rList = rList.OrderBy(x => x.RoleId).ToList();
            List<SelectListItem> roleNames = new List<SelectListItem>();
            foreach (var role in rList)
            {
                roleNames.Add(new SelectListItem()
                {
                    Text = role.Role,
                    Value = role.Role
                });
            }
            var selectedRoleName = roleNames.FirstOrDefault(d => d.Value == usrrole);
            if (selectedRoleName != null) selectedRoleName.Selected = true;
            return roleNames;
        }

        //zapis zmian

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveUser(string id, AdminEditViewModel model)
        {
            try
            {
                AdmUsrRole = model.RankName;
                AdmUsrName = model.UserName;
                var userid = context.Users.Where(x => x.UserName == AdmUsrName).Select(x =>
                x.Id).FirstOrDefault();
                var user = await UserManager.FindByIdAsync(userid);
                var userRoles = await UserManager.GetRolesAsync(user.Id);
                string[] roles = new string[userRoles.Count];
                userRoles.CopyTo(roles, 0);
                await UserManager.RemoveFromRolesAsync(user.Id, roles);
                await UserManager.AddToRoleAsync(user.Id, AdmUsrRole);
                return RedirectToAction("Index", "Admin", new
                {
                    Message = ManageMessageId.UserUpdated });
            }
            catch
            {
                return RedirectToAction("Index", "Admin", new
                {
                    Message = ManageMessageId.Error });
            }
        }

        //informacja czy operacja się powiodła czy nie
        public enum ManageMessageId
        {
            HighRankedUser,
            Error,
            UserDeleted,
            UserUpdated
        }

        //usuwanie uzytkownika
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUser()
        {
            return View();
        }


        //usuwanie uzytkownika metoda post
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUser(string userid)
        {
            if (AdmUsrRole == "Admin")
            {
                return RedirectToAction("Index", "Admin", new
                {
                    Message =
                ManageMessageId.HighRankedUser
                });
            }
            userid = context.Users.Where(x => x.UserName == AdmUsrName).Select(x =>
            x.Id).FirstOrDefault();
            var user = await UserManager.FindByIdAsync(userid);
            var userClaims = await UserManager.GetClaimsAsync(user.Id);
            var userRoles = await UserManager.GetRolesAsync(user.Id);
            var userLogins = await UserManager.GetLoginsAsync(user.Id);
            foreach (var claim in userClaims)
            {
                await UserManager.RemoveClaimAsync(user.Id, claim);
            }
            string[] roles = new string[userRoles.Count];
            userRoles.CopyTo(roles, 0);
            await UserManager.RemoveFromRolesAsync(user.Id, roles);
            foreach (var log in userLogins)
            {
                await UserManager.RemoveLoginAsync(user.Id, new
                UserLoginInfo(log.LoginProvider, log.ProviderKey));
            }
            await UserManager.DeleteAsync(user);
            return RedirectToAction("Index", "Admin", new
            {
                Message =
            ManageMessageId.UserDeleted
            });
        }  
        
   
        /*    public async Task<ActionResult> Index(AdminUserViewModel model, ManageMessageId? message = null)
           {
               ViewBag.StatusMessage = message == ManageMessageId.UserDeleted ? " Konto użytkownika zostało pomyślnie usunięte." : message == ManageMessageId.UserUpdated ? "Konto użytkownika zostało zaaktualizowane." : "";
               ViewBag.ErrorMessage = message == ManageMessageId.Error ? "Błąd." : message == ManageMessageId.HighRankedUser ? "Admin nie możezostać usunięty." : "";
               await ShowUserDetails(model);
               return View();
        */
    }
}
