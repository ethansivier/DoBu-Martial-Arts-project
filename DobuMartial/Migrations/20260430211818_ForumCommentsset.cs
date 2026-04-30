using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DobuMartial_project.Migrations
{
    /// <inheritdoc />
    public partial class ForumCommentsset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumComment_AspNetUsers_UserId",
                table: "ForumComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumComment_ForumPosts_PostId",
                table: "ForumComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ForumComment",
                table: "ForumComment");

            migrationBuilder.RenameTable(
                name: "ForumComment",
                newName: "ForumComments");

            migrationBuilder.RenameIndex(
                name: "IX_ForumComment_UserId",
                table: "ForumComments",
                newName: "IX_ForumComments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ForumComment_PostId",
                table: "ForumComments",
                newName: "IX_ForumComments_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForumComments",
                table: "ForumComments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumComments_AspNetUsers_UserId",
                table: "ForumComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumComments_ForumPosts_PostId",
                table: "ForumComments",
                column: "PostId",
                principalTable: "ForumPosts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumComments_AspNetUsers_UserId",
                table: "ForumComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumComments_ForumPosts_PostId",
                table: "ForumComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ForumComments",
                table: "ForumComments");

            migrationBuilder.RenameTable(
                name: "ForumComments",
                newName: "ForumComment");

            migrationBuilder.RenameIndex(
                name: "IX_ForumComments_UserId",
                table: "ForumComment",
                newName: "IX_ForumComment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ForumComments_PostId",
                table: "ForumComment",
                newName: "IX_ForumComment_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForumComment",
                table: "ForumComment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumComment_AspNetUsers_UserId",
                table: "ForumComment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumComment_ForumPosts_PostId",
                table: "ForumComment",
                column: "PostId",
                principalTable: "ForumPosts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
