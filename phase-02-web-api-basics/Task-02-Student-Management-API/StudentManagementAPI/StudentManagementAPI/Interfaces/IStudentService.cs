using StudentManagementAPI.DTOs;

namespace StudentManagementAPI.Interfaces
{
    public interface IStudentService
    {
        List<StudentResponse> GetAll(string? search, string? trackName, bool? isActive);

        StudentResponse? GetById(int id);

        StudentResponse CreateStudent(CreateStudentRequest createDto);

        StudentResponse? UpdateStudent(int id, UpdateStudentRequest updateDto);

        StudentResponse? UpdateStudentStatus(int id, UpdateStudentStatusRequest update);

        bool DeleteStudent(int id);

        List<StudentResponse> GetByTrack(string trackName);

        StudentStatsResponse GetStudentStats();
    }
}