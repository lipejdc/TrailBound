using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrailBound.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlsAndThumbnail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "ImageURLs",
                table: "Activities",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Activities",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURLs",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Activities");
        }
    }
}
