using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Models.Enums;

namespace task_02_requirements_to_erd.Interface
{
    public interface ITrainingService
    {
        List<TrainingTrackResponse> GetAll(string? keyword, int? level, EnrollmentStatus? status, int? instructorId);
        TrainingTrackResponse Details(int id);
        TrainingTrackResponse Create(CreateTrackDto dto);
        TrainingTrackResponse Update(int id, UpdateTrackDto dto);
        bool Delete(int id);
    }
}
