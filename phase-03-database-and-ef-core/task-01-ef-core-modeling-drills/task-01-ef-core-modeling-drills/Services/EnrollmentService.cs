using Microsoft.EntityFrameworkCore;
using task_01_ef_core_modeling_drills.Data;
using task_01_ef_core_modeling_drills.DTOs;
using task_01_ef_core_modeling_drills.Interface;
using task_01_ef_core_modeling_drills.Models;

namespace task_01_ef_core_modeling_drills.Services
{
    public class EnrollmentService :IEnrollmentService
    {
        private readonly AppDbContext _context;
        public EnrollmentService(AppDbContext context)
        {
            _context = context;
        }
        public EnrollmentDto GetEnrollmentWithPaymentSummary(int id)
        {
            var enrollment = _context.Enrollments
                .Include(e => e.PaymentSummary)
                .Include(e=>e.Student)
                .FirstOrDefault(e => e.Id == id);

            if (enrollment == null)
                return null;

            return MapToResponse(enrollment);
        }

        protected EnrollmentDto MapToResponse(Enrollment enrollment)
        {
            return new EnrollmentDto
            {
                Status = enrollment.Status,
                Id = enrollment.Id,
                FinalGrade = enrollment.FinalGrade,
                EnrollmentDate = enrollment.EnrollmentDate,
                StudentId= enrollment.StudentId,
                FullName= enrollment.Student.FullName,
                PaymentSummary = enrollment.PaymentSummary==null? null :
                     new PaymentSummaryDto
                     {
                         Id = enrollment.PaymentSummary.Id,
                         TotalRequired = enrollment.PaymentSummary.TotalRequired,
                         TotalPaid = enrollment.PaymentSummary.TotalPaid,
                         RemainingAmount = enrollment.PaymentSummary.TotalRequired-
                         enrollment.PaymentSummary.TotalPaid,
                         PaymentStatus = enrollment.PaymentSummary.PaymentStatus
                     }
            };
        }
    }
}
