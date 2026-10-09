using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using task_02_requirements_to_erd.Data;
using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Interface;
using task_02_requirements_to_erd.Models;
using task_02_requirements_to_erd.Models.Enums;

namespace task_02_requirements_to_erd.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public InstructorService(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<InstructorResponse> CreateAsync(CreateInstructorDto dto)
        {
            var EmailExist = await _userManager.FindByEmailAsync(dto.Email);

            if (EmailExist != null)
                throw new Exception("Email already exist");

            

            var instructor = new Instructor
            {
                FullName = dto.FullName.Trim(),
                Email = dto.Email.Trim(),
                Bio = dto.Bio,
                CreatedAt=DateTime.UtcNow,
                IsActive=true,
                Specialization=dto.Specialization
            };

            await using var transaction = await _context.Database.BeginTransactionAsync();


            try
            {
                _context.Instructors.Add(instructor);
                await _context.SaveChangesAsync();

                var user = new ApplicationUser
                {
                    UserName = dto.Email.Trim(),
                    Email = dto.Email.Trim(),
                    FullName = dto.FullName.Trim(),
                    Role = "Instructor",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    InstructorId = instructor.InstructorId
                };

                var createResult = await _userManager.CreateAsync(user, dto.Password);

                if (!createResult.Succeeded)
                {
                    var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));

                    throw new InvalidOperationException(errors);
                }

                var roleResult = await _userManager.AddToRoleAsync( user, "Instructor");

                if (!roleResult.Succeeded)
                {
                    var errors = string.Join( ", ", roleResult.Errors.Select(e => e.Description));

                    throw new InvalidOperationException(errors);
                }

                await transaction.CommitAsync();

                return MapToResponse(instructor);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public InstructorResponse Details(int id)
        {
            var instructor =_context.Instructors.FirstOrDefault(d=>d.InstructorId== id);

            if (instructor == null)
                return null;

            return MapToResponse(instructor);
        }

        public List<InstructorResponse> GetAll()
        {
            var instructors = _context.Instructors.ToList();

            return instructors.Select(MapToResponse).ToList();
        }

        public InstructorResponse Update(int id, UpdateInstructorDto dto)
        {
            var instructor = _context.Instructors.FirstOrDefault(d => d.InstructorId == id);

            if (instructor == null)
                return null;

           instructor.FullName=dto.FullName;
            instructor.Email=dto.Email;
            instructor.Bio=dto.Bio;
            instructor.Specialization=dto.Specialization;
            instructor.IsActive = dto.IsActive;

            _context.SaveChanges();

            return MapToResponse(instructor);
        }

        public InstructorTracksDto GetInstructorWithTracks(int id)
        {
            var instructor = _context.Instructors.Include(f=>f.TrainingTracks).FirstOrDefault(d => d.InstructorId == id);

            if (instructor == null)
                return null;

            return InstructorTracksResponse(instructor);
        }

        private InstructorResponse MapToResponse(Instructor instructor)
        {
            return new InstructorResponse
            {
                FullName= instructor.FullName,
                Email= instructor.Email,
                Bio= instructor.Bio,
                CreatedAt= instructor.CreatedAt,
                IsActive=instructor.IsActive,
                Specialization=instructor.Specialization,
                Id= instructor.InstructorId
            };
        }

        public async Task<InstructorResponse> UpdateMyProfileAsync( string userId, UpdateMyInstructorProfileDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new UnauthorizedAccessException("User not found.");

            if (user.InstructorId == null)
                throw new UnauthorizedAccessException("This account is not linked to an instructor.");

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(i => i.InstructorId == user.InstructorId);

            if (instructor == null)
                throw new KeyNotFoundException("Instructor not found.");

            instructor.FullName = dto.FullName.Trim();
            instructor.Bio = dto.Bio;
            instructor.Specialization = dto.Specialization;

            user.FullName = instructor.FullName;
            user.UpdatedAt = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();

            return MapToResponse(instructor);
        }

        public async Task<List<TrainingTrack>> GetMyTracksAsync( string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new UnauthorizedAccessException("User not found.");

            if (user.InstructorId == null)
                throw new UnauthorizedAccessException( "Instructor profile not found.");

            return await _context.TrainingTracks
                .Where(t =>t.InstructorId == user.InstructorId && !t.IsDeleted)
                .ToListAsync();
        }


        public async Task<List<StudentResponse>> GetTrackStudentsAsync(
    string userId,
    int trackId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new UnauthorizedAccessException("User not found.");

            if (user.InstructorId == null)
                throw new UnauthorizedAccessException( "Instructor profile not found.");

            var track = await _context.TrainingTracks
                .FirstOrDefaultAsync(t => t.TrainingTrackId == trackId && !t.IsDeleted);

            if (track == null)
                throw new KeyNotFoundException("Track not found.");

            if (track.InstructorId != user.InstructorId)
                throw new UnauthorizedAccessException( "You are not assigned to this track.");

            return await _context.Enrollments
                .Where(e => e.TrainingTrackId == trackId)
                .Select(e => e.Student)
                .Distinct()
                .Select(s => new StudentResponse
                {
                    StudentId = s.StudentId,
                    FullName = s.FullName,
                    Email = s.Email
                })
                .ToListAsync();
        }

        public async Task<TrackProgressResponse> GetTrackProgressAsync( string userId, int trackId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null || user.InstructorId == null)
                throw new UnauthorizedAccessException( "Instructor profile not found.");

            var track = await _context.TrainingTracks
                .FirstOrDefaultAsync(t => t.TrainingTrackId == trackId && !t.IsDeleted);

            if (track == null)
                throw new KeyNotFoundException("Track not found.");

            if (track.InstructorId != user.InstructorId)
                throw new UnauthorizedAccessException("You are not assigned to this track.");

            var enrollments = await _context.Enrollments
                .Where(e => e.TrainingTrackId == trackId)
                .ToListAsync();

            return new TrackProgressResponse
            {
                TrainingTrackId = track.TrainingTrackId,
                TrackTitle = track.Title,

                TotalEnrollments = enrollments.Count,

                ActiveEnrollments = enrollments.Count(e => e.Status == EnrollmentStatus.Active),

                CompletedEnrollments = enrollments.Count(e =>  e.Status == EnrollmentStatus.Completed),

                AverageProgress = enrollments.Count == 0 ? 0:
                Math.Round( enrollments.Where(s=>s.Status != EnrollmentStatus.Cancelled).Average(e => e.ProgressPercentage), 2)
            };
        }

        public async Task<TrackSession> CreateSessionAsync( string userId, int trackId, CreateTrackSessionDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null || user.InstructorId == null)
                throw new UnauthorizedAccessException( "Instructor profile not found.");

            var track = await _context.TrainingTracks
                .FirstOrDefaultAsync(t => t.TrainingTrackId == trackId && !t.IsDeleted);

            if (track == null)
                throw new KeyNotFoundException("Track not found.");

            if (track.InstructorId != user.InstructorId)
                throw new UnauthorizedAccessException( "You are not assigned to this track.");

            var session = new TrackSession
            {
                Title = dto.Title.Trim(),
                Notes = dto.Notes,
                SessionDate = dto.SessionDate,
                TrainingTrackId = trackId,
                CreatedAt = DateTime.UtcNow
            };

            _context.TrackSessions.Add(session);
            await _context.SaveChangesAsync();

            return session;
        }

        public async Task<TrackSession> UpdateSessionAsync( string userId, int sessionId,UpdateTrackSessionDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null || user.InstructorId == null)
                throw new UnauthorizedAccessException(
                    "Instructor profile not found.");

            var session = await _context.TrackSessions
                .Include(s => s.TrainingTrack)
                .FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null)
                throw new KeyNotFoundException("Session not found.");

            if (session.TrainingTrack.InstructorId != user.InstructorId)
                throw new UnauthorizedAccessException( "You are not allowed to update this session.");

            session.Title = dto.Title.Trim();
            session.Notes = dto.Notes;
            session.SessionDate = dto.SessionDate;
            session.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return session;
        }

        private InstructorTracksDto InstructorTracksResponse(Instructor instructor)
        {
            return new InstructorTracksDto
            {
                FullName = instructor.FullName,
                Email = instructor.Email,
                Bio = instructor.Bio,
                CreatedAt = instructor.CreatedAt,
                IsActive = instructor.IsActive,
                Specialization = instructor.Specialization,
                Id = instructor.InstructorId,
                TrainingTracks = instructor.TrainingTracks.Select(
                    d => new TrainingTrackResponse
                    {
                        Capacity = d.Capacity,
                        Title = d.Title,
                        Code = d.Code,
                        TrainingTrackId = d.TrainingTrackId,
                        IsDeleted = d.IsDeleted,
                        CreatedAt = d.CreatedAt,
                        EndDate = d.EndDate,
                        StartDate = d.StartDate,
                        Status = d.Status,
                        Description = d.Description,
                        Level = d.Level
                    }).ToList()
            };
        }
    }
}
