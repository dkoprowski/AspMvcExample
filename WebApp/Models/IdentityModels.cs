using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApp.Models
{
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public virtual List<CommentModel> Comments { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(): base("DefaultConnection"){}

        public System.Data.Entity.DbSet<WebApp.Models.ProductModel> ProductModels { get; set; }

        public System.Data.Entity.DbSet<WebApp.Models.CommentModel> CommentModels { get; set; }

        //public System.Data.Entity.DbSet<WebApp.Models.User> Users { get; set; }
    }
}