using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interface
{
    public interface IVisitRepository
    {
        IEnumerable<Visit> GetVisits(string search = null);
        IEnumerable<Visit> GetVisitByUser(string userid);
        Visit GetVisitById(int visitid);
        Visit Create(Visit visit);
        Visit Update(Visit visit);
        int Commit();
    }
}
