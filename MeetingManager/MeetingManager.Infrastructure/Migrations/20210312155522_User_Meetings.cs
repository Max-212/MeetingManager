using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingManager.Migrations
{
    public partial class User_Meetings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Meetings_MeetingId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_MeetingId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MeetingId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "MeetingUser",
                columns: table => new
                {
                    MeetingsId = table.Column<int>(type: "int", nullable: false),
                    PartitipantsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingUser", x => new { x.MeetingsId, x.PartitipantsId });
                    table.ForeignKey(
                        name: "FK_MeetingUser_Meetings_MeetingsId",
                        column: x => x.MeetingsId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetingUser_Users_PartitipantsId",
                        column: x => x.PartitipantsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingUser_PartitipantsId",
                table: "MeetingUser",
                column: "PartitipantsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetingUser");

            migrationBuilder.AddColumn<int>(
                name: "MeetingId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MeetingId",
                table: "Users",
                column: "MeetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Meetings_MeetingId",
                table: "Users",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
