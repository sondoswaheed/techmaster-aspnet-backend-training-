using task_01_ef_core_modeling_drills.Models.Enums;

namespace task_01_ef_core_modeling_drills.Models
{
    public class PaymentSummary
    {
        public int Id { get; set; }
        public decimal TotalRequired { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal RemainingAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public int EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }
    }
}
