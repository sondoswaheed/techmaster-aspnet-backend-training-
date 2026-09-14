using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace task_01_ef_core_modeling_drills.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "Email", "Name", "PhoneNumber" },
                values: new object[] { 2, "sara.hassan@gmail.com", "Sara Hassan", "01123456789" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "IsActive" },
                values: new object[,]
                {
                    { 2, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "omar.ali@gmail.com", "Omar Ali", true },
                    { 3, new DateTime(2026, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "nour.m@gmail.com", "Nour Mahmoud", true },
                    { 4, new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "youssef.i@gmail.com", "Youssef Ibrahim", true },
                    { 5, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mariam.k@gmail.com", "Mariam Khaled", false }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "EnrollmentDate", "FinalGrade", "Status", "StudentId", "TrainingTrackId" },
                values: new object[,]
                {
                    { 3, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, 2, 2 },
                    { 5, new DateTime(2026, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "StudentProfiles",
                columns: new[] { "Id", "Address", "DateOfBirth", "EmergencyPhone", "NationalId", "StudentId" },
                values: new object[,]
                {
                    { 2, "Nasr City, Cairo", new DateOnly(1999, 1, 1), "01011112222", "29901011234567", 2 },
                    { 3, "Maadi, Cairo", new DateOnly(2001, 5, 5), "01033334444", "30105051234568", 3 },
                    { 4, "Giza", new DateOnly(1998, 11, 11), "01055556666", "29811111234569", 4 },
                    { 5, "Alexandria", new DateOnly(2002, 3, 3), "01077778888", "30203031234570", 5 }
                });

            migrationBuilder.InsertData(
                table: "TrainingTracks",
                columns: new[] { "Id", "InstructorId", "Title" },
                values: new object[] { 3, 2, "UI/UX Design" });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "EnrollmentDate", "FinalGrade", "Status", "StudentId", "TrainingTrackId" },
                values: new object[] { 4, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 95m, 1, 3, 3 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TrainingTracks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
