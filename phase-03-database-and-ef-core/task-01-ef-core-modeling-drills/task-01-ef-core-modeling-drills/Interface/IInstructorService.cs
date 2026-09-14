using task_01_ef_core_modeling_drills.Models;

namespace task_01_ef_core_modeling_drills.Interface
{
    public interface IInstructorService
    {
        Instructor GeInstructorWithTrack(int id); 
    }
}
