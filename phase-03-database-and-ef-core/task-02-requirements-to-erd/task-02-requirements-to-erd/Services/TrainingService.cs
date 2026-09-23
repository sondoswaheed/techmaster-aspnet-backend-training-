using Azure.Core;
using Microsoft.EntityFrameworkCore;
using task_02_requirements_to_erd.Data;
using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Interface;
using task_02_requirements_to_erd.Models;
using task_02_requirements_to_erd.Models.Enums;

namespace task_02_requirements_to_erd.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly AppDbContext _context;
        public TrainingService(AppDbContext context)
        {
            _context = context;
        }
        public TrainingTrackResponse Create(CreateTrackDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new InvalidOperationException("Title is required.");

            var instructorExists = _context.Instructors.Any(i => i.InstructorId == dto.InstructorId);

            if (!instructorExists)
                throw new InvalidOperationException("Instructor not found.");

            if (dto.Capacity <= 0)
                throw new InvalidOperationException( "Capacity must be greater than zero.");

            if(dto.StartDate>= dto.EndDate)
            {
                throw new InvalidOperationException("Start date must be before the end date ");
            }

            var track = new TrainingTrack
            {
                Status= dto.Status,
                StartDate= dto.StartDate,
                EndDate= dto.EndDate,
                Capacity= dto.Capacity,
                Code= dto.Code,
                CreatedAt= DateTime.UtcNow,
                Description= dto.Description,
                IsDeleted=false,
                Level= dto.Level,
                Title= dto.Title,
                InstructorId=dto.InstructorId
            };

            _context.TrainingTracks.Add(track);
            _context.SaveChanges();

            return MapToResponse(track);
        }

        public bool Delete(int id)
        {
            var track = _context.TrainingTracks.FirstOrDefault(s=>s.TrainingTrackId==id);

            if (track == null)
                return false;

            var hasActiveEnrollments = _context.Enrollments.Any(e =>
            e.TrainingTrackId == id &&
            (e.Status == EnrollmentStatus.Active || e.Status == EnrollmentStatus.Completed));

            if (hasActiveEnrollments)
                throw new InvalidOperationException( "Cannot delete track because it has active enrollments.");

            track.IsDeleted= true;

            _context.SaveChanges();

            return true;
        }

        public TrainingTrackResponse Details(int id)
        {
            var track = _context.TrainingTracks.Include(t => t.Instructor)
                .Include(t => t.Enrollments)
                .FirstOrDefault(t => t.TrainingTrackId == id);

            if (track == null)
                return null;

           return MapToResponseDetails(track);

        }

        public List<TrainingTrackResponse> GetAll(string? keyword, int? level, EnrollmentStatus? status, int? instructorId)
        {
            var query = _context.TrainingTracks.Where(k=>!k.IsDeleted);

            if (keyword != null)
            {
                query = query.Where(d => d.Title.Contains(keyword));
            }
            if (level.HasValue)
            {
                query=query.Where(d=>d.Level.Equals(level));
            }
            if(status.HasValue)
            {
                query=query.Where(d=>d.Status.Equals(status));
            }
            if (instructorId.HasValue)
            {
                query = query.Where(d => d.InstructorId ==instructorId);
            }


            return query.Select(MapToResponse).ToList();
        }

        public TrainingTrackResponse Update(int id, UpdateTrackDto dto)
        {
            var track = _context.TrainingTracks.FirstOrDefault(s => s.TrainingTrackId == id);

            if (track == null)
                return null;

            if (dto.StartDate >= dto.EndDate)
            {
                throw new InvalidOperationException("Start date must be before the end date ");
            }

            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new InvalidOperationException("Title is required.");

            var instructorExists = _context.Instructors.Any(i => i.InstructorId == dto.InstructorId);

            if (!instructorExists)
                throw new InvalidOperationException("Instructor not found.");

            if (dto.Capacity <= 0)
                throw new InvalidOperationException( "Capacity must be greater than zero.");

            track.Title=dto.Title;
            track.Status=dto.Status;
            track.StartDate=dto.StartDate;
            track.EndDate=dto.EndDate;
            track.Capacity=dto.Capacity;
            track.Code=dto.Code;
            track.Description=dto.Description;
            track.Level = dto.Level;

            _context.SaveChanges();

            return MapToResponse(track);

        }


        private TrainingTrackResponse MapToResponse(TrainingTrack track)
        {
            return new TrainingTrackResponse
            {
                TrainingTrackId= track.TrainingTrackId,
                StartDate = track.StartDate,
                EndDate = track.EndDate,
                Capacity = track.Capacity,
                Code = track.Code,
                Status = track.Status,
                Title = track.Title,
                Level = track.Level,
                CreatedAt= track.CreatedAt,
                IsDeleted=track.IsDeleted,
                Description= track.Description,
                InstructorId= track.InstructorId,
            };
        }
        private TrainingTrackResponse MapToResponseDetails(TrainingTrack track)
        {
            var enrolledCount = track.Enrollments.Count;

            return new TrainingTrackResponse
            {
                TrainingTrackId = track.TrainingTrackId,
                StartDate = track.StartDate,
                EndDate = track.EndDate,
                Capacity = track.Capacity,
                Code = track.Code,
                Status = track.Status,
                Title = track.Title,
                Level = track.Level,
                CreatedAt = track.CreatedAt,
                IsDeleted = track.IsDeleted,
                Description = track.Description,
                InstructorId = track.InstructorId,
                InstructorName = track.Instructor?.FullName,
                EnrolledCount = enrolledCount,
                RemainingCapacity = track.Capacity - enrolledCount
            };
        }
    }
}
