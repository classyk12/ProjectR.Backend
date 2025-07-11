using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectR.Backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    PhoneCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    AccountType = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    RegistrationType = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    RecordStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "businesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PhoneCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    Industry = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    About = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ServiceType = table.Column<int>(type: "integer", nullable: true),
                    Location = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Longitude = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Latitude = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    RecordStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_businesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_businesses_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessAvailabilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BusinessId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ValidFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    ValidTo = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    RecordStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessAvailabilities_businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessAvailabilitySlots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BusinessAvailabilityId = table.Column<Guid>(type: "uuid", nullable: false),
                    BusinessAvailabilityId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    Breaks = table.Column<List<TimeOnly>>(type: "time without time zone[]", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    RecordStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessAvailabilitySlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessAvailabilitySlots_BusinessAvailabilities_BusinessAv~",
                        column: x => x.BusinessAvailabilityId,
                        principalTable: "BusinessAvailabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessAvailabilitySlots_BusinessAvailabilities_BusinessA~1",
                        column: x => x.BusinessAvailabilityId1,
                        principalTable: "BusinessAvailabilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessAvailabilities_BusinessId",
                table: "BusinessAvailabilities",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessAvailabilitySlots_BusinessAvailabilityId",
                table: "BusinessAvailabilitySlots",
                column: "BusinessAvailabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessAvailabilitySlots_BusinessAvailabilityId1",
                table: "BusinessAvailabilitySlots",
                column: "BusinessAvailabilityId1");

            migrationBuilder.CreateIndex(
                name: "IX_businesses_UserId",
                table: "businesses",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessAvailabilitySlots");

            migrationBuilder.DropTable(
                name: "BusinessAvailabilities");

            migrationBuilder.DropTable(
                name: "businesses");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
