using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DobuMartial_project.Migrations
{
    /// <inheritdoc />
    public partial class juniormembershipjunioriskidsnowoops : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "MembershipId",
                keyValue: 5,
                column: "IsKids",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "MembershipId",
                keyValue: 5,
                column: "IsKids",
                value: false);
        }
    }
}
