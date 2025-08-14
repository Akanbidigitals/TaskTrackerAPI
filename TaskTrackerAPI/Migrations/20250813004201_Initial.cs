using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    AssignToId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_AssignToId",
                        column: x => x.AssignToId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "PasswordHash", "Role", "UserName" },
                values: new object[,]
                {
                    { new Guid("148170c6-7910-44c2-91da-a24b72a0e269"), new DateTime(2025, 8, 13, 0, 41, 59, 968, DateTimeKind.Utc).AddTicks(2117), "$2a$11$bDLZvRCmYI39r6QSr3G.m.DHbRby/QWpSVtXS8T6L66DhPzR6BWTi", 0, "User7" },
                    { new Guid("1f21d873-568b-4468-89b2-7e43c860a46a"), new DateTime(2025, 8, 13, 0, 41, 59, 585, DateTimeKind.Utc).AddTicks(788), "$2a$11$tDcmaD.777fgENXslhwpAuQTELz7MYOsjbHZF6V42As1Wb0yeBTti", 0, "User5" },
                    { new Guid("524ce4f8-dc6a-4565-88c7-b8ee2cde8be9"), new DateTime(2025, 8, 13, 0, 42, 0, 558, DateTimeKind.Utc).AddTicks(9712), "$2a$11$V9Ep5GILQlok.MYJtwMROOM/9W95EMrKkMeWZFY9BXSbH9HpCqtZe", 1, "User10" },
                    { new Guid("5e91f86e-2708-46a1-a6f1-d810c6ed247e"), new DateTime(2025, 8, 13, 0, 42, 0, 167, DateTimeKind.Utc).AddTicks(8489), "$2a$11$OQek1tLuF/QiZy/PtJaE5u50e6MxIfhKSbfPblq7Tu5VAdY0Sk1pS", 1, "User8" },
                    { new Guid("7354465e-9813-451b-a339-dd1ca1c8f50f"), new DateTime(2025, 8, 13, 0, 41, 59, 391, DateTimeKind.Utc).AddTicks(6082), "$2a$11$rNIgG1xwAVLHocgYGnyR6exBx9g9emPImkNcTZFLuytZpNl5OEnjm", 1, "User4" },
                    { new Guid("86033173-51f7-4a31-911f-4afc1022a1d7"), new DateTime(2025, 8, 13, 0, 41, 59, 9, DateTimeKind.Utc).AddTicks(7955), "$2a$11$LxxvuHuNzt/VWYDrqIfnwu0nkqXl1iLfqmrlL7RL31Yfs0AbQy/py", 1, "User2" },
                    { new Guid("8cd1ed42-b707-4fd8-a050-a77283f6972b"), new DateTime(2025, 8, 13, 0, 41, 59, 203, DateTimeKind.Utc).AddTicks(8213), "$2a$11$fGKU5kYs44Xwk2sSmcwVn.hVYgI8Lnw/3iq1XxE67mYOhUbRfEIfu", 0, "User3" },
                    { new Guid("c1009d1c-47bd-40f5-85d9-a192cc3a6499"), new DateTime(2025, 8, 13, 0, 41, 58, 794, DateTimeKind.Utc).AddTicks(1303), "$2a$11$27.bjFvfqkNLGWFv.JvLketbzf6X32V2ArSgOwVLSZktJJJm4pE2q", 0, "User1" },
                    { new Guid("cb3c6c5c-a8a5-433f-bdbf-6998c620aafb"), new DateTime(2025, 8, 13, 0, 42, 0, 360, DateTimeKind.Utc).AddTicks(9855), "$2a$11$/4n.1IAAkRVGLdT3Hw6LauvyenzlJcASvaUC634Lln0yHdlOJ7wAe", 0, "User9" },
                    { new Guid("f9d711bd-3803-4adb-afd5-de7465a2b4c6"), new DateTime(2025, 8, 13, 0, 41, 59, 776, DateTimeKind.Utc).AddTicks(1949), "$2a$11$vfA3Kq/6YB8dxT/tEs87NOee0v70jGWAfz9RQ2DVtfQmagsWMoYBm", 1, "User6" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignToId",
                table: "Tasks",
                column: "AssignToId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
