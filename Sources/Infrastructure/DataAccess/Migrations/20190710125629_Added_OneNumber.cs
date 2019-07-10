using Microsoft.EntityFrameworkCore.Migrations;

namespace Mmu.Mls3.WebApi.Migrations
{
    public partial class Added_OneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OneNumber",
                schema: "Core",
                table: "Fact",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OneNumber",
                schema: "Core",
                table: "Fact");
        }
    }
}
