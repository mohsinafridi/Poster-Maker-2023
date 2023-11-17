using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosterMaker.API.Migrations
{
    /// <inheritdoc />
    public partial class jsonaddedinitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JsonPath",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JsonPath",
                table: "Items");
        }
    }
}
