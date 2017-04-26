using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DopeZoo.Models
{
    public class ShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public ShopDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Sale> Sales { get; set; }

        public static ShopDbContext Create()
        {
            return new ShopDbContext();
        }
    }
}