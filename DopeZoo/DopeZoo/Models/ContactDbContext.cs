using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DopeZoo.Models
{
    public class ContactDbContext : IdentityDbContext<ApplicationUser>
    {
        public ContactDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; }

        public static ContactDbContext Create()
        {
            return new ContactDbContext();
        }
    }
}