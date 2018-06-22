using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestniZadatak_LeaRezic.Dal
{
    // jednostavnosti radi je jedan repositorij za oba entiteta i to samo metode koje su potrebne za zadatak,
    // za ljepši separation of cencerns, implementira interface da kontroleri ne ovise o DBRepository-ju
    // makar bi totalni "Algebra PS" bio da se i domain razdvoji kao Class Library, pa svi implementatori
    // interface-a, kao razdvojeni class library-ji, rade s tim domenskim modelima

    public class DBRepository : IRepository
    {
        // TVRTKA
        public IEnumerable<Company> GetAllCompanies()
        {
            try
            {
                using (var db = new EntityContext())
                {
                    // za ovu količinu podataka je include vjerojatno prihvatljiv način zaobilaženja lazy-loadinga
                    // makar nije baš idealno rješenje
                    return db.Company.Include("Place").ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Company GetCompany(int id)
        {
            try
            {
                using (var db = new EntityContext())
                {
                    return db.Company.Include("Place").SingleOrDefault(c => c.IDCompany == id);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // jedna metoda za insert ili update, možda bi bolje bilo razdvojiti da se repozitorija ne tiče
        // logika kontrolora kada radi dummy objekt za dodavanje novog (mora znati da će staviti 0 kao id)
        public void InsertOrUpdateCompany(Company item)
        {
            try
            {
                using (var db = new EntityContext())
                {
                    db.Entry(item).State = item.IDCompany == 0 ?
                                            EntityState.Added :
                                            EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        // GRADOVI / MJESTA
        public IEnumerable<Place> GetAllPlaces()
        {
            try
            {
                using (var db = new EntityContext())
                {
                    return db.Place.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}
