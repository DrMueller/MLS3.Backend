using Microsoft.EntityFrameworkCore.Migrations;

namespace Mmu.Mls3.WebApi.Migrations
{
    public partial class Removed_LearningSessionCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionCategory",
                schema: "Core",
                table: "LearningSession");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SessionCategory",
                schema: "Core",
                table: "LearningSession",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }
    }
}
