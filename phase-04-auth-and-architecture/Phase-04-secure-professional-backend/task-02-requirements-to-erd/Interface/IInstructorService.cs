using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Models;

namespace task_02_requirements_to_erd.Interface
{
    public interface IInstructorService
    {
        Task<InstructorResponse> CreateAsync(CreateInstructorDto dto);
        InstructorResponse Update(int id , UpdateInstructorDto dto);
        InstructorResponse Details(int id);
        List<InstructorResponse> GetAll();

        InstructorTracksDto GetInstructorWithTracks(int id);
        Task<InstructorResponse> UpdateMyProfileAsync(string userId, UpdateMyInstructorProfileDto dto);

        Task<List<TrainingTrack>> GetMyTracksAsync(string userId);

        Task<List<StudentResponse>> GetTrackStudentsAsync( string userId,int trackId);

        Task<TrackProgressResponse> GetTrackProgressAsync( string userId, int trackId);

        Task<TrackSession> CreateSessionAsync( string userId, int trackId, CreateTrackSessionDto dto);

        Task<TrackSession> UpdateSessionAsync( string userId, int sessionId, UpdateTrackSessionDto dto);
    }
}
