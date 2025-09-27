using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectR.Backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangedFromTimeOnlyToDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "StartTime",
                table: "BusinessAvailabilitySlots",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldNullable: false,
                oldType: "time without time zone")
                 .Annotation("Npgsql:AlterColumnType", "USING \"StartTime\"::time AT TIME ZONE 'UTC'");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "EndTime",
                table: "BusinessAvailabilitySlots",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                  oldNullable: false,
                oldType: "time without time zone")
                 .Annotation("Npgsql:AlterColumnType", "USING \"StartTime\"::time AT TIME ZONE 'UTC'");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "StartTime",
                table: "Breaks",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                  oldNullable: false,
                oldType: "time without time zone")
                 .Annotation("Npgsql:AlterColumnType", "USING \"StartTime\"::time AT TIME ZONE 'UTC'");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "EndTime",
                table: "Breaks",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                  oldNullable: false,
                oldType: "time without time zone")
                 .Annotation("Npgsql:AlterColumnType", "USING \"StartTime\"::time AT TIME ZONE 'UTC'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "StartTime",
                table: "BusinessAvailabilitySlots",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "EndTime",
                table: "BusinessAvailabilitySlots",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "StartTime",
                table: "Breaks",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "EndTime",
                table: "Breaks",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");
        }
    }
}
