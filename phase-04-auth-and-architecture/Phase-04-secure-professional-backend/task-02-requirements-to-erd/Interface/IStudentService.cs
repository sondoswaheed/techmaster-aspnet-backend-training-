using task_02_requirements_to_erd.DTOs;

namespace task_02_requirements_to_erd.Interface
{
    public interface IStudentService
    {
        List<StudentResponse> GetAll(int pageSize, int pageNumber, bool? IsActive, string? search);
        StudentResponse GetById(int id);
        StudentResponse Create(CreateStudentDto dto);
        StudentResponse Update(int id,UpdateStudentDto dto);
        bool Delete(int id);
        Task<StudentResponse> GetMyProfileAsync(string userId);
        //Task<StudentResponse> UpdateMyProfileAsync(string userId);
        Task<List<EnrollmentResponseDto>> GetMyEnrollmentAsync(string userId);
        Task<StudentResponse> UpdateMyProfileAsync(string userId, UpdateStudentDto request);
        Task<List<PaymentResponse>> GetMyPayments(string userId);
    }
}
