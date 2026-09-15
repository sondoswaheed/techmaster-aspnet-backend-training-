using Microsoft.EntityFrameworkCore;
using task_02_requirements_to_erd.Models;

namespace task_02_requirements_to_erd.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Enrollment>  Enrollments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<TrainingTrack> TrainingTracks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
                .HasOne(d => d.Student)
                .WithMany(d => d.Enrollments)
                .HasForeignKey(d => d.StudentId)
                .IsRequired();

            modelBuilder.Entity<TrainingTrack>()
                .HasOne(d=>d.Instructor)
                .WithMany(d=>d.TrainingTracks)
                .HasForeignKey(d=>d.InstructorId)
                .IsRequired();

            modelBuilder.Entity<Enrollment>()
                .HasOne(d => d.TrainingTrack)
                .WithMany(d=>d.Enrollments)
                .HasForeignKey(d=>d.TrainingTrackId)
                .IsRequired();

            modelBuilder.Entity<Payment>()
                .HasOne(d => d.Enrollment)
                .WithMany(d => d.Payments)
                .HasForeignKey(d => d.EnrollmentId)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasIndex(d => d.Email)
                .IsUnique();

            modelBuilder.Entity<Instructor>()
                .HasIndex(d => d.Email)
                .IsUnique();

            modelBuilder.Entity<TrainingTrack>()
                .HasIndex(d=>d.Code)
                .IsUnique();
                
        }
    }
}
