using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizationUserToCreatoruserrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUsers_CreatedBy",
                table: "OrganizationUsers",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUsers_Users_CreatedBy",
                table: "OrganizationUsers",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUsers_Users_CreatedBy",
                table: "OrganizationUsers");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationUsers_CreatedBy",
                table: "OrganizationUsers");
        }
    }
}
