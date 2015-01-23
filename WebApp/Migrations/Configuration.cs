using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApp.Models;

namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApp.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            UserManager<User> um = new UserManager<User>(new UserStore<User>(context));
            RoleManager<IdentityRole> rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


            if (!rm.RoleExists("Moderator"))
            {
                rm.Create(new IdentityRole("Moderator"));
            }

            
            User userModerator = new User
            {
                UserName = "moderator",
                PasswordHash = new PasswordHasher().HashPassword("blabla"),
                
            };

            context.Users.AddOrUpdate(u => u.UserName, userModerator);
            um.AddToRole(userModerator.Id, "Moderator");
        }
    }
}
