// Migrated from EF6 EDMX (ExternalDBModel.edmx) to EF Core code-first.
// Replaces legacy ExternalDBEntities (ObjectContext) and ExternalDBModel.Designer.cs entities.

namespace WCMS.WebSystem.Apps.Integration.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore;

    public class ExternalDbContext : DbContext
    {
        public ExternalDbContext()
        {
        }

        public ExternalDbContext(DbContextOptions<ExternalDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AMSCountry> AMSCountries { get; set; }
        public virtual DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AMSCountry>(entity =>
            {
                entity.ToTable("Countries");
                entity.HasKey(e => e.CountryID);
                entity.HasMany(e => e.States)
                      .WithOne(e => e.Country)
                      .HasForeignKey(e => e.CountryID);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("States");
                entity.HasKey(e => new { e.CountryID, e.CapitalID, e.StateName });
            });
        }
    }

    [Table("Countries")]
    public class AMSCountry
    {
        [Key]
        public short CountryID { get; set; }
        public short? RegionID { get; set; }
        public int? CapitalID { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public int UpdatedBy { get; set; }
        public int? DivisionID { get; set; }

        public virtual ICollection<State> States { get; set; } = new HashSet<State>();
    }

    [Table("States")]
    public class State
    {
        public short CountryID { get; set; }
        public int CapitalID { get; set; }
        public string StateName { get; set; }
        public int? StateID { get; set; }
        public string AreaCode { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey("CountryID")]
        public virtual AMSCountry Country { get; set; }
    }
}
