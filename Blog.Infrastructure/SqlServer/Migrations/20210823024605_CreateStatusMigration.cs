using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Infrastructure.SqlServer.Migrations
{
    public partial class CreateStatusMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_STATUS",
                schema: "admin",
                columns: table => new
                {
                    STTS_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STTS_DESC = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STATUS", x => x.STTS_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_STATUS",
                schema: "admin");
        }
    }
}
