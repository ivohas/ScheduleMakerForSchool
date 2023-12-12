using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schedule.Data.Migrations
{
    public partial class RelationBetweenSPWAndClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectsPerWeeks_Classes_ClassId",
                table: "SubjectsPerWeeks");

            migrationBuilder.RenameColumn(
                name: "HourSPerWeek",
                table: "SubjectsPerWeeks",
                newName: "HoursPerWeek");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClassId",
                table: "SubjectsPerWeeks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectsPerWeeks_Classes_ClassId",
                table: "SubjectsPerWeeks",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectsPerWeeks_Classes_ClassId",
                table: "SubjectsPerWeeks");

            migrationBuilder.RenameColumn(
                name: "HoursPerWeek",
                table: "SubjectsPerWeeks",
                newName: "HourSPerWeek");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClassId",
                table: "SubjectsPerWeeks",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectsPerWeeks_Classes_ClassId",
                table: "SubjectsPerWeeks",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id");
        }
    }
}
