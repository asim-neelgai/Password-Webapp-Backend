using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOrganizationrelationfromSharedsecret : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedSecret_Organizations_SharedTo",
                table: "SharedSecret");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedSecret_Secrets_SecretId",
                table: "SharedSecret");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedSecret_Users_SharedTo",
                table: "SharedSecret");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedSecret",
                table: "SharedSecret");

            migrationBuilder.RenameTable(
                name: "SharedSecret",
                newName: "SharedSecrets");

            migrationBuilder.RenameIndex(
                name: "IX_SharedSecret_SharedTo",
                table: "SharedSecrets",
                newName: "IX_SharedSecrets_SharedTo");

            migrationBuilder.RenameIndex(
                name: "IX_SharedSecret_SecretId",
                table: "SharedSecrets",
                newName: "IX_SharedSecrets_SecretId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedSecrets",
                table: "SharedSecrets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedSecrets_Secrets_SecretId",
                table: "SharedSecrets",
                column: "SecretId",
                principalTable: "Secrets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedSecrets_Users_SharedTo",
                table: "SharedSecrets",
                column: "SharedTo",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedSecrets_Secrets_SecretId",
                table: "SharedSecrets");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedSecrets_Users_SharedTo",
                table: "SharedSecrets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedSecrets",
                table: "SharedSecrets");

            migrationBuilder.RenameTable(
                name: "SharedSecrets",
                newName: "SharedSecret");

            migrationBuilder.RenameIndex(
                name: "IX_SharedSecrets_SharedTo",
                table: "SharedSecret",
                newName: "IX_SharedSecret_SharedTo");

            migrationBuilder.RenameIndex(
                name: "IX_SharedSecrets_SecretId",
                table: "SharedSecret",
                newName: "IX_SharedSecret_SecretId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedSecret",
                table: "SharedSecret",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedSecret_Organizations_SharedTo",
                table: "SharedSecret",
                column: "SharedTo",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedSecret_Secrets_SecretId",
                table: "SharedSecret",
                column: "SecretId",
                principalTable: "Secrets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedSecret_Users_SharedTo",
                table: "SharedSecret",
                column: "SharedTo",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
