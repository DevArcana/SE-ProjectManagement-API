using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.API.Infrastructure.Persistence.Migrations
{
    public partial class UserProjectAccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProjectAccess",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProjectAccess", x => new { x.UserId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_UserProjectAccess_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProjectAccess_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectAccess_ProjectId",
                table: "UserProjectAccess",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProjectAccess");
        }
    }
}
