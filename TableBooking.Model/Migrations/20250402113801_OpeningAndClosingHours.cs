using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableBooking.Model.Migrations
{
    /// <inheritdoc />
    public partial class OpeningAndClosingHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseTime",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpenTime",
                table: "Restaurants");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningAndClosingHours_FridayClose",
                table: "Restaurants",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningAndClosingHours_FridayOpen",
                table: "Restaurants",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningAndClosingHours_MondayClose",
                table: "Restaurants",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningAndClosingHours_MondayOpen",
                table: "Restaurants",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningAndClosingHours_SaturdayClose",
                table: "Restaurants",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningAndClosingHours_SaturdayOpen",
                table: "Restaurants",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningAndClosingHours_SundayClose",
                table: "Restaurants",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningAndClosingHours_SundayOpen",
                table: "Restaurants",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningAndClosingHours_ThursdayClose",
                table: "Restaurants",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningAndClosingHours_ThursdayOpen",
                table: "Restaurants",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningAndClosingHours_TuesdayClose",
                table: "Restaurants",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningAndClosingHours_TuesdayOpen",
                table: "Restaurants",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningAndClosingHours_WednesdayClose",
                table: "Restaurants",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningAndClosingHours_WednesdayOpen",
                table: "Restaurants",
                type: "interval",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_FridayClose",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_FridayOpen",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_MondayClose",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_MondayOpen",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_SaturdayClose",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_SaturdayOpen",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_SundayClose",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_SundayOpen",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_ThursdayClose",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_ThursdayOpen",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_TuesdayClose",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_TuesdayOpen",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_WednesdayClose",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_WednesdayOpen",
                table: "Restaurants");

            migrationBuilder.AddColumn<DateTime>(
                name: "CloseTime",
                table: "Restaurants",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OpenTime",
                table: "Restaurants",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
