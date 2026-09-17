using Microsoft.EntityFrameworkCore;
using System.Linq;
using task_02_requirements_to_erd.Data;
using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Interface;
using task_02_requirements_to_erd.Models;

namespace task_02_requirements_to_erd.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public List<StudentResponse> GetAll(int pageSize=10,int pageNumber=1,bool? IsActive=null,string? search=null)
        {
            var query = _context.Students.Where(s => !s.IsDeleted).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.FullName.Contains(search) || s.Email.Contains(search));
            }

            if (IsActive.HasValue)
            {
                query = query.Where(s => s.IsActive == IsActive.Value);
            }

            query = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            var students = query.ToList();

            return students.Select(MapToResponse).ToList();
        }

        public StudentResponse GetById(int id)
        {
            var student = _context.Students.Include(d=>d.Enrollments).FirstOrDefault(d => d.StudentId == id);

            if (student == null)
                return null;

            return MapToEnrollmentResponse(student);
        }

       
        public StudentResponse Create(CreateStudentDto dto)
        {
            var emailexist = _context.Students.Any(d => d.Email ==dto.Email);

            if (emailexist)
            {
                throw new InvalidOperationException("email already exist");
            }

            var student = new Student
            {
                FullName= dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                CreatedAt=DateTime.UtcNow,
                IsActive=true,
                IsDeleted=false
            };

            _context.Students.Add(student);
            _context.SaveChanges();

            return MapToResponse(student);
        }

        public StudentResponse Update(int id, UpdateStudentDto dto)
        {
            var student = _context.Students.FirstOrDefault(d => d.StudentId == id);

            if (student == null)
                return null;

            var emailexist = _context.Students.Any(d => d.Email == dto.Email && d.StudentId != id);

            if (emailexist)
            {
                throw new InvalidOperationException("Email already exist");
            }

            student.FullName=dto.FullName;
            student.Email=dto.Email; 
            student.PhoneNumber=dto.PhoneNumber;
            student.UpdatedAt=DateTime.UtcNow;
            student.IsActive = dto.IsActive;

            _context.SaveChanges();

            return MapToResponse(student);
        }

        public bool Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(d => d.StudentId == id);

            if (student == null)
                return false;

            student.IsActive=false;
            student.IsDeleted = true;
            student.DeletedAt=DateTime.UtcNow;

            _context.SaveChanges();

            return true;
            
        }


        private StudentResponse MapToResponse(Student student)
        {
            return new StudentResponse
            {
                StudentId = student.StudentId,
                FullName = student.FullName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                IsActive = student.IsActive,
                CreatedAt = student.CreatedAt,
                UpdatedAt = student.UpdatedAt
            };
        }

        private StudentResponse MapToEnrollmentResponse(Student student)
        {
            return new StudentResponse
            {
                StudentId = student.StudentId,
                FullName = student.FullName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                IsActive = student.IsActive,
                CreatedAt = student.CreatedAt,
                UpdatedAt = student.UpdatedAt,
                TotalEnrollment =student.Enrollments.Count
                //Enrollments = student.Enrollments.Select(
                //    d => new EnrollmentResponseDto
                //    {
                //        EnrollmentId = d.EnrollmentId,
                //        EnrollmentDate=d.EnrollmentDate,
                //        Status = d.Status
                //    }).ToList()
            };
        }

        
    }
}