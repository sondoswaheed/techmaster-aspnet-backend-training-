using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Models.Enums;

namespace task_02_requirements_to_erd.Interface
{
    public interface IPaymentService
    {
        List<PaymentResponse> GetAll(DateOnly? FromDate ,DateOnly? ToDate,PaymentStatus? status);
        //PaymentResponse GetPayment(int id);
        Task<PaymentResponse> Create(CreatePaymentDto dto);
        Task<PaymentResponse> Update(int id,UpdatePaymentDto dto);

        List<PaymentResponse> GetByEnrollmentId(int id);
    }
}
