using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarCinema_Api.Migrations
{
    /// <inheritdoc />
    public partial class addService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingServiceServices");

            migrationBuilder.CreateIndex(
                name: "IX_BookingService_ServiceId",
                table: "BookingService",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingService_Services_ServiceId",
                table: "BookingService",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingService_Services_ServiceId",
                table: "BookingService");

            migrationBuilder.DropIndex(
                name: "IX_BookingService_ServiceId",
                table: "BookingService");

            migrationBuilder.CreateTable(
                name: "BookingServiceServices",
                columns: table => new
                {
                    BookingServicesId = table.Column<int>(type: "int", nullable: false),
                    ServicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingServiceServices", x => new { x.BookingServicesId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_BookingServiceServices_BookingService_BookingServicesId",
                        column: x => x.BookingServicesId,
                        principalTable: "BookingService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingServiceServices_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingServiceServices_ServicesId",
                table: "BookingServiceServices",
                column: "ServicesId");
        }
    }
}
