﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetAssign2.Data.Migrations
{
    public partial class AddPermissionLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
            name: "PermissionLevel",
            table: "AspNetUsers",
            type: "int",
            nullable: false,
            defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "PermissionLevel",
            table: "AspNetUsers");
        }
    }
}
