using Microsoft.EntityFrameworkCore;
using task_01_ef_core_modeling_drills.Models;
using task_01_ef_core_modeling_drills.Models.Enums;

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
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<PaymentSummary> PaymentSummaries { get; set; }

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

            modelBuilder.Entity<Enrollment>()
                .HasOne(k=>k.Student)
                .WithMany(f=>f.Enrollments)
                .HasForeignKey(k=>k.StudentId)
                .IsRequired();

            modelBuilder.Entity<Enrollment>()
                .HasOne(f=>f.TrainingTrack)
                .WithMany(d=>d.Enrollments)
                .HasForeignKey(d=>d.TrainingTrackId)
                .IsRequired();

            modelBuilder.Entity<PaymentSummary>()
                .HasOne(d => d.Enrollment)
                .WithOne(l => l.PaymentSummary)
                .HasForeignKey<PaymentSummary>(f => f.EnrollmentId)
                .IsRequired();



            // 1. Seed Instructors (At least 2)
            modelBuilder.Entity<Instructor>().HasData(
                new Instructor
                {
                    Id = 1,
                    Name = "Mohammed ahmed",
                    Email = "mohamed@gmail.com",
                    PhoneNumber = "01087655678"
                },
                new Instructor
                {
                    Id = 2,
                    Name = "Sara Hassan",
                    Email = "sara.hassan@gmail.com",
                    PhoneNumber = "01123456789"
                }
            );

            // 2. Seed Training Tracks (At least 3)
            modelBuilder.Entity<TrainingTrack>().HasData(
                new TrainingTrack
                {
                    Id = 1,
                    Title = "Frontend",
                    InstructorId = 1
                },
                new TrainingTrack
                {
                    Id = 2,
                    Title = "Backend",
                    InstructorId = 1
                },
                new TrainingTrack
                {
                    Id = 3,
                    Title = "UI/UX Design",
                    InstructorId = 2
                }
            );

            // 3. Seed Students & StudentProfiles 
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    FullName = "Sondos waheed",
                    Email = "Sonds@gmail.com",
                    CreatedAt = new DateTime(2026, 07, 18),
                    IsActive = true,
                    IsDeleted=false,
                    DeletedAt=null
                },
                new Student
                {
                    Id = 2,
                    FullName = "Omar Ali",
                    Email = "omar.ali@gmail.com",
                    CreatedAt = new DateTime(2026, 08, 01),
                    IsActive = true,
                    IsDeleted = false,
                    DeletedAt = null
                },
                new Student
                {
                    Id = 3,
                    FullName = "Nour Mahmoud",
                    Email = "nour.m@gmail.com",
                    CreatedAt = new DateTime(2026, 08, 10),
                    IsActive = true,
                    IsDeleted = true,
                    DeletedAt = null
                },
                new Student
                {
                    Id = 4,
                    FullName = "Youssef Ibrahim",
                    Email = "youssef.i@gmail.com",
                    CreatedAt = new DateTime(2026, 08, 15),
                    IsActive = true,
                    IsDeleted = false,
                    DeletedAt = null
                },
                new Student
                {
                    Id = 5,
                    FullName = "Mariam Khaled",
                    Email = "mariam.k@gmail.com",
                    CreatedAt = new DateTime(2026, 09, 01),
                    IsActive = false,
                    IsDeleted = false,
                    DeletedAt = null
                }
            );

            modelBuilder.Entity<StudentProfile>().HasData(
                new StudentProfile
                {
                    Id = 1,
                    StudentId = 1,
                    NationalId = "98464748493033",
                    EmergencyPhone = "01063500543",
                    Address = "21 sreet",
                    DateOfBirth = new DateOnly(2005, 04, 18)
                },
                new StudentProfile
                {
                    Id = 2,
                    StudentId = 2,
                    NationalId = "29901011234567",
                    EmergencyPhone = "01011112222",
                    Address = "Nasr City, Cairo",
                    DateOfBirth = new DateOnly(1999, 01, 01)
                },
                new StudentProfile
                {
                    Id = 3,
                    StudentId = 3,
                    NationalId = "30105051234568",
                    EmergencyPhone = "01033334444",
                    Address = "Maadi, Cairo",
                    DateOfBirth = new DateOnly(2001, 05, 05)
                },
                new StudentProfile
                {
                    Id = 4,
                    StudentId = 4,
                    NationalId = "29811111234569",
                    EmergencyPhone = "01055556666",
                    Address = "Giza",
                    DateOfBirth = new DateOnly(1998, 11, 11)
                },
                new StudentProfile
                {
                    Id = 5,
                    StudentId = 5,
                    NationalId = "30203031234570",
                    EmergencyPhone = "01077778888",
                    Address = "Alexandria",
                    DateOfBirth = new DateOnly(2002, 03, 03)
                }
            );

            // 4. Seed Enrollments (At least 5)
            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment
                {
                    Id = 1,
                    StudentId = 1,
                    TrainingTrackId = 1,
                    EnrollmentDate = new DateTime(2026, 9, 14),
                    Status = EnrollmentStatus.Active,
                    FinalGrade = null
                },
                new Enrollment
                {
                    Id = 2,
                    StudentId = 1,
                    TrainingTrackId = 2,
                    EnrollmentDate = new DateTime(2026, 9, 14),
                    Status = EnrollmentStatus.Completed,
                    FinalGrade = 90
                },
                new Enrollment
                {
                    Id = 3,
                    StudentId = 2,
                    TrainingTrackId = 2,
                    EnrollmentDate = new DateTime(2026, 9, 10),
                    Status = EnrollmentStatus.Active,
                    FinalGrade = null
                },
                new Enrollment
                {
                    Id = 4,
                    StudentId = 3,
                    TrainingTrackId = 3,
                    EnrollmentDate = new DateTime(2026, 9, 01),
                    Status = EnrollmentStatus.Completed,
                    FinalGrade = 95
                },
                new Enrollment
                {
                    Id = 5,
                    StudentId = 4,
                    TrainingTrackId = 1,
                    EnrollmentDate = new DateTime(2026, 9, 12),
                    Status = EnrollmentStatus.Active,
                    FinalGrade = null
                }
            );

        }
    }
}
