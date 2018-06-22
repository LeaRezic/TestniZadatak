using System.ComponentModel.DataAnnotations;
using System.Linq;
using TestniZadatak_LeaRezic.Dal;
using TestniZadatak_LeaRezic.Infrastructure;

namespace TestniZadatak_LeaRezic.Models
{
    public class UniqueCodeValidator : ValidationAttribute
    {
        // validator za unique company code constraint, možda je nezgodno što on sad ima instancu repozitorija
        // te je sve skupa moglo biti ljepše uz neku BLL klasu, npr dataManager
        // također, ova klasa je mogla ići u neki drugi folder, npr Extensions (na faksu trenutno još ne obrađujemo
        // baš teme arhitekture i urednog smještanja klasa, pa se obično sve stavlja u Models, čak i dal, repo, itd)

        private IRepository _repository = RepositoryFactory.GetDefaultInstance();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var companyVM = validationContext.ObjectInstance as CompanyVM;
            var entity = _repository.GetAllCompanies().FirstOrDefault(c => c.CompanyCode == companyVM.CompanyCode);

            // ako ne postoji takav Code, ili ako postoji ali je to on sam - vraća true, u suprotnom validation error
            if (entity == null)
            {
                return ValidationResult.Success;
            }
            else if (entity.IDCompany == companyVM.IDCompany)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Company code must be a unique value.");
            }
        }
    }
}