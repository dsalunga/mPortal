//------------------------------------------------------------------------------
// Migrated from EF6 auto-generated code to EF Core.
//------------------------------------------------------------------------------

namespace WCMS.Framework
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public partial class WFrameworkEntities : DbContext
    {
        public WFrameworkEntities()
        {
        }

        public WFrameworkEntities(DbContextOptions<WFrameworkEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<WApproval> WApprovals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WApproval>(entity =>
            {
                entity.ToTable("WApprovals");
            });
        }
    }
}
