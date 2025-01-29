using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dealership.Migrations
{
    /// <inheritdoc />
    public partial class AddUserActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roles",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "UserActivities",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ActivityType",
                table: "UserActivities",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "ActivityTime",
                table: "UserActivities",
                newName: "Action");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserActivities",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "UserActivities",
                newName: "ActivityType");

            migrationBuilder.RenameColumn(
                name: "Action",
                table: "UserActivities",
                newName: "ActivityTime");

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
