using Microsoft.EntityFrameworkCore;
using task_01_ef_core_modeling_drills.Models;

namespace task_01_ef_core_modeling_drills.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentProfile> StudentProfiles { get; set; }
        public DbSet<TrainingTrack> TrainingTracks { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentProfile>()
                .HasOne(s => s.student)
                .WithOne(s => s.studentProfile)
                .HasForeignKey<StudentProfile>(d => d.StudentId)
                .IsRequired();

            modelBuilder.Entity<TrainingTrack>()
                .HasOne(d => d.Instructor)
                .WithMany(d => d.TrainingTracks)
                .HasForeignKey(l => l.InstructorId)
                .IsRequired();

            modelBuilder.Entity<Instructor>()
                .HasData(new Instructor
                {
                    Id=1,
                    Name="Mohammed ahmed",
                    Email="mohamed@gmail.com",
                    PhoneNumber="01087655678"
                });
            modelBuilder.Entity<TrainingTrack>()
                .HasData(new TrainingTrack
                {
                    Id = 1,
                    Title = "Frontend",
                    InstructorId = 1
                },
                    new TrainingTrack
                    {
                        Id = 2,
                        Title="Backend",
                        InstructorId = 1
                    }
                );

            modelBuilder.Entity<Student>()
                .HasData(new Student
                {
                    Email = "Sonds@gmail.com",
                    CreatedAt = new DateTime(2026, 07, 18),
                    IsActive = true,
                    Id = 1,
                    FullName = "Sondos waheed"
                });
            modelBuilder.Entity<StudentProfile>()
                .HasData(new StudentProfile
                {
                    StudentId = 1,
                    Id = 1,
                    NationalId = "98464748493033",
                    EmergencyPhone = "01063500543",
                    Address = "21 sreet",
                    DateOfBirth = new DateOnly(2005, 04, 18)
                });
        }
    }
}
