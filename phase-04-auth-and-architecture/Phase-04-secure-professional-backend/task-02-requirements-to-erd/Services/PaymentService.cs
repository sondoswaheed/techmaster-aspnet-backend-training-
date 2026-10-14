using Microsoft.EntityFrameworkCore;
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
        private readonly ILogger<PaymentService> _logger;
        private readonly ActivityLogService _activityLogService; 
        public PaymentService(AppDbContext context , ILogger<PaymentService> logger , ActivityLogService activityLogService)
        {
            _context = context;
            _logger = logger;
            _activityLogService = activityLogService;
        }
        public async Task<PaymentResponse> Create(CreatePaymentDto dto)
        {
            var enrollment =await _context.Enrollments
                .Include(e => e.Payments)
                .FirstOrDefaultAsync(e => e.EnrollmentId == dto.EnrollmentId);

            if (enrollment== null)
            {
                throw new InvalidOperationException("Enrollment doesn't exist");
            }

            if(dto.Amount <= 0)
            {
                throw new InvalidOperationException("Payment amount must be positive");
            }

            var paidAmount = enrollment.Payments
                    .Where(p => p.PaymentStatus == PaymentStatus.Paid)
                    .Sum(p => p.Amount);


            var remainingAmount = dto.TotalAmount - paidAmount;

            if(remainingAmount < dto.Amount)
            {
                throw new InvalidOperationException("Payment amount exceeds the remaining amount.");
            }

            var pay = new Payment
            {
                Amount= dto.Amount,
                PaymentDate= dto.PaymentDate,
                PaymentStatus= dto.PaymentStatus,
                PaymentMethod= dto.PaymentMethod,
                ReferenceNumber= dto.ReferenceNumber,
                Notes= dto.Notes,
                TotalAmount= dto.TotalAmount,
                EnrollmentId= dto.EnrollmentId
            };

            _context.Payments.Add(pay);
            await _context.SaveChangesAsync();

            await _activityLogService.LogAsync(action:"Create" , entityName:"Payment" ,entityId: pay.PaymentId.ToString() ,description:$"payment {pay.PaymentId} was created");

            _logger.LogInformation("Payment {PaymentId} created for Enrollment {EnrollmentId}",pay.PaymentId, pay.EnrollmentId);

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


        public async Task<PaymentResponse> Update(int id, UpdatePaymentDto dto)
        {
            var pay=await _context.Payments.FirstOrDefaultAsync(d=>d.PaymentId== id);
            
            if (pay == null)
                return null;

            var old = pay.PaymentStatus;

            pay.PaymentStatus = dto.PaymentStatus;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Payment {PaymentId} status changed from {OldStatus} to {NewStatus}",
                    pay.PaymentId,
                    old,
                    pay.PaymentStatus
            );

            await _activityLogService.LogAsync(
                action:"update",
                entityName:"payment",
                entityId:pay.PaymentId.ToString(),
                description:$"Payment {pay.PaymentId} updated its status from {old} to {pay.PaymentStatus}"
                );

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
                TotalAmount= payment.TotalAmount,
                EnrollmentId= payment.EnrollmentId,
                PaymentId= payment.PaymentId
                
            };
        }
    }
}
