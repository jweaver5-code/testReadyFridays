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

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Admin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@qualityeducation.com",
                    Password = "admin123",
                    Role = "admin",
                    Grade = "N/A",
                    CompletedModules = "[]",
                    CompletedQuizzes = "[]",
                    GamesPlayed = 0,
                    RecentActivity = "[]",
                    CreatedAt = DateTime.UtcNow,
                    LastLogin = DateTime.UtcNow,
                    IsActive = true
                }
            );

            // Harry Potter Teachers
            var teachers = new[]
            {
                new { FirstName = "Albus", LastName = "Dumbledore", Email = "albus.dumbledore@hogwarts.edu", Password = "phoenix123", Grade = "Multiple Grades" },
                new { FirstName = "Minerva", LastName = "McGonagall", Email = "minerva.mcgonagall@hogwarts.edu", Password = "transfiguration123", Grade = "Multiple Grades" },
                new { FirstName = "Severus", LastName = "Snape", Email = "severus.snape@hogwarts.edu", Password = "potions123", Grade = "Multiple Grades" },
                new { FirstName = "Remus", LastName = "Lupin", Email = "remus.lupin@hogwarts.edu", Password = "defense123", Grade = "Multiple Grades" },
                new { FirstName = "Filius", LastName = "Flitwick", Email = "filius.flitwick@hogwarts.edu", Password = "charms123", Grade = "Multiple Grades" },
                new { FirstName = "Pomona", LastName = "Sprout", Email = "pomona.sprout@hogwarts.edu", Password = "herbology123", Grade = "Multiple Grades" },
                new { FirstName = "Sybill", LastName = "Trelawney", Email = "sybill.trelawney@hogwarts.edu", Password = "divination123", Grade = "Multiple Grades" },
                new { FirstName = "Rubeus", LastName = "Hagrid", Email = "rubeus.hagrid@hogwarts.edu", Password = "creatures123", Grade = "Multiple Grades" }
            };

            for (int i = 0; i < teachers.Length; i++)
            {
                modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = i + 2,
                        FirstName = teachers[i].FirstName,
                        LastName = teachers[i].LastName,
                        Email = teachers[i].Email,
                        Password = teachers[i].Password,
                        Role = "teacher",
                        Grade = teachers[i].Grade,
                        CompletedModules = "[]",
                        CompletedQuizzes = "[]",
                        GamesPlayed = 0,
                        RecentActivity = "[]",
                        CreatedAt = DateTime.UtcNow,
                        LastLogin = DateTime.UtcNow,
                        IsActive = true
                    }
                );
            }

            // Harry Potter Students
            var students = new[]
            {
                new { FirstName = "Harry", LastName = "Potter", Email = "harry.potter@hogwarts.edu", Password = "quidditch123", Grade = "Grade 7", Stars = 100 },
                new { FirstName = "Hermione", LastName = "Granger", Email = "hermione.granger@hogwarts.edu", Password = "books123", Grade = "Grade 7", Stars = 0 },
                new { FirstName = "Ron", LastName = "Weasley", Email = "ron.weasley@hogwarts.edu", Password = "chess123", Grade = "Grade 7", Stars = 0 },
                new { FirstName = "Neville", LastName = "Longbottom", Email = "neville.longbottom@hogwarts.edu", Password = "herbology123", Grade = "Grade 7", Stars = 0 },
                new { FirstName = "Luna", LastName = "Lovegood", Email = "luna.lovegood@hogwarts.edu", Password = "nargles123", Grade = "Grade 6", Stars = 0 },
                new { FirstName = "Ginny", LastName = "Weasley", Email = "ginny.weasley@hogwarts.edu", Password = "batbogey123", Grade = "Grade 6", Stars = 0 },
                new { FirstName = "Draco", LastName = "Malfoy", Email = "draco.malfoy@hogwarts.edu", Password = "slytherin123", Grade = "Grade 7", Stars = 0 },
                new { FirstName = "Cedric", LastName = "Diggory", Email = "cedric.diggory@hogwarts.edu", Password = "hufflepuff123", Grade = "Grade 7", Stars = 0 }
            };

            for (int i = 0; i < students.Length; i++)
            {
                modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = i + 10,
                        FirstName = students[i].FirstName,
                        LastName = students[i].LastName,
                        Email = students[i].Email,
                        Password = students[i].Password,
                        Role = "student",
                        Grade = students[i].Grade,
                        Stars = students[i].Stars,
                        CompletedModules = "[]",
                        CompletedQuizzes = "[]",
                        GamesPlayed = 0,
                        RecentActivity = "[]",
                        CreatedAt = DateTime.UtcNow,
                        LastLogin = DateTime.UtcNow,
                        IsActive = true
                    }
                );
            }
        }
    }
}

