using Core;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Data.SQL_Items
{
    public class VisitDataSql : IVisitRepository
    {
        private readonly SmartHomeDbContext context;
        public VisitDataSql(SmartHomeDbContext context)
        {
            this.context = context;
        }
        public Visit Create(Visit visit)
        {
            context.Visits.Add(visit);
            return visit;
        }

        public int Commit()
        {
            return context.SaveChanges();
        }

        public Visit GetVisitById(int visitid)
        {
            return context.Visits
                .Include(x => x.User)
                .ThenInclude(x=>x.Membership)
                .Include(x =>x.ShopItems)
                .SingleOrDefault(x =>x.Id == visitid);
        }

        public IEnumerable<Visit> GetVisitByUser(string userid)
        {
            return context.Visits
                .Include(x => x.User)
                .ThenInclude(x => x.Membership)
                .Include(x => x.ShopItems)
                .Where(x=>x.UserId == userid)
                .ToList();
        }

        public IEnumerable<Visit> GetVisits(string search = null)
        {
            return context.Visits
                .Include(x => x.User)
                .ThenInclude(x => x.Membership)
                .Include(x => x.ShopItems)
                .Where(x => string.IsNullOrEmpty(search) 
                || x.User.FirstName.ToLower().StartsWith(search.ToLower()) 
                || x.User.LastName.ToLower().StartsWith(search.ToLower()))
                .OrderBy(x=>x.User.FirstName)
                .ToList();
        }

        public Visit Update(Visit visit)
        {
            context.Entry(visit).State = EntityState.Modified;
            return visit;
        }
    }
}
