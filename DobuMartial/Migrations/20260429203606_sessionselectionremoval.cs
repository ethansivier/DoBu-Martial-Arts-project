using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DobuMartial_project.Migrations
{
    /// <inheritdoc />
    public partial class sessionselectionremoval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    InstructorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.InstructorID);
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    MembershipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MartialArts = table.Column<int>(type: "int", nullable: false),
                    Sessions = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    IsKids = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.MembershipId);
                });

            migrationBuilder.CreateTable(
                name: "WeekDays",
                columns: table => new
                {
                    DayID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekDays", x => x.DayID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MembershipId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Memberships_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Memberships",
                        principalColumn: "MembershipId");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    ClassID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
                    IsKids = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.ClassID);
                    table.ForeignKey(
                        name: "FK_Class_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStart = table.Column<TimeOnly>(type: "time", nullable: false),
                    TimeEnd = table.Column<TimeOnly>(type: "time", nullable: false),
                    DayID = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_Sessions_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassID");
                    table.ForeignKey(
                        name: "FK_Sessions_WeekDays_DayID",
                        column: x => x.DayID,
                        principalTable: "WeekDays",
                        principalColumn: "DayID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSession",
                columns: table => new
                {
                    SessionsSessionId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSession", x => new { x.SessionsSessionId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserSession_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSession_Sessions_SessionsSessionId",
                        column: x => x.SessionsSessionId,
                        principalTable: "Sessions",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Class",
                columns: new[] { "ClassID", "IsKids", "IsPrivate", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, false, false, "Jiu-Jitsu", null },
                    { 2, false, false, "Muay-Thai", null },
                    { 3, false, true, "Private Tuition", null },
                    { 4, false, false, "Open Mat/ Personal Practise", null },
                    { 5, true, false, "Kids Jiu-Jitsu", null },
                    { 6, false, false, "Karate", null },
                    { 7, false, false, "Judo", null },
                    { 8, true, false, "Kids Judo", null },
                    { 9, true, false, "Kids Jiu-jitsu", null },
                    { 10, true, false, "Kids karate", null },
                    { 11, false, false, "", null }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "InstructorID", "Description", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "Gym Owner\nHead Martial arts coach", "assets/img/mauricio.jpg", "Mauricio Gomez" },
                    { 2, "Assistant Martial Arts Coach", "assets/img/sarah.jpg", "Sarah Nova" },
                    { 3, "Assistant Martial Arts Coach", "assets/img/guy.jpg", "Guy Victory" },
                    { 4, "Assistant Martial Arts Coach", "assets/img/morris.jpg", "Morris Davis" },
                    { 5, "Fitness Coach", "assets/img/traci.jpg", "Traci Santiago" },
                    { 6, "Fitness Coach", "assets/img/harpeet.jpg", "Harpeet Kaur" }
                });

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "MembershipId", "IsKids", "MartialArts", "Name", "Price", "Sessions" },
                values: new object[,]
                {
                    { 1, false, 1, "Basic", 25.0, 2 },
                    { 2, false, 1, "Intermediate", 35.0, 3 },
                    { 3, false, 2, "Advanced", 45.0, 5 },
                    { 4, false, 100, "Elite", 60.0, 100 },
                    { 5, true, 100, "Junior Membership", 25.0, 100 }
                });

            migrationBuilder.InsertData(
                table: "WeekDays",
                columns: new[] { "DayID", "Name" },
                values: new object[,]
                {
                    { 1, "Monday" },
                    { 2, "Tuesday" },
                    { 3, "Wednesday" },
                    { 4, "Thursday" },
                    { 5, "Friday" },
                    { 6, "Saturday" },
                    { 7, "Sunday" }
                });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "SessionId", "ClassId", "DayID", "TimeEnd", "TimeStart" },
                values: new object[,]
                {
                    { 1, 1, 1, new TimeOnly(7, 30, 0), new TimeOnly(6, 30, 0) },
                    { 2, 2, 1, new TimeOnly(10, 0, 0), new TimeOnly(8, 0, 0) },
                    { 3, 3, 1, new TimeOnly(12, 0, 0), new TimeOnly(10, 30, 0) },
                    { 4, 4, 1, new TimeOnly(14, 30, 0), new TimeOnly(13, 0, 0) },
                    { 5, 5, 1, new TimeOnly(17, 0, 0), new TimeOnly(15, 0, 0) },
                    { 6, 6, 1, new TimeOnly(19, 0, 0), new TimeOnly(17, 30, 0) },
                    { 7, 7, 1, new TimeOnly(21, 0, 0), new TimeOnly(19, 0, 0) },
                    { 8, 6, 2, new TimeOnly(7, 30, 0), new TimeOnly(6, 30, 0) },
                    { 9, 3, 2, new TimeOnly(10, 0, 0), new TimeOnly(8, 0, 0) },
                    { 10, 3, 2, new TimeOnly(12, 0, 0), new TimeOnly(10, 30, 0) },
                    { 11, 4, 2, new TimeOnly(14, 30, 0), new TimeOnly(13, 0, 0) },
                    { 12, 8, 2, new TimeOnly(17, 0, 0), new TimeOnly(15, 0, 0) },
                    { 13, 2, 2, new TimeOnly(19, 0, 0), new TimeOnly(17, 30, 0) },
                    { 14, 7, 2, new TimeOnly(21, 0, 0), new TimeOnly(19, 0, 0) },
                    { 15, 7, 3, new TimeOnly(7, 30, 0), new TimeOnly(6, 30, 0) },
                    { 16, 3, 3, new TimeOnly(10, 0, 0), new TimeOnly(8, 0, 0) },
                    { 17, 3, 3, new TimeOnly(12, 0, 0), new TimeOnly(10, 30, 0) },
                    { 18, 4, 3, new TimeOnly(14, 30, 0), new TimeOnly(13, 0, 0) },
                    { 19, 10, 3, new TimeOnly(17, 0, 0), new TimeOnly(15, 0, 0) },
                    { 20, 7, 3, new TimeOnly(19, 0, 0), new TimeOnly(17, 30, 0) },
                    { 21, 1, 3, new TimeOnly(21, 0, 0), new TimeOnly(19, 0, 0) },
                    { 22, 1, 4, new TimeOnly(7, 30, 0), new TimeOnly(6, 30, 0) },
                    { 23, 3, 4, new TimeOnly(10, 0, 0), new TimeOnly(8, 0, 0) },
                    { 24, 3, 4, new TimeOnly(12, 0, 0), new TimeOnly(10, 30, 0) },
                    { 25, 4, 4, new TimeOnly(14, 30, 0), new TimeOnly(13, 0, 0) },
                    { 26, 9, 4, new TimeOnly(17, 0, 0), new TimeOnly(15, 0, 0) },
                    { 27, 1, 4, new TimeOnly(19, 0, 0), new TimeOnly(17, 30, 0) },
                    { 28, 6, 4, new TimeOnly(21, 0, 0), new TimeOnly(19, 0, 0) },
                    { 29, 2, 5, new TimeOnly(7, 30, 0), new TimeOnly(6, 30, 0) },
                    { 30, 1, 5, new TimeOnly(10, 0, 0), new TimeOnly(8, 0, 0) },
                    { 31, 3, 5, new TimeOnly(12, 0, 0), new TimeOnly(10, 30, 0) },
                    { 32, 4, 5, new TimeOnly(14, 30, 0), new TimeOnly(13, 0, 0) },
                    { 33, 8, 5, new TimeOnly(17, 0, 0), new TimeOnly(15, 0, 0) },
                    { 34, 2, 5, new TimeOnly(19, 0, 0), new TimeOnly(17, 30, 0) },
                    { 35, 3, 5, new TimeOnly(21, 0, 0), new TimeOnly(19, 0, 0) },
                    { 36, 11, 6, new TimeOnly(7, 30, 0), new TimeOnly(6, 30, 0) },
                    { 37, 3, 6, new TimeOnly(10, 0, 0), new TimeOnly(8, 0, 0) },
                    { 38, 7, 6, new TimeOnly(12, 0, 0), new TimeOnly(10, 30, 0) },
                    { 39, 6, 6, new TimeOnly(14, 30, 0), new TimeOnly(13, 0, 0) },
                    { 40, 2, 6, new TimeOnly(17, 0, 0), new TimeOnly(15, 0, 0) },
                    { 41, 11, 6, new TimeOnly(19, 0, 0), new TimeOnly(17, 30, 0) },
                    { 42, 11, 6, new TimeOnly(21, 0, 0), new TimeOnly(19, 0, 0) },
                    { 43, 11, 7, new TimeOnly(7, 30, 0), new TimeOnly(6, 30, 0) },
                    { 44, 3, 7, new TimeOnly(10, 0, 0), new TimeOnly(8, 0, 0) },
                    { 45, 6, 7, new TimeOnly(12, 0, 0), new TimeOnly(10, 30, 0) },
                    { 46, 7, 7, new TimeOnly(14, 30, 0), new TimeOnly(13, 0, 0) },
                    { 47, 1, 7, new TimeOnly(17, 0, 0), new TimeOnly(15, 0, 0) },
                    { 48, 11, 7, new TimeOnly(19, 0, 0), new TimeOnly(17, 30, 0) },
                    { 49, 11, 7, new TimeOnly(21, 0, 0), new TimeOnly(19, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MembershipId",
                table: "AspNetUsers",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Class_UserId",
                table: "Class",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ClassId",
                table: "Sessions",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_DayID",
                table: "Sessions",
                column: "DayID");

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_UsersId",
                table: "UserSession",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "UserSession");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "WeekDays");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Memberships");
        }
    }
}
