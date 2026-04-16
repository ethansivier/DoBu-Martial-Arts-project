using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DobuMartial_project.Data.Migrations
{
    /// <inheritdoc />
    public partial class Instructors2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Instructors",
                keyColumn: "InstructorID",
                keyValue: 1,
                column: "Description",
                value: "Gym Owner\nHead Martial arts coach");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Instructors",
                keyColumn: "InstructorID",
                keyValue: 1,
                column: "Description",
                value: "Gym Owner<br>Head Martial arts coach");
        }
    }
}
