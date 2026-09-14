using Microsoft.EntityFrameworkCore;
using task_01_ef_core_modeling_drills.Data;
using task_01_ef_core_modeling_drills.DTOs;
using task_01_ef_core_modeling_drills.Interface;
using task_01_ef_core_modeling_drills.Models;

namespace task_01_ef_core_modeling_drills.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        public StudentService(AppDbContext context)
        {
            _context = context;
        }
        public StudentDto GetStudentWithTracks(int id)
        {
            var student = _context.Students.Include(f => f.Enrollments)
                .ThenInclude(f => f.TrainingTrack).FirstOrDefault(f=>f.Id==id);

            if (student == null)
                return null;
            
            return MapToResponse(student);

        }

        public bool DeleteStudent(int id)
        {
            var student = _context.Students
                .FirstOrDefault(s => s.Id == id);

            if (student == null)
                return false;

            student.IsDeleted = true;
            student.DeletedAt = DateTime.Now;

            _context.SaveChanges();

            return true;
        }

        public List<StudentDto> GetStudents(bool includeDeleted = false)
        {
            var query = _context.Students.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(s => !s.IsDeleted);
            }

            var students = query.Include(s => s.Enrollments)
                    .ThenInclude(e => e.TrainingTrack)
                    .ToList();

            return students.Select(s => MapToResponse(s)).ToList();
        }


        protected StudentDto MapToResponse(Student student)
        {
            return new StudentDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                IsActive = student.IsActive,
                IsDeleted=student.IsDeleted,
                DeletedAt=student.DeletedAt,
                Enrollments = student.Enrollments.Select(
                    s => new EnrollmentDto
                    {
                        Id = s.Id,
                        Status = s.Status,
                        EnrollmentDate = s.EnrollmentDate,
                        FinalGrade = s.FinalGrade,
                        TrainingTrackId = s.TrainingTrackId,
                        TrainingTrackTitle= s.TrainingTrack.Title
                        
                    }).ToList()
            };
        }
    }
}
