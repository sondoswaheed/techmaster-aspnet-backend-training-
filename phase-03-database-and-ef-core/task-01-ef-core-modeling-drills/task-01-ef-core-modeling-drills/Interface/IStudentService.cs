using task_01_ef_core_modeling_drills.DTOs;

namespace task_01_ef_core_modeling_drills.Interface
{
    public interface IStudentService
    {
        StudentDto GetStudentWithTracks(int id);
        bool DeleteStudent(int id);
        List<StudentDto> GetStudents(bool includeDeleted = false);
        StudentDto Create(CreateStudentDto dto);
        StudentDto Update(int id, UpdateStudentDto studentDto);
        Task<PaginationResult<StudentListItemDto>> GetAll(int pageNumber, int pageSize);

    }
}
