using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iphound.API.Migrations
{
    /// <inheritdoc />
    public partial class RenameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IpAddress_Countries_CountryId",
                table: "IpAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IpAddress",
                table: "IpAddress");

            migrationBuilder.RenameTable(
                name: "IpAddress",
                newName: "IpAddresses");

            migrationBuilder.RenameIndex(
                name: "IX_IpAddress_CountryId",
                table: "IpAddresses",
                newName: "IX_IpAddresses_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IpAddresses",
                table: "IpAddresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IpAddresses_Countries_CountryId",
                table: "IpAddresses",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IpAddresses_Countries_CountryId",
                table: "IpAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IpAddresses",
                table: "IpAddresses");

            migrationBuilder.RenameTable(
                name: "IpAddresses",
                newName: "IpAddress");

            migrationBuilder.RenameIndex(
                name: "IX_IpAddresses_CountryId",
                table: "IpAddress",
                newName: "IX_IpAddress_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IpAddress",
                table: "IpAddress",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IpAddress_Countries_CountryId",
                table: "IpAddress",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
