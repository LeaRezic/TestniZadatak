using System.ComponentModel.DataAnnotations;

namespace TestniZadatak_LeaRezic.Models
{
    // jednostavnosti radi, postoji samo jedan view model (možda nije baš spretno ime), mogla su postojati
    // 3 modela za pojedini view, pa da svi skupa nasljeđuju neku base klasu s zajedničkim propertijima
    // kod prikaza je sasvim beskorisan placeID, dok bi za create i edit mogli dodati listu mjesta na sami
    // model, a ne koristiti ViewBag
    public class CompanyVM
    {
        public int IDCompany { get; set; }

        [Display(Name = "Company Code")]
        [UniqueCodeValidator]
        [Required(ErrorMessage = "Company code is a mandatory field.")]
        public string CompanyCode { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Company name is a mandatory field.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Address is a mandatory field.")]
        public string Address { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "Postal code is a mandatory field.")]
        public string PostalCode { get; set; }

        public int PlaceID { get; set; }

        [Display(Name = "City / Place")]
        public string PlaceName { get; set; }
    }
}