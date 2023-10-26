using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetAssign2.Migrations
{
    /// <inheritdoc />
    public partial class Relationchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEvent_AspNetUsers_UserId",
                table: "UserEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEvent_Events_EventId",
                table: "UserEvent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEvent",
                table: "UserEvent");

            migrationBuilder.RenameTable(
                name: "UserEvent",
                newName: "UserEvents");

            migrationBuilder.RenameIndex(
                name: "IX_UserEvent_UserId",
                table: "UserEvents",
                newName: "IX_UserEvents_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserEvent_EventId",
                table: "UserEvents",
                newName: "IX_UserEvents_EventId");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserEvents",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEvents",
                table: "UserEvents",
                columns: new[] { "UsersId", "EventsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvents_AspNetUsers_UserId",
                table: "UserEvents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvents_Events_EventId",
                table: "UserEvents",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEvents_AspNetUsers_UserId",
                table: "UserEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEvents_Events_EventId",
                table: "UserEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEvents",
                table: "UserEvents");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "UserEvents",
                newName: "UserEvent");

            migrationBuilder.RenameIndex(
                name: "IX_UserEvents_UserId",
                table: "UserEvent",
                newName: "IX_UserEvent_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserEvents_EventId",
                table: "UserEvent",
                newName: "IX_UserEvent_EventId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserEvent",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEvent",
                table: "UserEvent",
                columns: new[] { "UsersId", "EventsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvent_AspNetUsers_UserId",
                table: "UserEvent",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvent_Events_EventId",
                table: "UserEvent",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
