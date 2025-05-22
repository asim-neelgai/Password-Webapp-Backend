using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovePrimarykeycombinationofUserIDandOrganizationID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUsers_Users_UserId",
                table: "OrganizationUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationUsers",
                table: "OrganizationUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "OrganizationUsers",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationUsers",
                table: "OrganizationUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUsers_OrganizationId",
                table: "OrganizationUsers",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUsers_Users_UserId",
                table: "OrganizationUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUsers_Users_UserId",
                table: "OrganizationUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationUsers",
                table: "OrganizationUsers");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationUsers_OrganizationId",
                table: "OrganizationUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "OrganizationUsers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationUsers",
                table: "OrganizationUsers",
                columns: new[] { "OrganizationId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUsers_Users_UserId",
                table: "OrganizationUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
