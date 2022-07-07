using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Personal_Website.Migrations
{
    public partial class addVisitDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitors_Countries_CountryId",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "Minutes",
                table: "Visitors");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Visitors",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "VisitDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitorId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    DateDetail = table.Column<long>(nullable: false),
                    Minutes = table.Column<long>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitDetails_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitDetails_VisitorId",
                table: "VisitDetails",
                column: "VisitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitors_Countries_CountryId",
                table: "Visitors",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitors_Countries_CountryId",
                table: "Visitors");

            migrationBuilder.DropTable(
                name: "VisitDetails");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Visitors",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Visitors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "Minutes",
                table: "Visitors",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitors_Countries_CountryId",
                table: "Visitors",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
