using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mmu.Mls3.WebApi.Infrastructure.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LearningSessionFact",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "AppUser",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Fact",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "LearningSession",
                schema: "Core");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Core");

            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.CreateTable(
                name: "Fact",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerText = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    QuestionText = table.Column<string>(nullable: false)
                },
                constraints: table =>
                             {
                                 table.PrimaryKey("PK_Fact", x => x.Id);
                             });

            migrationBuilder.CreateTable(
                name: "LearningSession",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SessionName = table.Column<string>(maxLength: 256, nullable: false),
                    OneString = table.Column<string>(nullable: true)
                },
                constraints: table =>
                             {
                                 table.PrimaryKey("PK_LearningSession", x => x.Id);
                             });

            migrationBuilder.CreateTable(
                name: "AppUser",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HashedPassword = table.Column<string>(maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                             {
                                 table.PrimaryKey("PK_AppUser", x => x.Id);
                             });

            migrationBuilder.CreateTable(
                name: "LearningSessionFact",
                schema: "Core",
                columns: table => new { FactId = table.Column<long>(nullable: false), LearningSessionId = table.Column<long>(nullable: false) },
                constraints: table =>
                             {
                                 table.PrimaryKey("PK_LearningSessionFact", x => new { x.FactId, x.LearningSessionId });
                                 table.ForeignKey(
                                     name: "FK_LearningSessionFact_Fact_FactId",
                                     column: x => x.FactId,
                                     principalSchema: "Core",
                                     principalTable: "Fact",
                                     principalColumn: "Id",
                                     onDelete: ReferentialAction.Cascade);
                                 table.ForeignKey(
                                     name: "FK_LearningSessionFact_LearningSession_LearningSessionId",
                                     column: x => x.LearningSessionId,
                                     principalSchema: "Core",
                                     principalTable: "LearningSession",
                                     principalColumn: "Id",
                                     onDelete: ReferentialAction.Cascade);
                             });

            migrationBuilder.CreateIndex(
                name: "IX_LearningSessionFact_LearningSessionId",
                schema: "Core",
                table: "LearningSessionFact",
                column: "LearningSessionId");
        }
    }
}