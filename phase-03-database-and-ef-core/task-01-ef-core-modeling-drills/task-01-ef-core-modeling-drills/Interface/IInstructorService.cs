using task_01_ef_core_modeling_drills.DTOs;
using task_01_ef_core_modeling_drills.Models;

namespace task_01_ef_core_modeling_drills.Interface
{
    public interface IInstructorService
    {
        InstructorDto GeInstructorWithTrack(int id); 
    }
}
