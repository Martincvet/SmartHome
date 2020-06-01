using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHomeMVC.Models.Identity;

namespace SmartHomeMVC.Models
{
    public class SmartHomemvcDbContext : IdentityDbContext<IdentityUser>
    {
        public SmartHomemvcDbContext(DbContextOptions<SmartHomemvcDbContext> dbContextOptions): base(dbContextOptions)
        {

        }
    }
}
