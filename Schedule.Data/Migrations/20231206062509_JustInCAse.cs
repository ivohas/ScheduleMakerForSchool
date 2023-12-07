using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schedule.Data.Migrations
{
    public partial class JustInCAse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserConsultation_AspNetUsers_ApplicationUserID",
                table: "UserConsultation");

            migrationBuilder.DropForeignKey(
                name: "FK_UserConsultation_Consultations_ConsultationID",
                table: "UserConsultation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserConsultation",
                table: "UserConsultation");

            migrationBuilder.RenameTable(
                name: "UserConsultation",
                newName: "UsersConsultation");

            migrationBuilder.RenameIndex(
                name: "IX_UserConsultation_ConsultationID",
                table: "UsersConsultation",
                newName: "IX_UsersConsultation_ConsultationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersConsultation",
                table: "UsersConsultation",
                columns: new[] { "ApplicationUserID", "ConsultationID" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsersConsultation_AspNetUsers_ApplicationUserID",
                table: "UsersConsultation",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersConsultation_Consultations_ConsultationID",
                table: "UsersConsultation",
                column: "ConsultationID",
                principalTable: "Consultations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersConsultation_AspNetUsers_ApplicationUserID",
                table: "UsersConsultation");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersConsultation_Consultations_ConsultationID",
                table: "UsersConsultation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersConsultation",
                table: "UsersConsultation");

            migrationBuilder.RenameTable(
                name: "UsersConsultation",
                newName: "UserConsultation");

            migrationBuilder.RenameIndex(
                name: "IX_UsersConsultation_ConsultationID",
                table: "UserConsultation",
                newName: "IX_UserConsultation_ConsultationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserConsultation",
                table: "UserConsultation",
                columns: new[] { "ApplicationUserID", "ConsultationID" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserConsultation_AspNetUsers_ApplicationUserID",
                table: "UserConsultation",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserConsultation_Consultations_ConsultationID",
                table: "UserConsultation",
                column: "ConsultationID",
                principalTable: "Consultations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
