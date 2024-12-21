using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableBooking.Model.Migrations
{
    public partial class restaurant_rating_and_approle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppRoleId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Restaurants",
                type: "double precision",
                precision: 1,
                scale: 1,
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NormalizedName = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AppRoleId",
                table: "Users",
                column: "AppRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_AppRoleId",
                table: "Users",
                column: "AppRoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_AppRoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Users_AppRoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AppRoleId",
                table: "Users");

            migrationBuilder.AlterColumn<float>(
                name: "Rating",
                table: "Restaurants",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldPrecision: 1,
                oldScale: 1);
        }
    }
}
