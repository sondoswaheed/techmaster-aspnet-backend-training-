using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using task_02_requirements_to_erd.Common.Responses;
using task_02_requirements_to_erd.Data;
using task_02_requirements_to_erd.Interface;
using task_02_requirements_to_erd.Middleware;
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

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })

            .AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey))
    };

    options.Events = new JwtBearerEvents
    {
        // Missing, invalid, or expired token
        OnChallenge = async context =>
        {
            context.HandleResponse();

            context.Response.StatusCode =
                StatusCodes.Status401Unauthorized;

            context.Response.ContentType = "application/json";

            var response = ApiResponse<object>.ErrorResponse(
                "Authentication is required or the access token is invalid."
            );

            await context.Response.WriteAsJsonAsync(response);
        },

        // Authenticated user does not have permission
        OnForbidden = async context =>
        {
            context.Response.StatusCode =
                StatusCodes.Status403Forbidden;

            context.Response.ContentType = "application/json";

            var response = ApiResponse<object>.ErrorResponse(
                "You do not have permission to perform this action."
            );

            await context.Response.WriteAsJsonAsync(response);
        }
    };
});


            builder.Services.AddAuthorization();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.UseInlineDefinitionsForEnums();

                c.AddSecurityDefinition("Bearer",
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
                                Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                            },
                            Array.Empty<string>()
                        }
                    });
            });


            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(x => x.Value?.Errors.Count > 0)
                        .SelectMany(x => x.Value!.Errors)
                        .Select(x => string.IsNullOrWhiteSpace(x.ErrorMessage)
                            ? "Invalid request value.": x.ErrorMessage)
                        .ToList();

                    var response = ApiResponse<object>.ErrorResponse( "Validation failed.", errors
                    );

                    return new BadRequestObjectResult(response);
                };
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

            //ActivityLog service
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ActivityLogService>();

            var app = builder.Build();

            app.UseMiddleware<GlobalExceptionMiddleware>();

            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                await IdentitySeeder.SeedAsync(scope.ServiceProvider);
            }

            app.Run();

        }
    }
}