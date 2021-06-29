using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingManager.Migrations
{
    public partial class RenameColumnName_Participants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingUser_Users_PartitipantsId",
                table: "MeetingUser");

            migrationBuilder.RenameColumn(
                name: "PartitipantsId",
                table: "MeetingUser",
                newName: "ParticipantsId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingUser_PartitipantsId",
                table: "MeetingUser",
                newName: "IX_MeetingUser_ParticipantsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingUser_Users_ParticipantsId",
                table: "MeetingUser",
                column: "ParticipantsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingUser_Users_ParticipantsId",
                table: "MeetingUser");

            migrationBuilder.RenameColumn(
                name: "ParticipantsId",
                table: "MeetingUser",
                newName: "PartitipantsId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingUser_ParticipantsId",
                table: "MeetingUser",
                newName: "IX_MeetingUser_PartitipantsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingUser_Users_PartitipantsId",
                table: "MeetingUser",
                column: "PartitipantsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
