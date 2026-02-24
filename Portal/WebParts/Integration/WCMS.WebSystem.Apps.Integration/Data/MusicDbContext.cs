// Migrated from EF6 EDMX (MusicModel.edmx) to EF Core code-first.

namespace WCMS.WebSystem.Apps.Integration.Data
{
    using Microsoft.EntityFrameworkCore;

    public class MusicDbContext : DbContext
    {
        public MusicDbContext()
        {
        }

        public MusicDbContext(DbContextOptions<MusicDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Music> Musics { get; set; }
        public virtual DbSet<MusicEntry> MusicEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Music>(entity =>
            {
                entity.ToTable("Musics");
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.MusicEntries)
                      .WithOne(e => e.Music)
                      .HasForeignKey(e => e.MusicId);
            });

            modelBuilder.Entity<MusicEntry>(entity =>
            {
                entity.ToTable("MusicEntries");
                entity.HasKey(e => e.Id);
            });
        }
    }
}
