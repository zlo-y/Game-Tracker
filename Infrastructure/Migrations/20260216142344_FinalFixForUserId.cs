using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FinalFixForUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_UserId1",
                table: "ActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_Games_GameId",
                table: "ActivityLogs");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLogs_UserId1",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ActivityLogs");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_Games_GameId",
                table: "ActivityLogs",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_Games_GameId",
                table: "ActivityLogs");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "ActivityLogs",
                type: "uuid",
                nullable: true);

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
    }
}
