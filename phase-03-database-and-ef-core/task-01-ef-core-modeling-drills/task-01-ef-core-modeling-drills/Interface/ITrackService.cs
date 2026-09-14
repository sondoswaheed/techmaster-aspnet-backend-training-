using task_01_ef_core_modeling_drills.DTOs;

namespace task_01_ef_core_modeling_drills.Interface
{
    public interface ITrackService
    {
        TrainingTrackDto GettrackWithStudents(int id);
    }
}
