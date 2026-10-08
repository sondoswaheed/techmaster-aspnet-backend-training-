using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using task_02_requirements_to_erd.Data;
using task_02_requirements_to_erd.Interface;
using task_02_requirements_to_erd.Models;
using task_02_requirements_to_erd.Services;

namespace task_02_requirements_to_erd
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new JsonStringEnumConverter());
                });

            // Database
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString(
                        "DefaultConnection")));

            // Identity
            builder.Services
                .AddIdentityCore<ApplicationUser>(options =>
                {
                    options.User.RequireUniqueEmail = true;

                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            // JWT Authentication
            var jwtKey = builder.Configuration["Jwt:Key"];

            if (string.IsNullOrWhiteSpace(jwtKey))
            {
                throw new InvalidOperationException(
                    "JWT Key is missing from configuration.");
            }

            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;

                    options.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer =
                                builder.Configuration["Jwt:Issuer"],

                            ValidAudience =
                                builder.Configuration["Jwt:Audience"],

                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(jwtKey))
                        };
                });

            builder.Services.AddAuthorization();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.UseInlineDefinitionsForEnums();

                c.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description =
                            "Enter JWT token like: Bearer {your token}"
                    });

                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference =
                                    new OpenApiReference
                                    {
                                        Type =
                                            ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                            },
                            Array.Empty<string>()
                        }
                    });
            });

            // Existing services
            builder.Services.AddScoped<IInstructorService, InstructorService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
            builder.Services.AddScoped<ITrainingService, TrainingService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IReportService, ReportService>();

            // Authentication service
            builder.Services.AddScoped<IAuthService, AuthService>();

            var app = builder.Build();

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            // IMPORTANT ORDER
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            // Seed roles
            using (var scope = app.Services.CreateScope())
            {
                var roleManager =
                    scope.ServiceProvider
                        .GetRequiredService<RoleManager<IdentityRole>>();

                string[] roles =
                {
                    "Admin",
                    "Instructor",
                    "Student"
                };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(
                            new IdentityRole(role));
                    }
                }
            }

            app.Run();
        }
    }
}