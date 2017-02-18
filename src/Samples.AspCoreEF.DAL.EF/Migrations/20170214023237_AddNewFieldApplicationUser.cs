using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Samples.AspCoreEF.DAL.EF.Migrations
{
    public partial class AddNewFieldApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AccountExpires",
                table: "ApplicationUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataEventRecordsRole",
                table: "ApplicationUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "ApplicationUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecuredFilesRole",
                table: "ApplicationUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountExpires",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "DataEventRecordsRole",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "SecuredFilesRole",
                table: "ApplicationUsers");
        }
    }
}
