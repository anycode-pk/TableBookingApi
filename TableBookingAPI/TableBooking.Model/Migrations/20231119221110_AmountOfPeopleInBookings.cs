using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableBooking.Model.Migrations
{
    public partial class AmountOfPeopleInBookings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountOfPeople",
                table: "Bookings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfPeople",
                table: "Bookings");
        }
    }
}
