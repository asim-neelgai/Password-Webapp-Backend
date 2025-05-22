using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailinSharedSecret : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedSecrets_Users_SharedTo",
                table: "SharedSecrets");

            migrationBuilder.AlterColumn<Guid>(
                name: "SharedTo",
                table: "SharedSecrets",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "SharedToEmail",
                table: "SharedSecrets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedSecrets_Users_SharedTo",
                table: "SharedSecrets",
                column: "SharedTo",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedSecrets_Users_SharedTo",
                table: "SharedSecrets");

            migrationBuilder.DropColumn(
                name: "SharedToEmail",
                table: "SharedSecrets");

            migrationBuilder.AlterColumn<Guid>(
                name: "SharedTo",
                table: "SharedSecrets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedSecrets_Users_SharedTo",
                table: "SharedSecrets",
                column: "SharedTo",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
