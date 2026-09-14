using Microsoft.EntityFrameworkCore;
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

        private InstructorDto MapToResponse(Instructor instructor)
        {
            return new InstructorDto
            {
                Id = instructor.Id,
                Name = instructor.Name,
                Email = instructor.Email,
                PhoneNumber = instructor.PhoneNumber,
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
