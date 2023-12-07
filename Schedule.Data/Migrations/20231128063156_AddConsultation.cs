using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schedule.Data.Migrations
{
    public partial class AddConsultation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectsPerWeek_Classes_ClassId",
                table: "SubjectsPerWeek");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectsPerWeek",
                table: "SubjectsPerWeek");

            migrationBuilder.RenameTable(
                name: "SubjectsPerWeek",
                newName: "SubjectsPerWeeks");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectsPerWeek_ClassId",
                table: "SubjectsPerWeeks",
                newName: "IX_SubjectsPerWeeks_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectsPerWeeks",
                table: "SubjectsPerWeeks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Consultations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartHour = table.Column<TimeSpan>(type: "time", nullable: false),
                    TimeLenght = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultations_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultations_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_SubjectId",
                table: "Consultations",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_TeacherId",
                table: "Consultations",
                column: "TeacherId");

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

            migrationBuilder.DropTable(
                name: "Consultations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectsPerWeeks",
                table: "SubjectsPerWeeks");

            migrationBuilder.RenameTable(
                name: "SubjectsPerWeeks",
                newName: "SubjectsPerWeek");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectsPerWeeks_ClassId",
                table: "SubjectsPerWeek",
                newName: "IX_SubjectsPerWeek_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectsPerWeek",
                table: "SubjectsPerWeek",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectsPerWeek_Classes_ClassId",
                table: "SubjectsPerWeek",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id");
        }
    }
}
