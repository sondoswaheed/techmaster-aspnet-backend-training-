using Microsoft.EntityFrameworkCore;
using task_01_ef_core_modeling_drills.Data;
using task_01_ef_core_modeling_drills.DTOs;
using task_01_ef_core_modeling_drills.Interface;
using task_01_ef_core_modeling_drills.Models;

namespace task_01_ef_core_modeling_drills.Services
{
    public class TrackService : ITrackService
    {
        private readonly AppDbContext _context;
        public TrackService(AppDbContext context)
        {
            _context = context;
        }
        public TrainingTrackDto GettrackWithStudents(int id)
        {
            var result =_context.TrainingTracks.Include(d=>d.Enrollments)
                .ThenInclude(d=>d.Student).FirstOrDefault(s=>s.Id == id);

            if (result == null)
                return null;

            return MapToResponse(result);
        }


       public List<TrackDetailsDto> Details()
        {
            var track = _context.TrainingTracks.Select(f => new TrackDetailsDto
            {
                Id = f.Id,
                Title = f.Title
            }).ToList();

            return track;
        }



        protected TrainingTrackDto MapToResponse(TrainingTrack track)
        {
            return new TrainingTrackDto
            {
                Id = track.Id,
                InstructorId = track.InstructorId,
                Title = track.Title,
                Enrollments = track.Enrollments.Select(
                    s => new EnrollmentDto
                    {
                        Id = s.Id,
                        Status=s.Status,
                        EnrollmentDate= s.EnrollmentDate,
                        FinalGrade  = s.FinalGrade,
                        StudentId=s.StudentId,
                        FullName=s.Student.FullName,
                    }).ToList()
            };
        }
    }
}
