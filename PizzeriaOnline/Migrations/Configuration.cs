using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PizzeriaOnline.Models;


namespace PizzeriaOnline.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PizzeriaOnline.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PizzeriaOnline.Models.ApplicationDbContext";
        }

        protected override void Seed(PizzeriaOnline.Models.ApplicationDbContext context)
        {
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            string[] roleNames = { "Admin", "Pracownik", "Uzytkownik" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                if (!RoleManager.RoleExists(roleName))
                {
                    roleResult = RoleManager.Create(new IdentityRole(roleName));
                }
            }

            //administrator
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            UserManager.AddToRole("d232de14-c33c-4e43-9dfa-8bd14c93b01c", "Admin");



            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
