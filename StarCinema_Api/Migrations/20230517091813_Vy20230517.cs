using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarCinema_Api.Migrations
{
    /// <inheritdoc />
    public partial class Vy20230517 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTrash",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTrash",
                table: "Categories");
        }
    }
}
