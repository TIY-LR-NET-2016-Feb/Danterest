using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Danterest2.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class DanUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<DanUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public virtual ICollection<Dan> Dans { get; set; } = new List<Dan>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();


    }



    public class Dan
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public string LinkUrl { get; set; }
        public string Description { get; set; }

        public virtual DanUser Owner { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public DateTime CreatedOn { get; set; }
    }

    public class Comment
    {
        public int Id { get; set; }
        public virtual DanUser CreatedBy { get; set; }
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }
        public Dan ParentDan { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<DanUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Dan> Dans { get; set; }
    }
}