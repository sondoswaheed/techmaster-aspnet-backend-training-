using task_02_requirements_to_erd.Data;
using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Interface;
using task_02_requirements_to_erd.Models;
using task_02_requirements_to_erd.Models.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace task_02_requirements_to_erd.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;
        public PaymentService(AppDbContext context)
        {
            _context = context;
        }
        public PaymentResponse Create(CreatePaymentDto dto)
        {
            var EnrollExist= _context.Enrollments.Any(d=>d.EnrollmentId ==dto.EnrollmentId);

            if (!EnrollExist)
            {
                throw new InvalidOperationException("Enrollment doesn't exist");
            }

            var pay = new Payment
            {
                Amount= dto.Amount,
                PaymentDate= dto.PaymentDate,
                PaymentStatus= dto.PaymentStatus,
                PaymentMethod= dto.PaymentMethod,
                ReferenceNumber= dto.ReferenceNumber,
                Notes= dto.Notes,
                EnrollmentId= dto.EnrollmentId
            };

            _context.Payments.Add(pay);
            _context.SaveChanges();

            return MapToResponse(pay);
        }

        public List<PaymentResponse> GetAll(DateOnly? FromDate, DateOnly? ToDate, PaymentStatus? status)
        {


            var pay = _context.Payments.AsQueryable();

            if (status.HasValue)
            {
                pay=pay.Where(s=>s.PaymentStatus == status.Value);
            }
            if (FromDate.HasValue)
            {
                pay = pay.Where(p => p.PaymentDate >= FromDate.Value);
            }

            if (ToDate.HasValue)
            {
                pay = pay.Where(p => p.PaymentDate <= ToDate.Value);
            }
            

            return pay.Select(MapToResponse).ToList();

        }


        public PaymentResponse Update(int id, UpdatePaymentDto dto)
        {
            var pay=_context.Payments.FirstOrDefault(d=>d.PaymentId== id);
            
            if (pay == null)
                return null;

            pay.PaymentStatus = dto.PaymentStatus;

            _context.SaveChanges();

            return new PaymentResponse { PaymentStatus = pay.PaymentStatus };
        }

        public List<PaymentResponse> GetByEnrollmentId(int enrollmentId)
        {
            var enrollmentExists = _context.Enrollments.Any(e => e.EnrollmentId == enrollmentId);

            if (!enrollmentExists)
            {
                throw new InvalidOperationException("Enrollment doesn't exist");
            }


            var payments = _context.Payments.Where(p => p.EnrollmentId == enrollmentId).ToList();

            return payments.Select(MapToResponse).ToList();
        }

        private PaymentResponse MapToResponse(Payment payment)
        {
            return new PaymentResponse
            {
                Amount= payment.Amount,
                PaymentDate= payment.PaymentDate,
                PaymentStatus= payment.PaymentStatus,
                PaymentMethod= payment.PaymentMethod,
                ReferenceNumber= payment.ReferenceNumber,
                Notes= payment.Notes,
                EnrollmentId= payment.EnrollmentId,
                PaymentId= payment.PaymentId
                
            };
        }
    }
}
