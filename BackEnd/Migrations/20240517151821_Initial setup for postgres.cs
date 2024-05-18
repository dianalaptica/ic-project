using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Initialsetupforpostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", unicode: false, nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Country",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", unicode: false, nullable: false),
                    LastName = table.Column<string>(type: "text", unicode: false, nullable: false),
                    Gender = table.Column<string>(type: "text", unicode: false, nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", unicode: false, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", unicode: false, nullable: false),
                    TokenCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TokenExpires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "bytea", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppliedForGuide",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    IdentityCard = table.Column<byte[]>(type: "bytea", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedForGuide", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_AppliedForGuide_City",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GuideId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", unicode: false, nullable: false),
                    Description = table.Column<string>(type: "text", unicode: false, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MaxTourists = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "text", unicode: false, nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trip_AppliedForGuide",
                        column: x => x.GuideId,
                        principalTable: "AppliedForGuide",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Trip_City",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TripNotification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TripId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", unicode: false, nullable: false),
                    Message = table.Column<string>(type: "text", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripNotification_Trip",
                        column: x => x.TripId,
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTrip",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrip", x => new { x.TripId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserTrip_Trip",
                        column: x => x.TripId,
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTrip_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserNotification",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotification", x => new { x.NotificationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserNotification_TripNotification",
                        column: x => x.NotificationId,
                        principalTable: "TripNotification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserNotification_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppliedForGuide_CityId",
                table: "AppliedForGuide",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryId",
                table: "City",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_CityId",
                table: "Trip",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_GuideId",
                table: "Trip",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_TripNotification_TripId",
                table: "TripNotification",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotification_UserId",
                table: "UserNotification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrip_UserId",
                table: "UserTrip",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserNotification");

            migrationBuilder.DropTable(
                name: "UserTrip");

            migrationBuilder.DropTable(
                name: "TripNotification");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Trip");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "AppliedForGuide");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
