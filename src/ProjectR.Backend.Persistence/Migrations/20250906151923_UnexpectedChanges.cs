using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectR.Backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UnexpectedChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessAvailabilities_businesses_BusinessId",
                table: "BusinessAvailabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessAvailabilitySlots_BusinessAvailabilities_BusinessAv~",
                table: "BusinessAvailabilitySlots");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessAvailabilitySlots_BusinessAvailabilities_BusinessA~1",
                table: "BusinessAvailabilitySlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessAvailabilitySlots",
                table: "BusinessAvailabilitySlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessAvailabilities",
                table: "BusinessAvailabilities");

            migrationBuilder.DropColumn(
                name: "Breaks",
                table: "BusinessAvailabilitySlots");

            migrationBuilder.DropColumn(
                name: "ValidFrom",
                table: "BusinessAvailabilities");

            migrationBuilder.DropColumn(
                name: "ValidTo",
                table: "BusinessAvailabilities");

            migrationBuilder.RenameTable(
                name: "BusinessAvailabilitySlots",
                newName: "BusinessAvailabilitySlot");

            migrationBuilder.RenameTable(
                name: "BusinessAvailabilities",
                newName: "BusinessAvailability");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessAvailabilitySlots_BusinessAvailabilityId1",
                table: "BusinessAvailabilitySlot",
                newName: "IX_BusinessAvailabilitySlot_BusinessAvailabilityId1");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessAvailabilitySlots_BusinessAvailabilityId",
                table: "BusinessAvailabilitySlot",
                newName: "IX_BusinessAvailabilitySlot_BusinessAvailabilityId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessAvailabilities_BusinessId",
                table: "BusinessAvailability",
                newName: "IX_BusinessAvailability_BusinessId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessAvailabilitySlot",
                table: "BusinessAvailabilitySlot",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessAvailability",
                table: "BusinessAvailability",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Break",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BusinessAvailabilitySlotId = table.Column<Guid>(type: "uuid", nullable: false),
                    BusinessAvailabilitySlotId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    RecordStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Break", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Break_BusinessAvailabilitySlot_BusinessAvailabilitySlotId",
                        column: x => x.BusinessAvailabilitySlotId,
                        principalTable: "BusinessAvailabilitySlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Break_BusinessAvailabilitySlot_BusinessAvailabilitySlotId1",
                        column: x => x.BusinessAvailabilitySlotId1,
                        principalTable: "BusinessAvailabilitySlot",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Break_BusinessAvailabilitySlotId",
                table: "Break",
                column: "BusinessAvailabilitySlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Break_BusinessAvailabilitySlotId1",
                table: "Break",
                column: "BusinessAvailabilitySlotId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessAvailability_businesses_BusinessId",
                table: "BusinessAvailability",
                column: "BusinessId",
                principalTable: "businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessAvailabilitySlot_BusinessAvailability_BusinessAvail~",
                table: "BusinessAvailabilitySlot",
                column: "BusinessAvailabilityId",
                principalTable: "BusinessAvailability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessAvailabilitySlot_BusinessAvailability_BusinessAvai~1",
                table: "BusinessAvailabilitySlot",
                column: "BusinessAvailabilityId1",
                principalTable: "BusinessAvailability",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessAvailability_businesses_BusinessId",
                table: "BusinessAvailability");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessAvailabilitySlot_BusinessAvailability_BusinessAvail~",
                table: "BusinessAvailabilitySlot");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessAvailabilitySlot_BusinessAvailability_BusinessAvai~1",
                table: "BusinessAvailabilitySlot");

            migrationBuilder.DropTable(
                name: "Break");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessAvailabilitySlot",
                table: "BusinessAvailabilitySlot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessAvailability",
                table: "BusinessAvailability");

            migrationBuilder.RenameTable(
                name: "BusinessAvailabilitySlot",
                newName: "BusinessAvailabilitySlots");

            migrationBuilder.RenameTable(
                name: "BusinessAvailability",
                newName: "BusinessAvailabilities");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessAvailabilitySlot_BusinessAvailabilityId1",
                table: "BusinessAvailabilitySlots",
                newName: "IX_BusinessAvailabilitySlots_BusinessAvailabilityId1");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessAvailabilitySlot_BusinessAvailabilityId",
                table: "BusinessAvailabilitySlots",
                newName: "IX_BusinessAvailabilitySlots_BusinessAvailabilityId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessAvailability_BusinessId",
                table: "BusinessAvailabilities",
                newName: "IX_BusinessAvailabilities_BusinessId");

            migrationBuilder.AddColumn<List<TimeOnly>>(
                name: "Breaks",
                table: "BusinessAvailabilitySlots",
                type: "time without time zone[]",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ValidFrom",
                table: "BusinessAvailabilities",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ValidTo",
                table: "BusinessAvailabilities",
                type: "date",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessAvailabilitySlots",
                table: "BusinessAvailabilitySlots",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessAvailabilities",
                table: "BusinessAvailabilities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessAvailabilities_businesses_BusinessId",
                table: "BusinessAvailabilities",
                column: "BusinessId",
                principalTable: "businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessAvailabilitySlots_BusinessAvailabilities_BusinessAv~",
                table: "BusinessAvailabilitySlots",
                column: "BusinessAvailabilityId",
                principalTable: "BusinessAvailabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessAvailabilitySlots_BusinessAvailabilities_BusinessA~1",
                table: "BusinessAvailabilitySlots",
                column: "BusinessAvailabilityId1",
                principalTable: "BusinessAvailabilities",
                principalColumn: "Id");
        }
    }
}
