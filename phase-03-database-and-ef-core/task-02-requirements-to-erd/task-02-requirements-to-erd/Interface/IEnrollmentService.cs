using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Models.Enums;
namespace task_02_requirements_to_erd.Interface
{
    public interface IEnrollmentService
    {
        List<EnrollmentResponseDto> GetAll(EnrollmentStatus? status, int? trackId, int? studentId, PaymentStatus? paymentStatus);

        EnrollmentResponseDto Details(int id);
        EnrollmentResponseDto Create(CreateEnrollmentDto dto);
        EnrollmentResponseDto UpdateStatus(int id, EnrollmentStatus status);

        List<EnrollmentResponseDto> GetStudentById(int id);
        List<StudentResponse> GetTracksById(int id);
    }
}