using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.Infa.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLocalFilePathToImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocalFilePath",
                table: "Image",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalFilePath",
                table: "Image");
        }
    }
}
