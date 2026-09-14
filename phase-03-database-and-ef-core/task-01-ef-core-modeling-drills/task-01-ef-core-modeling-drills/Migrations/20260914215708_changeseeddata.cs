using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace task_01_ef_core_modeling_drills.Migrations
{
    /// <inheritdoc />
    public partial class changeseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsDeleted",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsDeleted",
                value: false);
        }
    }
}
