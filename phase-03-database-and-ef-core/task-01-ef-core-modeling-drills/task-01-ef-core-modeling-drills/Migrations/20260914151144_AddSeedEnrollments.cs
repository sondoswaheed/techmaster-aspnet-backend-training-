using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace task_01_ef_core_modeling_drills.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedEnrollments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "Email", "Name", "PhoneNumber" },
                values: new object[] { 1, "mohamed@gmail.com", "Mohammed ahmed", "01087655678" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "IsActive" },
                values: new object[] { 1, new DateTime(2026, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sonds@gmail.com", "Sondos waheed", true });

            migrationBuilder.InsertData(
                table: "StudentProfiles",
                columns: new[] { "Id", "Address", "DateOfBirth", "EmergencyPhone", "NationalId", "StudentId" },
                values: new object[] { 1, "21 sreet", new DateOnly(2005, 4, 18), "01063500543", "98464748493033", 1 });

            migrationBuilder.InsertData(
                table: "TrainingTracks",
                columns: new[] { "Id", "InstructorId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Frontend" },
                    { 2, 1, "Backend" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "EnrollmentDate", "FinalGrade", "Status", "StudentId", "TrainingTrackId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, 1, 1 },
                    { 2, new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 90m, 1, 1, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TrainingTracks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TrainingTracks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
