using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace task_01_ef_core_modeling_drills.Migrations
{
    /// <inheritdoc />
    public partial class AddEnrollments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StudentProfiles",
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

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FinalGrade = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TrainingTrackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_TrainingTracks_TrainingTrackId",
                        column: x => x.TrainingTrackId,
                        principalTable: "TrainingTracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_TrainingTrackId",
                table: "Enrollments",
                column: "TrainingTrackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

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
        }
    }
}
