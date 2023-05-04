using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarCinema_Api.Migrations
{
    /// <inheritdoc />
    public partial class updateProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Films_Categories_categoryid",
                table: "Films");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Roles",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Roles",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "categoryid",
                table: "Films",
                newName: "Categoryid");

            migrationBuilder.RenameIndex(
                name: "IX_Films_categoryid",
                table: "Films",
                newName: "IX_Films_Categoryid");

            migrationBuilder.AddForeignKey(
                name: "FK_Films_Categories_Categoryid",
                table: "Films",
                column: "Categoryid",
                principalTable: "Categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Films_Categories_Categoryid",
                table: "Films");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Roles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Categoryid",
                table: "Films",
                newName: "categoryid");

            migrationBuilder.RenameIndex(
                name: "IX_Films_Categoryid",
                table: "Films",
                newName: "IX_Films_categoryid");

            migrationBuilder.AddForeignKey(
                name: "FK_Films_Categories_categoryid",
                table: "Films",
                column: "categoryid",
                principalTable: "Categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
