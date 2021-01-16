using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.API.Infrastructure.Persistence.Migrations
{
    public partial class AssignIssueToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssigneeId",
                table: "Issues",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssigneeId",
                table: "Issues",
                column: "AssigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AspNetUsers_AssigneeId",
                table: "Issues",
                column: "AssigneeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AspNetUsers_AssigneeId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_AssigneeId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "Issues");
        }
    }
}
