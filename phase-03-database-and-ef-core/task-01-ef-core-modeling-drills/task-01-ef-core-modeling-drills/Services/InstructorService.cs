using Microsoft.EntityFrameworkCore;
using task_01_ef_core_modeling_drills.Data;
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
        public Instructor GeInstructorWithTrack( int id )
        {
            var instructor = _context.Instructors.Include(d=>d.TrainingTracks)
                .FirstOrDefault(d=>d.Id ==id);
            return instructor;
        }
    }
}
