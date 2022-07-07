using Microsoft.EntityFrameworkCore.Migrations;

namespace Personal_Website.Migrations
{
    public partial class fixIpRange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_iPRanges_Countries_CountryId",
                table: "iPRanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_iPRanges",
                table: "iPRanges");

            migrationBuilder.RenameTable(
                name: "iPRanges",
                newName: "IPRanges");

            migrationBuilder.RenameIndex(
                name: "IX_iPRanges_CountryId",
                table: "IPRanges",
                newName: "IX_IPRanges_CountryId");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "IPRanges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IPRanges",
                table: "IPRanges",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IPRanges_Countries_CountryId",
                table: "IPRanges",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IPRanges_Countries_CountryId",
                table: "IPRanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IPRanges",
                table: "IPRanges");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "IPRanges");

            migrationBuilder.RenameTable(
                name: "IPRanges",
                newName: "iPRanges");

            migrationBuilder.RenameIndex(
                name: "IX_IPRanges_CountryId",
                table: "iPRanges",
                newName: "IX_iPRanges_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_iPRanges",
                table: "iPRanges",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_iPRanges_Countries_CountryId",
                table: "iPRanges",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
