using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizationIdinsecrets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Secrets_Organizations_CreatedBy",
                table: "Secrets");

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "Secrets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "OrganizationUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Secrets_OrganizationId",
                table: "Secrets",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Secrets_Organizations_OrganizationId",
                table: "Secrets",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Secrets_Organizations_OrganizationId",
                table: "Secrets");

            migrationBuilder.DropIndex(
                name: "IX_Secrets_OrganizationId",
                table: "Secrets");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Secrets");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "OrganizationUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Secrets_Organizations_CreatedBy",
                table: "Secrets",
                column: "CreatedBy",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
