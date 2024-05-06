using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class DeleteonCascadefortripandnotificationrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripNotification_Trip",
                table: "TripNotification");

            migrationBuilder.AddForeignKey(
                name: "FK_TripNotification_Trip",
                table: "TripNotification",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripNotification_Trip",
                table: "TripNotification");

            migrationBuilder.AddForeignKey(
                name: "FK_TripNotification_Trip",
                table: "TripNotification",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id");
        }
    }
}
