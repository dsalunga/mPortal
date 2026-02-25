// EF Core DbContext for Integration module entities.

namespace WCMS.WebSystem.Apps.Integration.Data
{
    using Microsoft.EntityFrameworkCore;

    public class IntegrationDbContext : DbContext
    {
        public IntegrationDbContext()
        {
        }

        public IntegrationDbContext(DbContextOptions<IntegrationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MemberLink> MemberLinks { get; set; }
        public virtual DbSet<MemberVisit> MemberVisits { get; set; }
        public virtual DbSet<GenericRegistration> Registrations { get; set; }
        public virtual DbSet<MCCandidate> MCCandidates { get; set; }
        public virtual DbSet<MCInterpreterScore> MCInterpreterScores { get; set; }
        public virtual DbSet<MCSongScore> MCSongScores { get; set; }
        public virtual DbSet<MCVote> MCVotes { get; set; }
        public virtual DbSet<MCComposer> MCComposers { get; set; }
        public virtual DbSet<Sportsfest> Sportsfests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberLink>(entity =>
            {
                entity.ToTable("MemberLinks");
                entity.HasKey(e => e.MemberLinkId);
                entity.Ignore(e => e.Id);
                entity.Ignore(e => e.IsApproved);
                entity.Ignore(e => e.IsPrivate);
                entity.Property(e => e.LocaleCountryCode).HasColumnName("HomeAddressCountryCode");
            });

            modelBuilder.Entity<MemberVisit>(entity =>
            {
                entity.ToTable("ODKVisits");
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<GenericRegistration>(entity =>
            {
                entity.ToTable("Registrations");
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<MCCandidate>(entity =>
            {
                entity.ToTable("MCCandidates");
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<MCInterpreterScore>(entity =>
            {
                entity.ToTable("MCInterpreterScores");
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<MCSongScore>(entity =>
            {
                entity.ToTable("MCSongScores");
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<MCVote>(entity =>
            {
                entity.ToTable("MCVotes");
                entity.HasKey(e => e.Id);
                entity.Ignore(e => e.IsSpam);
            });

            modelBuilder.Entity<MCComposer>(entity =>
            {
                entity.ToTable("MCComposers");
                entity.HasKey(e => e.Id);
                entity.Ignore(e => e.IsActive);
            });

            modelBuilder.Entity<Sportsfest>(entity =>
            {
                entity.ToTable("Sportsfests");
                entity.HasKey(e => e.Id);
            });
        }
    }
}
