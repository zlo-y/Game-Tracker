using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GameEntity",
                table: "GameEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityTime",
                table: "ActivityTime");

            migrationBuilder.RenameTable(
                name: "GameEntity",
                newName: "Games");

            migrationBuilder.RenameTable(
                name: "ActivityTime",
                newName: "ActivityLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityLogs",
                table: "ActivityLogs",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityLogs",
                table: "ActivityLogs");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "GameEntity");

            migrationBuilder.RenameTable(
                name: "ActivityLogs",
                newName: "ActivityTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameEntity",
                table: "GameEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityTime",
                table: "ActivityTime",
                column: "Id");
        }
    }
}
