using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schedule.Data.Migrations
{
    public partial class SubjectsPerWeekForClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassTeacher_Teachers_TeacherId",
                table: "ClassTeacher");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "ClassTeacher",
                newName: "TeachersId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassTeacher_TeacherId",
                table: "ClassTeacher",
                newName: "IX_ClassTeacher_TeachersId");

            migrationBuilder.CreateTable(
                name: "SubjectsPerWeek",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HourSPerWeek = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectsPerWeek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectsPerWeek_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectsPerWeek_ClassId",
                table: "SubjectsPerWeek",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassTeacher_Teachers_TeachersId",
                table: "ClassTeacher",
                column: "TeachersId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassTeacher_Teachers_TeachersId",
                table: "ClassTeacher");

            migrationBuilder.DropTable(
                name: "SubjectsPerWeek");

            migrationBuilder.RenameColumn(
                name: "TeachersId",
                table: "ClassTeacher",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassTeacher_TeachersId",
                table: "ClassTeacher",
                newName: "IX_ClassTeacher_TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassTeacher_Teachers_TeacherId",
                table: "ClassTeacher",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
