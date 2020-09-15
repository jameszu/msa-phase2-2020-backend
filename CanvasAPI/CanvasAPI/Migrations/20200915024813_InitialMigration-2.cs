using Microsoft.EntityFrameworkCore.Migrations;

namespace CanvasAPI.Migrations
{
    public partial class InitialMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Poll",
                columns: table => new
                {
                    Poll_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pool_Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poll", x => x.Poll_ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    User_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "PollOption",
                columns: table => new
                {
                    Option_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Poll_ID = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    Poll_ID1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollOption", x => x.Option_ID);
                    table.ForeignKey(
                        name: "FK_PollOption_Poll_Poll_ID1",
                        column: x => x.Poll_ID1,
                        principalTable: "Poll",
                        principalColumn: "Poll_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    User_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Poll_ID = table.Column<int>(nullable: false),
                    Poll_ID1 = table.Column<int>(nullable: true),
                    User_ID1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => x.User_ID);
                    table.ForeignKey(
                        name: "FK_Vote_Poll_Poll_ID1",
                        column: x => x.Poll_ID1,
                        principalTable: "Poll",
                        principalColumn: "Poll_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vote_User_User_ID1",
                        column: x => x.User_ID1,
                        principalTable: "User",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PollOption_Poll_ID1",
                table: "PollOption",
                column: "Poll_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_Poll_ID1",
                table: "Vote",
                column: "Poll_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_User_ID1",
                table: "Vote",
                column: "User_ID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PollOption");

            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.DropTable(
                name: "Poll");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
