using Microsoft.EntityFrameworkCore;
using task_02_requirements_to_erd.Data;
using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Interface;
using task_02_requirements_to_erd.Models;

namespace task_02_requirements_to_erd.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly AppDbContext _context;
        public InstructorService(AppDbContext context)
        {
            _context = context;
        }
        public InstructorResponse Create(CreateInstructorDto dto)
        {
            var instructor = new Instructor
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Bio = dto.Bio,
                CreatedAt=DateTime.UtcNow,
                IsActive=true,
                Specialization=dto.Specialization
            };

            _context.Instructors.Add(instructor);
            _context.SaveChanges();

            return MapToResponse(instructor);
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
