namespace TestniZadatak_LeaRezic.Dal
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EntityContext : DbContext
    {
        public EntityContext()
            : base("name=EntityContextCS")
        {
        }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Place> Place { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Place>()
                .HasMany(e => e.Company)
                .WithRequired(e => e.Place)
                .HasForeignKey(e => e.PlaceID)
                .WillCascadeOnDelete(false);
        }
    }
}
