
using Microsoft.EntityFrameworkCore;
using task_01_ef_core_modeling_drills.Data;
using task_01_ef_core_modeling_drills.Interface;
using task_01_ef_core_modeling_drills.Services;

namespace task_01_ef_core_modeling_drills
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IInstructorService,InstructorService>();
            builder.Services.AddScoped<IStudentService,StudentService>();
            builder.Services.AddScoped<ITrackService, TrackService>();
            builder.Services.AddScoped<IEnrollmentService,EnrollmentService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
