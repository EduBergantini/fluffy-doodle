using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Infrastructure.SqlServer.Migrations
{
    public partial class CreateUsersMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_USERS",
                schema: "admin",
                columns: table => new
                {
                    USER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_PUBID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    USER_FNAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    USER_EMAIL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    USER_PASSWD = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    USER_REGDATE = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    STTS_ID = table.Column<int>(type: "int", nullable: false),
                    ROLE_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.USER_ID);
                    table.ForeignKey(
                        name: "FK_USERS_ROLES",
                        column: x => x.ROLE_ID,
                        principalSchema: "admin",
                        principalTable: "TBL_ROLES",
                        principalColumn: "ROLE_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USERS_STATUS",
                        column: x => x.STTS_ID,
                        principalSchema: "admin",
                        principalTable: "TBL_STATUS",
                        principalColumn: "STTS_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_USERS_ROLE_ID",
                schema: "admin",
                table: "TBL_USERS",
                column: "ROLE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_USERS_STTS_ID",
                schema: "admin",
                table: "TBL_USERS",
                column: "STTS_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_USERS",
                schema: "admin");
        }
    }
}
