using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableBooking.Model.Migrations
{
    /// <inheritdoc />
    public partial class OpeningAndClosingHoursEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_WednesdayOpen",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_Wednesday_OpenTime");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_WednesdayClose",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_Wednesday_CloseTime");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_TuesdayOpen",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_Tuesday_OpenTime");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_TuesdayClose",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_Tuesday_CloseTime");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_ThursdayOpen",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_Thursday_OpenTime");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_ThursdayClose",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_Thursday_CloseTime");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_SundayOpen",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_Sunday_OpenTime");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_SundayClose",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_Sunday_CloseTime");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_SaturdayOpen",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_Saturday_OpenTime");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_SaturdayClose",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_Saturday_CloseTime");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_MondayOpen",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_Monday_OpenTime");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_MondayClose",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_Monday_CloseTime");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_FridayOpen",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_Friday_OpenTime");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_FridayClose",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_Friday_CloseTime");

            migrationBuilder.AddColumn<bool>(
                name: "OpeningAndClosingHours_Friday_Closed",
                table: "Restaurants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OpeningAndClosingHours_Monday_Closed",
                table: "Restaurants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OpeningAndClosingHours_Saturday_Closed",
                table: "Restaurants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OpeningAndClosingHours_Sunday_Closed",
                table: "Restaurants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OpeningAndClosingHours_Thursday_Closed",
                table: "Restaurants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OpeningAndClosingHours_Tuesday_Closed",
                table: "Restaurants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OpeningAndClosingHours_Wednesday_Closed",
                table: "Restaurants",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_Friday_Closed",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_Monday_Closed",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_Saturday_Closed",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_Sunday_Closed",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_Thursday_Closed",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_Tuesday_Closed",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpeningAndClosingHours_Wednesday_Closed",
                table: "Restaurants");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_Wednesday_OpenTime",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_WednesdayOpen");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_Wednesday_CloseTime",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_WednesdayClose");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_Tuesday_OpenTime",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_TuesdayOpen");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_Tuesday_CloseTime",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_TuesdayClose");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_Thursday_OpenTime",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_ThursdayOpen");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_Thursday_CloseTime",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_ThursdayClose");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_Sunday_OpenTime",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_SundayOpen");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_Sunday_CloseTime",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_SundayClose");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_Saturday_OpenTime",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_SaturdayOpen");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_Saturday_CloseTime",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_SaturdayClose");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_Monday_OpenTime",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_MondayOpen");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_Monday_CloseTime",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_MondayClose");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_Friday_OpenTime",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_FridayOpen");

            migrationBuilder.RenameColumn(
                name: "OpeningAndClosingHours_Friday_CloseTime",
                table: "Restaurants",
                newName: "OpeningAndClosingHours_FridayClose");
        }
    }
}
