using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Infrastructure.SqlServer.Migrations
{
    public partial class CreateContentsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "contents");

            migrationBuilder.CreateTable(
                name: "TBL_CONTENTS",
                schema: "contents",
                columns: table => new
                {
                    CTNT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CTNT_TITLE = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    CTNT_SUBTITLE = table.Column<string>(type: "nvarchar(270)", maxLength: 270, nullable: false),
                    CTNT_FEATIMG = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONTENTS", x => x.CTNT_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_CONTENTS",
                schema: "contents");
        }
    }
}
