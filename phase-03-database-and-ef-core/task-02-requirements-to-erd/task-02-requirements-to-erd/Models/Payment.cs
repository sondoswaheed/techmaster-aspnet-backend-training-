using task_02_requirements_to_erd.Models.Enums;

namespace task_02_requirements_to_erd.Models
{
    public class Payment
    {
        public int PaymentId {  get; set; }
        public decimal Amount {  get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateOnly PaymentDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string ReferenceNumber { get; set; }
        public string? Notes {  get; set; }
        public int EnrollmentId {  get; set; }
        public Enrollment Enrollment { get; set; }
    }
}
