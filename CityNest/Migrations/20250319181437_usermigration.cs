using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityNest.Migrations
{
    /// <inheritdoc />
    public partial class usermigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Users_UsersId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "OfferType",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Square",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "Properties",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_UsersId",
                table: "Properties",
                newName: "IX_Properties_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Users_UserId",
                table: "Properties",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Users_UserId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Properties",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_UserId",
                table: "Properties",
                newName: "IX_Properties_UsersId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "Properties",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfferType",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Square",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Users_UsersId",
                table: "Properties",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
