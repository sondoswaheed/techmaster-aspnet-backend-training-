using Azure.Core;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentService(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

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
            };
        }

        public async Task<StudentResponse> GetMyProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new UnauthorizedAccessException("user not found");

            if (user.StudentId == null)
                throw new UnauthorizedAccessException("this user not linked to a student");

            var student =await _context.Students.FirstOrDefaultAsync(d=>d.StudentId==user.StudentId && !d.IsDeleted);

            if (student == null)
                return null;

            return MapToResponse(student);
        }

        public async Task<StudentResponse> UpdateMyProfileAsync(string userId , UpdateStudentDto request)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) throw new UnauthorizedAccessException("user not found");

            if (user.StudentId == null)
                throw new UnauthorizedAccessException("this user not linked to a student");

            var emailExist = await _userManager.FindByEmailAsync(user.Email);

            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.StudentId == user.StudentId &&!s.IsDeleted);

            if (student == null)
                throw new KeyNotFoundException("Student profile not found.");

            student.FullName = request.FullName.Trim();
            student.PhoneNumber = request.PhoneNumber?.Trim();
            student.Email = request.Email?.Trim();
            student.UpdatedAt = DateTime.UtcNow;

            user.FullName = student.FullName;
            user.UpdatedAt = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();

            return MapToResponse(student);

        }


        public async Task<List<EnrollmentResponseDto>> GetMyEnrollmentAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new UnauthorizedAccessException("user not found");

            if (user.StudentId == null)
                throw new UnauthorizedAccessException("this user not linked to a student");



            var enrol = _context.Enrollments.Where(s => s.StudentId == user.StudentId)
                .Select(d => new EnrollmentResponseDto
                {
                    EnrollmentId = d.EnrollmentId,
                    EnrollmentDate = d.EnrollmentDate,
                    Status = d.Status,
                    CreatedAt = d.CreatedAt
                }).ToList();

            return enrol;
        }


        public async Task<List<PaymentResponse>> GetMyPayments(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new UnauthorizedAccessException("user not found");

            if (user.StudentId == null)
                throw new UnauthorizedAccessException("this user not linked to a student");

            var payments = await _context.Payments
                .Where(p => p.Enrollment.StudentId == user.StudentId)
                .Select(p => new PaymentResponse
                    {
                        PaymentId = p.PaymentId,
                        Amount = p.Amount,
                        PaymentMethod = p.PaymentMethod,
                        PaymentDate = p.PaymentDate,
                        PaymentStatus = p.PaymentStatus,
                        ReferenceNumber = p.ReferenceNumber,
                        Notes = p.Notes,
                        EnrollmentId = p.EnrollmentId
                    })
                    .ToListAsync();

            return payments;    
        }

    }
}