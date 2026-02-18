using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixShadowProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedAt",
                table: "Games",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "ActivityLogs",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "ActivityLogs",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "ActivityLogs",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_GameId",
                table: "ActivityLogs",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_UserId1",
                table: "ActivityLogs",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_UserId1",
                table: "ActivityLogs",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_Games_GameId",
                table: "ActivityLogs",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_UserId1",
                table: "ActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_Games_GameId",
                table: "ActivityLogs");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLogs_GameId",
                table: "ActivityLogs");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLogs_UserId1",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ActivityLogs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedAt",
                table: "Games",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "ActivityLogs",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "ActivityLogs",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }
    }
}
