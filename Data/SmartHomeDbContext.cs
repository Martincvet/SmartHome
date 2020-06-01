using Core;
using Core.AppUser;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class SmartHomeDbContext : IdentityDbContext<App_User,App_Role, string>
    {
        public SmartHomeDbContext(DbContextOptions<SmartHomeDbContext> dbContext) : base(dbContext)
        {

        }

        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ShopItem> ShopItems { get; set; }
        public DbSet<Visit> Visits { get; set; }
    }
}
