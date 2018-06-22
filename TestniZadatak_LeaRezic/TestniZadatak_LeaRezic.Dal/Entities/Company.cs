namespace TestniZadatak_LeaRezic.Dal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Company")]
    public partial class Company
    {
        [Key]
        public int IDCompany { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(25)]
        public string CompanyCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(25)]
        public string PostalCode { get; set; }

        public int PlaceID { get; set; }

        public virtual Place Place { get; set; }
    }
}
