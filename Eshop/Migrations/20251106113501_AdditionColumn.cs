using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Migrations
{
    /// <inheritdoc />
    public partial class AdditionColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Roles_rolesId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_rolesId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_rolesId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Categories_rolesId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "rolesId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "rolesId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "Auths",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auths", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auths");

            migrationBuilder.AddColumn<Guid>(
                name: "rolesId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "rolesId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_rolesId",
                table: "Users",
                column: "rolesId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_rolesId",
                table: "Categories",
                column: "rolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Roles_rolesId",
                table: "Categories",
                column: "rolesId",
                principalTable: "Roles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_rolesId",
                table: "Users",
                column: "rolesId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
