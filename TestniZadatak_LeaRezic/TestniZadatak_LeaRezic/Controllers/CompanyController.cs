using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestniZadatak_LeaRezic.Dal;
using TestniZadatak_LeaRezic.Infrastructure;
using TestniZadatak_LeaRezic.Models;

namespace TestniZadatak_LeaRezic.Controllers
{
    // sve je na istom kontroleru, makar se moglo razdvojiti
    public class CompanyController : Controller
    {

        private IRepository _repository = RepositoryFactory.GetDefaultInstance();

        // GET: Company
        public ActionResult ViewAll()
        {
            var model = new List<CompanyVM>();
            model = _repository.GetAllCompanies().Select(c => getViewModelFromCompany(c)).ToList();
            return View(model);
        }

        // Edit i Create su jako slični, mogli su biti isti view pa da postoji bilo neki flag o čemu je riječ,
        // bilo da se provjeri je li id==0 pa se prikazuje ili "save" ili "create" - sad je razdvojeno radi preglednosti
        // ali se definitivno ponavlja isti kod, pogotovo u pogledu
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Company entity = _repository.GetCompany(id);
            var model = getViewModelFromCompany(entity);
            ViewBag.allPlaces = getAllPlaces();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CompanyVM vm)
        {
            if (ModelState.IsValid)
            {
                Company entity = getCompanyFromViewModel(vm);
                _repository.InsertOrUpdateCompany(entity);
                return RedirectToAction("ViewAll");
            }
            else
            {
                ViewBag.allPlaces = getAllPlaces();
                return View(vm);
            }
            
        }

        [HttpGet]
        public ActionResult Create()
        {
            // pogledu proslijedi "dummy" model - id 0, a placeID prvi abecedno
            var model = new CompanyVM
            {
                IDCompany = 0,
                PlaceID = _repository.GetAllPlaces().OrderBy(p => p.Name).FirstOrDefault().IDPlace
            };
            ViewBag.allPlaces = getAllPlaces();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CompanyVM vm)
        {
            if (ModelState.IsValid)
            {
                Company entity = getCompanyFromViewModel(vm);
                _repository.InsertOrUpdateCompany(entity);
                return RedirectToAction("ViewAll");
            }
            else
            {
                ViewBag.allPlaces = getAllPlaces();
                return View(vm);
            }

        }

        // privatne metode, mogao je postojati i dodatni BLL layer / klasa koji bi se bavio
        // transformacijama iz domenskog u pogledni model i obratno uz potrebnu logiku
        private CompanyVM getViewModelFromCompany(Company c)
        {
            return new CompanyVM
            {
                IDCompany = c.IDCompany,
                CompanyName = c.Name,
                CompanyCode = c.CompanyCode,
                Address = c.Address,
                PostalCode = c.PostalCode,
                PlaceID = c.PlaceID,
                PlaceName = c.Place.Name
            };
        }

        private Company getCompanyFromViewModel(CompanyVM vm)
        {
            return new Company
            {
                IDCompany = vm.IDCompany,
                CompanyCode = vm.CompanyCode,
                Name = vm.CompanyName,
                Address = vm.Address,
                PostalCode = vm.PostalCode,
                PlaceID = vm.PlaceID
            };
        }

        private List<Place> getAllPlaces()
        {
            return _repository.GetAllPlaces().OrderBy(p => p.Name).ToList();
        }
    }
}