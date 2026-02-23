using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using University_test_system.Models;
using Microsoft.EntityFrameworkCore;


namespace University_test_system.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }
    
    public DbSet<User> Users => Set<User>();
    public DbSet<Test> Tests => Set<Test>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Answer> Answers => Set<Answer>();
    public DbSet<Attempt> Attempts => Set<Attempt>();
    public DbSet<Faculty> Faculties => Set<Faculty>();
    public DbSet<TestFaculty> TestFaculties => Set<TestFaculty>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Faculty)
            .WithMany(f => f.Users)
            .HasForeignKey(u => u.FacultyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TestFaculty>()
            .HasKey(tf => new { tf.TestId, tf.FacultyId });
        
        modelBuilder.Entity<Subject>().HasData(
            new Subject { Id = 1, Name = "Математика", Type = "STEM", Description = "" },
            new Subject { Id = 2, Name = "Фізика", Type = "STEM", Description = "" },
            new Subject { Id = 3, Name = "Хімія", Type = "STEM", Description = "" },
            new Subject { Id = 4, Name = "Біологія", Type = "STEM", Description = "" },
            new Subject { Id = 5, Name = "Історія", Type = "Гуманітарний", Description = "" },
            new Subject { Id = 6, Name = "Іноземна мова", Type = "Гуманітарний", Description = "" },
            new Subject { Id = 7, Name = "Література", Type = "Гуманітарний", Description = "" }
        );

        modelBuilder.Entity<Faculty>().HasData(
            new Faculty { Id = 1, Name = "Факультет інформатики та кібернетики" },
            new Faculty { Id = 2, Name = "Факультет фізики" },
            new Faculty { Id = 3, Name = "Факультет філології" },
            new Faculty { Id = 4, Name = "Факультет історії" },
            new Faculty { Id = 5, Name = "Механіко-математичний факультет" }
        );
    }
    
}