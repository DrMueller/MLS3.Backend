﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Mmu.Mls3.WebApi.Migrations
{
    public partial class Removed_TestStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnotherString",
                schema: "Core",
                table: "LearningSession");

            migrationBuilder.DropColumn(
                name: "OneNumber",
                schema: "Core",
                table: "Fact");

            migrationBuilder.AlterColumn<string>(
                name: "SessionName",
                schema: "Core",
                table: "LearningSession",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "OneString",
                schema: "Core",
                table: "LearningSession",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SessionName",
                schema: "Core",
                table: "LearningSession",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "OneString",
                schema: "Core",
                table: "LearningSession",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnotherString",
                schema: "Core",
                table: "LearningSession",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OneNumber",
                schema: "Core",
                table: "Fact",
                nullable: false,
                defaultValue: 0);
        }
    }
}