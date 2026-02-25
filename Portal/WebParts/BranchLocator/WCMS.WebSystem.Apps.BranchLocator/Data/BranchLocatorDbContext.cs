namespace WCMS.WebSystem.Apps.BranchLocator.Data
{
    using Microsoft.EntityFrameworkCore;

    public class BranchLocatorDbContext : DbContext
    {
        public BranchLocatorDbContext()
        {
        }

        public BranchLocatorDbContext(DbContextOptions<BranchLocatorDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MChapter> MChapters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MChapter>(entity =>
            {
                entity.ToTable("MChapters");
                entity.HasKey(e => e.Id);

                entity.Ignore(e => e.Children);
                entity.Ignore(e => e.Parent);
                entity.Ignore(e => e.HasChildren);
                entity.Ignore(e => e.HasExtra);
                entity.Ignore(e => e.OBJECT_ID);
            });
        }
    }
}
