using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interface
{
    public interface IMembershipData
    {
        IEnumerable<Membership> GetMemberships(string name = null);
        Membership GetMembershipById(int membershipid);
        Membership CreateMembership(Membership membership);
        Membership Update(Membership membership);
        Membership DeleteMembership(int membershipid);
        int Commit();
        int Count();
    }
}
