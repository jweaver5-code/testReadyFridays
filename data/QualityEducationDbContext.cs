using Microsoft.EntityFrameworkCore;
using QualityEducation.Models;

namespace QualityEducation.Data
{
    public class QualityEducationDbContext : DbContext
    {
        public QualityEducationDbContext(DbContextOptions<QualityEducationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Role).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Grade).HasMaxLength(50);
                entity.Property(e => e.CompletedModules).HasColumnType("TEXT");
                entity.Property(e => e.CompletedQuizzes).HasColumnType("TEXT");
                entity.Property(e => e.RecentActivity).HasColumnType("TEXT");
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.LastLogin).IsRequired();
                entity.Property(e => e.IsActive).IsRequired();
            });

            // Configure Classroom entity
            modelBuilder.Entity<Classroom>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Grade).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Subject).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(20);
                entity.Property(e => e.StudentIds).HasColumnType("TEXT");
                entity.Property(e => e.AssignedContent).HasColumnType("TEXT");
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.IsActive).IsRequired();
            });

            // Seed data is now handled in Program.cs to avoid overriding existing data
        }
    }
}

