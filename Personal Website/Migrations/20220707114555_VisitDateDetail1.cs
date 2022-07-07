using Microsoft.EntityFrameworkCore.Migrations;

namespace Personal_Website.Migrations
{
    public partial class VisitDateDetail1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitDetails_VisitDetails_VisitDetailId",
                table: "VisitDetails");

            migrationBuilder.DropIndex(
                name: "IX_VisitDetails_VisitDetailId",
                table: "VisitDetails");

            migrationBuilder.DropColumn(
                name: "VisitDetailId",
                table: "VisitDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VisitDetailId",
                table: "VisitDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisitDetails_VisitDetailId",
                table: "VisitDetails",
                column: "VisitDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitDetails_VisitDetails_VisitDetailId",
                table: "VisitDetails",
                column: "VisitDetailId",
                principalTable: "VisitDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
