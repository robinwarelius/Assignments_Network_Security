using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoT_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class updateadvertmodel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Advertisings",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Advertisings");
        }
    }
}
