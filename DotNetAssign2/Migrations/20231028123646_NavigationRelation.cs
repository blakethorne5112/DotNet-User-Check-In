using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetAssign2.Migrations
{
    /// <inheritdoc />
    public partial class NavigationRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLocations_Locations_LocationsID",
                table: "UserLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLocations_Users_UsersID",
                table: "UserLocations");

            migrationBuilder.DropIndex(
                name: "IX_UserLocations_LocationsID",
                table: "UserLocations");

            migrationBuilder.RenameColumn(
                name: "LocationsID",
                table: "UserLocations",
                newName: "LocationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationsId",
                table: "UserLocations",
                newName: "LocationsID");

            migrationBuilder.CreateIndex(
                name: "IX_UserLocations_LocationsID",
                table: "UserLocations",
                column: "LocationsID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLocations_Locations_LocationsID",
                table: "UserLocations",
                column: "LocationsID",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLocations_Users_UsersID",
                table: "UserLocations",
                column: "UsersID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
