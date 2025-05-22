using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceInviteadAtwithStatusinOrganizationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUsers_Users_CognitoUserId",
                table: "OrganizationUsers");

            migrationBuilder.DropColumn(
                name: "InvitationAcceptedAt",
                table: "OrganizationUsers");

            migrationBuilder.RenameColumn(
                name: "CognitoUserId",
                table: "OrganizationUsers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationUsers_CognitoUserId",
                table: "OrganizationUsers",
                newName: "IX_OrganizationUsers_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OrganizationUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUsers_Users_UserId",
                table: "OrganizationUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Secrets_Organizations_CreatedBy",
                table: "Secrets",
                column: "CreatedBy",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUsers_Users_UserId",
                table: "OrganizationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Secrets_Organizations_CreatedBy",
                table: "Secrets");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrganizationUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OrganizationUsers",
                newName: "CognitoUserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationUsers_UserId",
                table: "OrganizationUsers",
                newName: "IX_OrganizationUsers_CognitoUserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "InvitationAcceptedAt",
                table: "OrganizationUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUsers_Users_CognitoUserId",
                table: "OrganizationUsers",
                column: "CognitoUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
