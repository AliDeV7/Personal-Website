using Microsoft.EntityFrameworkCore.Migrations;

namespace Personal_Website.Migrations
{
    public partial class VisitDateDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "VisitDetails");

            migrationBuilder.DropColumn(
                name: "DateDetail",
                table: "VisitDetails");

            migrationBuilder.DropColumn(
                name: "Minutes",
                table: "VisitDetails");

            migrationBuilder.AddColumn<long>(
                name: "Period",
                table: "VisitDetails",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "VisitDetailId",
                table: "VisitDetails",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VisitDateDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitDetailId = table.Column<int>(nullable: false),
                    FullDate = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitDateDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitDateDetails_VisitDetails_VisitDetailId",
                        column: x => x.VisitDetailId,
                        principalTable: "VisitDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitDetails_VisitDetailId",
                table: "VisitDetails",
                column: "VisitDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitDateDetails_VisitDetailId",
                table: "VisitDateDetails",
                column: "VisitDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitDetails_VisitDetails_VisitDetailId",
                table: "VisitDetails",
                column: "VisitDetailId",
                principalTable: "VisitDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitDetails_VisitDetails_VisitDetailId",
                table: "VisitDetails");

            migrationBuilder.DropTable(
                name: "VisitDateDetails");

            migrationBuilder.DropIndex(
                name: "IX_VisitDetails_VisitDetailId",
                table: "VisitDetails");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "VisitDetails");

            migrationBuilder.DropColumn(
                name: "VisitDetailId",
                table: "VisitDetails");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "VisitDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "DateDetail",
                table: "VisitDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Minutes",
                table: "VisitDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
