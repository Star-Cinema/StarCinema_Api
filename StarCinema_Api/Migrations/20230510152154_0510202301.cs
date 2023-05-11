using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarCinema_Api.Migrations
{
    /// <inheritdoc />
    public partial class _0510202301 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "Films",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Images");

            migrationBuilder.AlterColumn<string>(
                name: "IsDelete",
                table: "Films",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
