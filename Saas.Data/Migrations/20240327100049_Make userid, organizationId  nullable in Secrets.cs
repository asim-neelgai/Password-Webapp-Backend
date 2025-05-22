using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeuseridorganizationIdnullableinSecrets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Secrets_Organizations_OrganizationId",
                table: "Secrets");

            migrationBuilder.DropForeignKey(
                name: "FK_Secrets_Users_CreatedBy",
                table: "Secrets");

            migrationBuilder.DropIndex(
                name: "IX_Secrets_CreatedBy",
                table: "Secrets");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                table: "Secrets",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Secrets",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Secrets_UserId",
                table: "Secrets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Secrets_Organizations_OrganizationId",
                table: "Secrets",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Secrets_Users_UserId",
                table: "Secrets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Secrets_Organizations_OrganizationId",
                table: "Secrets");

            migrationBuilder.DropForeignKey(
                name: "FK_Secrets_Users_UserId",
                table: "Secrets");

            migrationBuilder.DropIndex(
                name: "IX_Secrets_UserId",
                table: "Secrets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Secrets");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                table: "Secrets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Secrets_CreatedBy",
                table: "Secrets",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Secrets_Organizations_OrganizationId",
                table: "Secrets",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Secrets_Users_CreatedBy",
                table: "Secrets",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
