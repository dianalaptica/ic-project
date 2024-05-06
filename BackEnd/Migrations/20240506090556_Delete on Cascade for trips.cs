using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class DeleteonCascadefortrips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTrip_Trip",
                table: "UserTrip");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTrip_User",
                table: "UserTrip");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTrip_Trip",
                table: "UserTrip",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTrip_User",
                table: "UserTrip",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTrip_Trip",
                table: "UserTrip");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTrip_User",
                table: "UserTrip");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTrip_Trip",
                table: "UserTrip",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTrip_User",
                table: "UserTrip",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
