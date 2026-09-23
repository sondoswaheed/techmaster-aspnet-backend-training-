using Microsoft.EntityFrameworkCore;
using task_02_requirements_to_erd.Data;
using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Interface;
using task_02_requirements_to_erd.Models;
using task_02_requirements_to_erd.Models.Enums;

namespace task_02_requirements_to_erd.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly AppDbContext _context;
        public EnrollmentService(AppDbContext context)
        {
            _context = context;
        }
        public EnrollmentResponseDto Create(CreateEnrollmentDto dto)
        {
            var studentExist= _context.Students.Any(s=>s.StudentId==dto.StudentId);
            if(!studentExist)
            {
                throw new InvalidOperationException("Student doesn't exist");
            }

            var track= _context.TrainingTracks.FirstOrDefault(t => t.TrainingTrackId == dto.TrainingTrackId);

            if (track ==null)
            {
                throw new InvalidOperationException("Training track doesn't exist");
            }

            var duplicate = _context.Enrollments
                .Any(s => s.TrainingTrackId == dto.TrainingTrackId 
                && s.StudentId == dto.StudentId 
                && s.Status== EnrollmentStatus.Active);

            if(duplicate)
                throw new InvalidOperationException("Student is already enrolled in this track");


            var enrolledCount = _context.Enrollments.Count(e => e.TrainingTrackId == dto.TrainingTrackId && e.Status ==EnrollmentStatus.Active);

          

            if (track.Status == TrainingStatus.Finished || track.Status == TrainingStatus.Cancelled)
            {
                throw new InvalidOperationException(
                    "Cannot enroll in a closed track.");
            }

            if (enrolledCount >= track.Capacity)
            {
                throw new InvalidOperationException("Training track is full");
            }

            var student = new Enrollment
            {
                EnrollmentDate = dto.EnrollmentDate,
                Status = EnrollmentStatus.Pending,
                FinalResult = dto.FinalResult,
                CreatedAt = DateTime.UtcNow,
                ProgressPercentage = dto.ProgressPercentage,
                StudentId = dto.StudentId,
                TrainingTrackId = dto.TrainingTrackId
            };
             
            _context.Enrollments.Add(student);
            _context.SaveChanges();

            return MApToResponse(student);
        }

        public EnrollmentResponseDto Details(int id)
        {
            var enroll=_context.Enrollments
                //.Include(s=>s.Student)
                //.Include(t=>t.TrainingTrack)
                .Include(p=>p.Payments)
                .FirstOrDefault(s=>s.EnrollmentId==id);

            if (enroll == null)
                return null;
             return MApToResponse(enroll);
        }

        public List<EnrollmentResponseDto> GetAll(EnrollmentStatus? status, int? trackId, int? studentId, PaymentStatus? paymentStatus)
        {
            var enroll=_context.Enrollments.Include(s=>s.Payments).AsQueryable();

            if (status.HasValue)
                enroll=enroll.Where(s=>s.Status==status.Value);

            if(trackId.HasValue)
                enroll=enroll.Where(s=>s.TrainingTrackId==trackId.Value);

            if(studentId.HasValue)
                enroll=enroll.Where(s=>s.StudentId==studentId.Value);

            if(paymentStatus.HasValue)
                enroll=enroll.Where(s=>s.Payments.Any(a=>a.PaymentStatus==paymentStatus.Value));

            var enrollment = enroll.ToList();

            return enrollment.Select(MApToResponse).ToList();
        }

        public List<EnrollmentResponseDto> GetStudentById(int id)
        {
            var studentExist = _context.Students.Any(s => s.StudentId == id);

            if (!studentExist)
            {
                throw new InvalidOperationException("Student doesn't exist");
            }

            var students = _context.Enrollments.Include(d=>d.Payments).Where(s => s.StudentId == id).ToList();

            return students.Select(MApToResponse).ToList();
        }

        public List<StudentResponse> GetTracksById(int id)
        {
            var trackExist = _context.TrainingTracks.Any(t => t.TrainingTrackId == id);

            if (!trackExist)
            {
                throw new InvalidOperationException("Training track doesn't exist");
            }

            var students = _context.Enrollments.Where(s => s.TrainingTrackId==id).Select(s=>s.Student).ToList();

            return students.Select(s => new StudentResponse
            {
                StudentId = s.StudentId,
                FullName = s.FullName,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                IsActive = s.IsActive
            }).ToList();
        }

        public EnrollmentResponseDto UpdateStatus(int id,EnrollmentStatus status)
        {
            var enroll=_context.Enrollments.FirstOrDefault(s=>s.EnrollmentId==id);

            if (enroll == null)
                return null;



            if (!IsValidTransition(enroll.Status, status))
            {
                throw new InvalidOperationException( $"Cannot change enrollment status from {enroll.Status} to {status}");
            }


            if (enroll.Status == EnrollmentStatus.Pending && status == EnrollmentStatus.Active)
            {

                var checkstatus = enroll.Payments.Any(s => s.PaymentStatus == PaymentStatus.Paid);

                if (!checkstatus)
                {
                    throw new InvalidOperationException("can't create enrollment without paid payment");
                }
            }
            enroll.Status = status;
            enroll.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return new EnrollmentResponseDto { Status=status };
        }


        private EnrollmentResponseDto MApToResponse(Enrollment enrollment)
        {
            return new EnrollmentResponseDto
            {
                Status = enrollment.Status,
                EnrollmentId = enrollment.EnrollmentId,
                TrainingTrackId = enrollment.TrainingTrackId,
                StudentId = enrollment.StudentId,
                ProgressPercentage = enrollment.ProgressPercentage,
                CreatedAt = enrollment.CreatedAt,
                UpdatedAt = enrollment.UpdatedAt,
                EnrollmentDate = enrollment.EnrollmentDate,
                FinalResult = enrollment.FinalResult,
                Payments = enrollment.Payments.Select(
                    s => new PaymentResponse
                    {
                        PaymentId = s.PaymentId,
                        PaymentMethod = s.PaymentMethod,
                        PaymentStatus = s.PaymentStatus,
                        Amount = s.Amount,
                        PaymentDate = s.PaymentDate
                    }).ToList()
            };
        }

        private bool IsValidTransition(EnrollmentStatus currentStatus, EnrollmentStatus newStatus)
        {

            if (currentStatus == EnrollmentStatus.Pending && newStatus == EnrollmentStatus.Active)
                return true;


            if (currentStatus == EnrollmentStatus.Active &&
                (newStatus == EnrollmentStatus.Completed ||newStatus == EnrollmentStatus.Cancelled))
            {
                return true;
            }

            return false;
        }
    }
}
