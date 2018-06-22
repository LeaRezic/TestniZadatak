using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestniZadatak_LeaRezic.Dal
{
    public interface IRepository
    {
        IEnumerable<Company> GetAllCompanies();
        Company GetCompany(int id);
        void InsertOrUpdateCompany(Company c);
        IEnumerable<Place> GetAllPlaces();
    }
}
