using task_02_requirements_to_erd.DTOs;

namespace task_02_requirements_to_erd.Interface
{
    public interface IInstructorService
    {
        InstructorResponse Create(CreateInstructorDto dto);
        InstructorResponse Update(int id , UpdateInstructorDto dto);
        InstructorResponse Details(int id);
        List<InstructorResponse> GetAll();

        InstructorTracksDto GetInstructorWithTracks(int id);
    }
}
