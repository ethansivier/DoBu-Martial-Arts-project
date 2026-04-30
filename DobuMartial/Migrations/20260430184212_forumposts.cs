using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DobuMartial_project.Migrations
{
    /// <inheritdoc />
    public partial class forumposts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumComment_ForumPost_PostId",
                table: "ForumComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumPost_AspNetUsers_OwnerId",
                table: "ForumPost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ForumPost",
                table: "ForumPost");

            migrationBuilder.RenameTable(
                name: "ForumPost",
                newName: "ForumPosts");

            migrationBuilder.RenameIndex(
                name: "IX_ForumPost_OwnerId",
                table: "ForumPosts",
                newName: "IX_ForumPosts_OwnerId");

            migrationBuilder.AddColumn<DateTime>(
                name: "PostDate",
                table: "ForumPosts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForumPosts",
                table: "ForumPosts",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumComment_ForumPosts_PostId",
                table: "ForumComment",
                column: "PostId",
                principalTable: "ForumPosts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPosts_AspNetUsers_OwnerId",
                table: "ForumPosts",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumComment_ForumPosts_PostId",
                table: "ForumComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumPosts_AspNetUsers_OwnerId",
                table: "ForumPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ForumPosts",
                table: "ForumPosts");

            migrationBuilder.DropColumn(
                name: "PostDate",
                table: "ForumPosts");

            migrationBuilder.RenameTable(
                name: "ForumPosts",
                newName: "ForumPost");

            migrationBuilder.RenameIndex(
                name: "IX_ForumPosts_OwnerId",
                table: "ForumPost",
                newName: "IX_ForumPost_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForumPost",
                table: "ForumPost",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumComment_ForumPost_PostId",
                table: "ForumComment",
                column: "PostId",
                principalTable: "ForumPost",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPost_AspNetUsers_OwnerId",
                table: "ForumPost",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
