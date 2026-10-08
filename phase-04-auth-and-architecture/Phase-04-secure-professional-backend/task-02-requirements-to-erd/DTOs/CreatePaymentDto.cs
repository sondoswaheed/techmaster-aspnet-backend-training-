using task_02_requirements_to_erd.Models;
using task_02_requirements_to_erd.Models.Enums;

namespace task_02_requirements_to_erd.DTOs
{
    public class CreatePaymentDto
    {
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateOnly PaymentDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string ReferenceNumber { get; set; }
        public string? Notes { get; set; }
        public decimal TotalAmount { get; set; }
        public int EnrollmentId { get; set; }
    }
}
