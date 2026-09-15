using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            else
            {
                query = query.Where(s => s.IsDeleted);
            }

                var students = query.Include(s => s.Enrollments)
                        .ThenInclude(e => e.TrainingTrack)
                        .ToList();

            return students.Select(s => MapToResponse(s)).ToList();
        }

        public StudentDto Create(CreateStudentDto dto)
        {
            var student = new Student
            {
                FullName = dto.FullName,
                Email = dto.Email,
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                IsDeleted=false
            };
            _context.Students.Add(student);
            _context.SaveChanges();

            return MapToResponse(student);
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
                CreatedAt=student.CreatedAt,
                UpdatedAt=student.UpdatedAt,
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


        public async Task<PaginationResult<StudentListItemDto>> GetAll(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentException("Page number must be greater than 0.");

            if (pageSize < 1 || pageSize > 50)
                throw new ArgumentException("Page size must be between 1 and 50.");

            var query = _context.Students
                .Where(s => !s.IsDeleted);

            var totalCount = await query.CountAsync();

            var skip = (pageNumber - 1) * pageSize;

            var students = await query
                .Select(s => new StudentListItemDto
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Email = s.Email,
                    CreatedAt = s.CreatedAt,
                    UpdatedAt = s.UpdatedAt,
                    IsActive = s.IsActive
                })
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return new PaginationResult<StudentListItemDto>
            {
                Items = students,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }


        public StudentDto Update(int id, UpdateStudentDto dto)
        {
            var student = _context.Students.FirstOrDefault(d => d.Id == id);
            
            if (student == null)
                return null;

            student.FullName = dto.FullName;
            student.Email = dto.Email;
            student.IsActive = dto.IsActive;
            student.IsDeleted = dto.IsDeleted;
            student.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return MapToResponse(student);

        }
    }
}
