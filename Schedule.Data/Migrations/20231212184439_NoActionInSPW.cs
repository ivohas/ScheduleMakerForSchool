using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schedule.Data.Migrations
{
    public partial class NoActionInSPW : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectsPerWeeks_Classes_ClassId",
                table: "SubjectsPerWeeks");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectsPerWeeks_Classes_ClassId",
                table: "SubjectsPerWeeks",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectsPerWeeks_Classes_ClassId",
                table: "SubjectsPerWeeks");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectsPerWeeks_Classes_ClassId",
                table: "SubjectsPerWeeks",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
