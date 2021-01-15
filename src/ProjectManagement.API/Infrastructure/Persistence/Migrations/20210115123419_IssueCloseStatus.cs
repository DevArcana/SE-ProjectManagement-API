using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.API.Infrastructure.Persistence.Migrations
{
    public partial class IssueCloseStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Closed",
                table: "Issues",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Issues",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Closed",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Issues");
        }
    }
}
