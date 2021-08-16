using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Infrastructure.SqlServer.Migrations
{
    public partial class CreatePublicIdForContents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CTNT_PUBID",
                schema: "contents",
                table: "TBL_CONTENTS",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CTNT_PUBID",
                schema: "contents",
                table: "TBL_CONTENTS");
        }
    }
}
