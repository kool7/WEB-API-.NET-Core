using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class seedMigrationData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "Id", "HowTo", "Line", "Platform" },
                values: new object[] { 1, "How to create migrations in .NET Core", "Add-Migration MigrationName", "EF Core" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
