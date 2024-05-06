using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class DeleteonCascadefornotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotification_TripNotification",
                table: "UserNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotification_User",
                table: "UserNotification");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotification_TripNotification",
                table: "UserNotification",
                column: "NotificationId",
                principalTable: "TripNotification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotification_User",
                table: "UserNotification",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotification_TripNotification",
                table: "UserNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotification_User",
                table: "UserNotification");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotification_TripNotification",
                table: "UserNotification",
                column: "NotificationId",
                principalTable: "TripNotification",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotification_User",
                table: "UserNotification",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
