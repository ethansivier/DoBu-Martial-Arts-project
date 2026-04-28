using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DobuMartial_project.Migrations
{
    /// <inheritdoc />
    public partial class juniorMembership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "MembershipId", "IsKids", "MartialArts", "Name", "Price", "Sessions" },
                values: new object[] { 5, false, 100, "Junior Membership", 25.0, 100 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "MembershipId",
                keyValue: 5);
        }
    }
}
