﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mmu.Mls3.WebApi.Migrations
{
    public partial class _1234_Added_SessionCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SessionName",
                schema: "Core",
                table: "LearningSession",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "SessionCategory",
                schema: "Core",
                table: "LearningSession",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "QuestionText",
                schema: "Core",
                table: "Fact",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "AnswerText",
                schema: "Core",
                table: "Fact",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionCategory",
                schema: "Core",
                table: "LearningSession");

            migrationBuilder.AlterColumn<string>(
                name: "SessionName",
                schema: "Core",
                table: "LearningSession",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "QuestionText",
                schema: "Core",
                table: "Fact",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "AnswerText",
                schema: "Core",
                table: "Fact",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
