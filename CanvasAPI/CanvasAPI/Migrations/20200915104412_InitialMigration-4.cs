using Microsoft.EntityFrameworkCore.Migrations;

namespace CanvasAPI.Migrations
{
    public partial class InitialMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Poll_Poll_ID1",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_Poll_ID1",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "Poll_ID1",
                table: "Vote");

            migrationBuilder.AddColumn<int>(
                name: "PollOptionOption_ID",
                table: "Vote",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "User_ID",
                table: "Poll",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vote_PollOptionOption_ID",
                table: "Vote",
                column: "PollOptionOption_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Poll_User_ID",
                table: "Poll",
                column: "User_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Poll_User_User_ID",
                table: "Poll",
                column: "User_ID",
                principalTable: "User",
                principalColumn: "User_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_PollOption_PollOptionOption_ID",
                table: "Vote",
                column: "PollOptionOption_ID",
                principalTable: "PollOption",
                principalColumn: "Option_ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poll_User_User_ID",
                table: "Poll");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_PollOption_PollOptionOption_ID",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_PollOptionOption_ID",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Poll_User_ID",
                table: "Poll");

            migrationBuilder.DropColumn(
                name: "PollOptionOption_ID",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "User_ID",
                table: "Poll");

            migrationBuilder.AddColumn<int>(
                name: "Poll_ID1",
                table: "Vote",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vote_Poll_ID1",
                table: "Vote",
                column: "Poll_ID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Poll_Poll_ID1",
                table: "Vote",
                column: "Poll_ID1",
                principalTable: "Poll",
                principalColumn: "Poll_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
