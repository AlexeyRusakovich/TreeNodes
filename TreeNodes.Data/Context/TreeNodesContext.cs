using Microsoft.EntityFrameworkCore;
using TreeNodes.Data.Models;

namespace TreeNodes.Data.Context
{
    public class TreeNodesContext : DbContext
    {
        public DbSet<Journal> Journal { get; set; }
        public DbSet<TreeNode> TreeNodes { get; set; }

        public TreeNodesContext(DbContextOptions<TreeNodesContext> options) : base(options) { } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Journal>(e =>
            {
                e.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<TreeNode>(e =>
            {
                e.HasIndex(x => x.TreeName);

                e.HasOne(x => x.Parent)
                 .WithMany(x => x.Childs)
                 .HasForeignKey(x => x.ParentId)
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
