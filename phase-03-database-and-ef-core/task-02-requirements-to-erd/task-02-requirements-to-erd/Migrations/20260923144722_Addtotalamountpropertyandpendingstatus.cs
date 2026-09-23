using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace task_02_requirements_to_erd.Migrations
{
    /// <inheritdoc />
    public partial class Addtotalamountpropertyandpendingstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Payments");
        }
    }
}
