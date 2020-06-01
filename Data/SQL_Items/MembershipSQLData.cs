using Core;
using Data.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Data.SQL_Items
{
    public class MembershipSQLData : IMembershipData
    {
        private readonly SmartHomeDbContext context;
        public MembershipSQLData(SmartHomeDbContext context)
        {
            this.context = context;
        }
        public int Commit()
        {
            return context.SaveChanges();
        }

        public int Count()
        {
            return context.Memberships.Count();
        }

        public Membership CreateMembership(Membership membership)
        {
            Membership nesho = context.Memberships.SingleOrDefault(s => s.Id == membership.Id);
            context.Memberships.Add(nesho);
            context.SaveChanges();
            return nesho;
        }

        public Membership DeleteMembership(int membershipid)
        {
            var temp = context.Memberships.FirstOrDefault(s=>s.Id == membershipid);
            if(temp != null)
            {
                context.Memberships.Remove(temp);
            }
            context.SaveChanges();

            return temp;
        }

        public Membership GetMembershipById(int membershipid)
        {
            return context.Memberships.SingleOrDefault(s=> s.Id == membershipid);
        }

        public IEnumerable<Membership> GetMemberships(string name)
        {
            return context.Memberships.Where(s => string.IsNullOrEmpty(name) || s.MemberhipTypeName.ToLower().StartsWith(name.ToLower())).OrderBy(s=>s.MemberhipTypeName).ToList();
        }

        public Membership Update(Membership membership)
        {
            var nesho = new Membership();
            if(nesho != null)
            {
                nesho.Id = membership.Id;
                nesho.Discount_Product = membership.Discount_Product;
                nesho.Discount_Service = membership.Discount_Service;
                nesho.MemberhipTypeName = membership.MemberhipTypeName;
            }
            context.SaveChanges();

            return nesho;
        }
    }
}
