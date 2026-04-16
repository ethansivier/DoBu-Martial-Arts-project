using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DobuMartial_project.Data.Migrations
{
    /// <inheritdoc />
    public partial class Instructors3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Instructors",
                keyColumn: "InstructorID",
                keyValue: 1,
                column: "Image",
                value: "assets/img/mauricio.jpg");

            migrationBuilder.UpdateData(
                table: "Instructors",
                keyColumn: "InstructorID",
                keyValue: 2,
                column: "Image",
                value: "assets/img/sarah.jpg");

            migrationBuilder.UpdateData(
                table: "Instructors",
                keyColumn: "InstructorID",
                keyValue: 3,
                column: "Image",
                value: "assets/img/guy.jpg");

            migrationBuilder.UpdateData(
                table: "Instructors",
                keyColumn: "InstructorID",
                keyValue: 4,
                column: "Image",
                value: "assets/img/morris.jpg");

            migrationBuilder.UpdateData(
                table: "Instructors",
                keyColumn: "InstructorID",
                keyValue: 5,
                column: "Image",
                value: "assets/img/traci.jpg");

            migrationBuilder.UpdateData(
                table: "Instructors",
                keyColumn: "InstructorID",
                keyValue: 6,
                column: "Image",
                value: "assets/img/harpeet.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Instructors",
                keyColumn: "InstructorID",
                keyValue: 1,
                column: "Image",
                value: "assets/image/mauricio.jpg");

            migrationBuilder.UpdateData(
                table: "Instructors",
                keyColumn: "InstructorID",
                keyValue: 2,
                column: "Image",
                value: "assets/image/sarah.jpg");

            migrationBuilder.UpdateData(
                table: "Instructors",
                keyColumn: "InstructorID",
                keyValue: 3,
                column: "Image",
                value: "assets/image/guy.jpg");

            migrationBuilder.UpdateData(
                table: "Instructors",
                keyColumn: "InstructorID",
                keyValue: 4,
                column: "Image",
                value: "assets/image/morris.jpg");

            migrationBuilder.UpdateData(
                table: "Instructors",
                keyColumn: "InstructorID",
                keyValue: 5,
                column: "Image",
                value: "assets/image/traci.jpg");

            migrationBuilder.UpdateData(
                table: "Instructors",
                keyColumn: "InstructorID",
                keyValue: 6,
                column: "Image",
                value: "assets/image/harpeet.jpg");
        }
    }
}
