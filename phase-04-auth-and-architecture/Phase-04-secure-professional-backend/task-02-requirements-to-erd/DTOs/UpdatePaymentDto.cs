using task_02_requirements_to_erd.Models;
using task_02_requirements_to_erd.Models.Enums;

namespace task_02_requirements_to_erd.DTOs
{
    public class UpdatePaymentDto
    {
        public PaymentStatus PaymentStatus { get; set; }
    }
}
