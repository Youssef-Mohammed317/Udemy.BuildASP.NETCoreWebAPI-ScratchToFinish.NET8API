using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.Infra.Identity.Migrations
{
    /// <inheritdoc />
    public partial class addidentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a0117157-56cc-494e-805c-8e4ca22086ec", "a0117157-56cc-494e-805c-8e4ca22086ec", "Reader", "READER" },
                    { "b34006d3-0323-46c3-89a6-8997578aaae7", "b34006d3-0323-46c3-89a6-8997578aaae7", "Writer", "WRITER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0117157-56cc-494e-805c-8e4ca22086ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b34006d3-0323-46c3-89a6-8997578aaae7");

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a0117157-56cc-494e-805c-8e4ca22086ec", "a0117157-56cc-494e-805c-8e4ca22086ec", "Reader", "READER" },
                    { "b34006d3-0323-46c3-89a6-8997578aaae7", "b34006d3-0323-46c3-89a6-8997578aaae7", "Writer", "WRITER" }
                });
        }
    }
}
