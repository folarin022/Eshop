using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Migrations
{
    /// <inheritdoc />
    public partial class create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
