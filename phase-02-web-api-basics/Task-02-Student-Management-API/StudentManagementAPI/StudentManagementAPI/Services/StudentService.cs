using StudentManagementAPI.DTOs;
using StudentManagementAPI.Interfaces;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Services
{
    public class StudentService : IStudentService
    {
        private static readonly List<Student> students = new List<Student>();
        public List<StudentResponse> GetAll(string? search, string? trackName, bool? isActive)
        {
            var query = students.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.FullName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                         s.Email.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(trackName))
            {
                query = query.Where(s => s.TrackName.Equals(trackName, StringComparison.OrdinalIgnoreCase));
            }

            if (isActive.HasValue)
            {
                query = query.Where(s => s.IsActive == isActive.Value);
            }

            return query.Select(s => MapToResponse(s)).ToList();
        }

        public StudentResponse CreateStudent(CreateStudentRequest createDto)
        {
            var emailExists = students.Any(s => s.Email.Equals(createDto.Email, StringComparison.OrdinalIgnoreCase));

            if (emailExists)
            {
                throw new InvalidOperationException("Email already exists.");
            }

            var student = new Student
            {
                StudentId = students.Count + 1,
                FullName = createDto.FullName,
                Email = createDto.Email,
                PhoneNumber = createDto.PhoneNumber,
                TrackName = createDto.TrackName,
                EnrollmentDate = DateTime.Now,
                IsActive = true,
                GitHubProfileUrl = createDto.GitHubProfileUrl,
                LinkedInProfileUrl = createDto.LinkedInProfileUrl
            };

            students.Add(student);

            return MapToResponse(student);
        }
        public StudentResponse? GetById(int id)
        {
            var student = students.FirstOrDefault(s => s.StudentId == id);

            if (student == null)
            {
                return null;
            }

            return MapToResponse(student);
        }

        public StudentResponse? UpdateStudent(int id, UpdateStudentRequest updateDto)
        {
            var student = students.FirstOrDefault(s => s.StudentId == id);

            if (student == null)
            {
                return null;
            }

            var emailExists = students.Any(s => s.StudentId != id && s.Email.Equals(updateDto.Email, StringComparison.OrdinalIgnoreCase));

            if (emailExists)
            {
                throw new InvalidOperationException("Email already exists.");
            }

            student.FullName = updateDto.FullName;
            student.Email = updateDto.Email;
            student.TrackName = updateDto.TrackName;
            student.PhoneNumber = updateDto.PhoneNumber;
            student.GitHubProfileUrl = updateDto.GitHubProfileUrl;
            student.LinkedInProfileUrl = updateDto.LinkedInProfileUrl;

            return MapToResponse(student);
        }

        public StudentResponse? UpdateStudentStatus(int id, UpdateStudentStatusRequest update)
        {
            var student = students.FirstOrDefault(s => s.StudentId == id);

            if (student == null)
            {
                return null;
            }

            student.IsActive = update.IsActive;

            return MapToResponse(student);
        }

        public bool DeleteStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.StudentId == id);

            if (student == null)
            {
                return false;
            }

            students.Remove(student);
            return true;
        }

        public List<StudentResponse> GetByTrack(string trackName)
        {
            var filteredStudents = students
                .Where(s => s.TrackName.Equals(trackName, StringComparison.OrdinalIgnoreCase))
                .Select(s => MapToResponse(s))
                .ToList();

            return filteredStudents;
        }

        public StudentStatsResponse GetStudentStats()
        {
            var stats = new StudentStatsResponse
            {
                TotalStudents = students.Count,
                ActiveStudents = students.Count(s => s.IsActive == true),
                InactiveStudents = students.Count(s => s.IsActive == false)
            };

            return stats;
        }
        //helper method
        private StudentResponse MapToResponse(Student student)
        {
            return new StudentResponse
            {
                StudentId = student.StudentId,
                FullName = student.FullName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                TrackName = student.TrackName,
                EnrollmentDate = student.EnrollmentDate,
                IsActive = student.IsActive,
                GitHubProfileUrl = student.GitHubProfileUrl,
                LinkedInProfileUrl = student.LinkedInProfileUrl
            };
        }
    }
}