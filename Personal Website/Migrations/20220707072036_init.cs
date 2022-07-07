using Microsoft.EntityFrameworkCore.Migrations;

namespace Personal_Website.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalName = table.Column<string>(nullable: true),
                    InternationalName = table.Column<string>(nullable: true),
                    ISO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "iPRanges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeginIPAddress = table.Column<long>(nullable: false),
                    EndIPAddress = table.Column<long>(nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iPRanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_iPRanges_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IP = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    Minutes = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visitors_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_iPRanges_CountryId",
                table: "iPRanges",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_CountryId",
                table: "Visitors",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "iPRanges");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
