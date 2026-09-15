using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.Metrics;
using task_01_ef_core_modeling_drills.Data;
using task_01_ef_core_modeling_drills.DTOs;
using task_01_ef_core_modeling_drills.Interface;
using task_01_ef_core_modeling_drills.Models;

namespace task_01_ef_core_modeling_drills.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly AppDbContext _context;
        public InstructorService(AppDbContext context)
        {
            _context = context;
        }

        public InstructorDto Create(CreateInstructorDto dto)
        {
            var instructor = new Instructor
            {
                Name= dto.Name,
                Email= dto.Email,
                CreatedAt=DateTime.UtcNow,
                PhoneNumber= dto.PhoneNumber
            };

            _context.Instructors.Add(instructor);
            _context.SaveChanges();

            return MapToResponse(instructor);
        }

        public InstructorDto GeInstructorWithTrack( int id )
        {
            var instructor = _context.Instructors.Include(d=>d.TrainingTracks)
                .FirstOrDefault(d=>d.Id ==id);

            if(instructor== null)
            {
                return null;
            }

            return MapToResponse(instructor);
        }

        public InstructorDto Update(int id, UpdateInstructorDto dto)
        {
            var instructor = _context.Instructors.FirstOrDefault(d => d.Id == id);

            if (instructor == null)
                return null;

            instructor.Email= dto.Email;
            instructor.Name= dto.Name;
            instructor.PhoneNumber= dto.PhoneNumber;
            instructor.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return MapToResponse(instructor);
        }

        private InstructorDto MapToResponse(Instructor instructor)
        {
            return new InstructorDto
            {
                Id = instructor.Id,
                Name = instructor.Name,
                Email = instructor.Email,
                PhoneNumber = instructor.PhoneNumber,
                CreatedAt= instructor.CreatedAt,
                UpdatedAt= instructor.UpdatedAt,
                TrainingTracks = instructor.TrainingTracks.Select(
                    s => new TrainingTrackDto
                    {
                        Id = s.Id,
                        Title = s.Title,
                        InstructorId = s.Id
                    }).ToList()
            };
        }
    }
}
