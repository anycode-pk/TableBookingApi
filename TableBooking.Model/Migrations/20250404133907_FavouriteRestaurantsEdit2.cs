using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableBooking.Model.Migrations
{
    /// <inheritdoc />
    public partial class FavouriteRestaurantsEdit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavouriteRestaurants");

            migrationBuilder.CreateTable(
                name: "UserFavouriteRestaurant",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RestaurantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavouriteRestaurant", x => new { x.UserId, x.RestaurantId });
                    table.ForeignKey(
                        name: "FK_UserFavouriteRestaurant_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavouriteRestaurant_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavouriteRestaurant_RestaurantId",
                table: "UserFavouriteRestaurant",
                column: "RestaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavouriteRestaurant");

            migrationBuilder.CreateTable(
                name: "UserFavouriteRestaurants",
                columns: table => new
                {
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FavouriteRestaurantsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavouriteRestaurants", x => new { x.AppUserId, x.FavouriteRestaurantsId });
                    table.ForeignKey(
                        name: "FK_UserFavouriteRestaurants_Restaurants_FavouriteRestaurantsId",
                        column: x => x.FavouriteRestaurantsId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavouriteRestaurants_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavouriteRestaurants_FavouriteRestaurantsId",
                table: "UserFavouriteRestaurants",
                column: "FavouriteRestaurantsId");
        }
    }
}
