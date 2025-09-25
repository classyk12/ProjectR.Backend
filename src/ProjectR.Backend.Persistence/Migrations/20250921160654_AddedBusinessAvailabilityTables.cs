using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectR.Backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedBusinessAvailabilityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Break_BusinessAvailabilitySlot_BusinessAvailabilitySlotId",
                table: "Break");

            migrationBuilder.DropForeignKey(
                name: "FK_Break_BusinessAvailabilitySlot_BusinessAvailabilitySlotId1",
                table: "Break");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessAvailability_businesses_BusinessId",
                table: "BusinessAvailability");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessAvailabilitySlot_BusinessAvailability_BusinessAvail~",
                table: "BusinessAvailabilitySlot");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessAvailabilitySlot_BusinessAvailability_BusinessAvai~1",
                table: "BusinessAvailabilitySlot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessAvailabilitySlot",
                table: "BusinessAvailabilitySlot");

            migrationBuilder.DropIndex(
                name: "IX_BusinessAvailabilitySlot_BusinessAvailabilityId1",
                table: "BusinessAvailabilitySlot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessAvailability",
                table: "BusinessAvailability");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Break",
                table: "Break");

            migrationBuilder.DropIndex(
                name: "IX_Break_BusinessAvailabilitySlotId1",
                table: "Break");

            migrationBuilder.DropColumn(
                name: "BusinessAvailabilityId1",
                table: "BusinessAvailabilitySlot");

            migrationBuilder.DropColumn(
                name: "BusinessAvailabilitySlotId1",
                table: "Break");

            migrationBuilder.RenameTable(
                name: "BusinessAvailabilitySlot",
                newName: "BusinessAvailabilitySlots");

            migrationBuilder.RenameTable(
                name: "BusinessAvailability",
                newName: "BusinessAvailabilities");

            migrationBuilder.RenameTable(
                name: "Break",
                newName: "Breaks");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessAvailabilitySlot_BusinessAvailabilityId",
                table: "BusinessAvailabilitySlots",
                newName: "IX_BusinessAvailabilitySlots_BusinessAvailabilityId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessAvailability_BusinessId",
                table: "BusinessAvailabilities",
                newName: "IX_BusinessAvailabilities_BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_Break_BusinessAvailabilitySlotId",
                table: "Breaks",
                newName: "IX_Breaks_BusinessAvailabilitySlotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessAvailabilitySlots",
                table: "BusinessAvailabilitySlots",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessAvailabilities",
                table: "BusinessAvailabilities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Breaks",
                table: "Breaks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Breaks_BusinessAvailabilitySlots_BusinessAvailabilitySlotId",
                table: "Breaks",
                column: "BusinessAvailabilitySlotId",
                principalTable: "BusinessAvailabilitySlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breaks_BusinessAvailabilitySlots_BusinessAvailabilitySlotId",
                table: "Breaks");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessAvailabilities_businesses_BusinessId",
                table: "BusinessAvailabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessAvailabilitySlots_BusinessAvailabilities_BusinessAv~",
                table: "BusinessAvailabilitySlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessAvailabilitySlots",
                table: "BusinessAvailabilitySlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessAvailabilities",
                table: "BusinessAvailabilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Breaks",
                table: "Breaks");

            migrationBuilder.RenameTable(
                name: "BusinessAvailabilitySlots",
                newName: "BusinessAvailabilitySlot");

            migrationBuilder.RenameTable(
                name: "BusinessAvailabilities",
                newName: "BusinessAvailability");

            migrationBuilder.RenameTable(
                name: "Breaks",
                newName: "Break");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessAvailabilitySlots_BusinessAvailabilityId",
                table: "BusinessAvailabilitySlot",
                newName: "IX_BusinessAvailabilitySlot_BusinessAvailabilityId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessAvailabilities_BusinessId",
                table: "BusinessAvailability",
                newName: "IX_BusinessAvailability_BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_Breaks_BusinessAvailabilitySlotId",
                table: "Break",
                newName: "IX_Break_BusinessAvailabilitySlotId");

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessAvailabilityId1",
                table: "BusinessAvailabilitySlot",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessAvailabilitySlotId1",
                table: "Break",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessAvailabilitySlot",
                table: "BusinessAvailabilitySlot",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessAvailability",
                table: "BusinessAvailability",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Break",
                table: "Break",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessAvailabilitySlot_BusinessAvailabilityId1",
                table: "BusinessAvailabilitySlot",
                column: "BusinessAvailabilityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Break_BusinessAvailabilitySlotId1",
                table: "Break",
                column: "BusinessAvailabilitySlotId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Break_BusinessAvailabilitySlot_BusinessAvailabilitySlotId",
                table: "Break",
                column: "BusinessAvailabilitySlotId",
                principalTable: "BusinessAvailabilitySlot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Break_BusinessAvailabilitySlot_BusinessAvailabilitySlotId1",
                table: "Break",
                column: "BusinessAvailabilitySlotId1",
                principalTable: "BusinessAvailabilitySlot",
                principalColumn: "Id");

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
    }
}
