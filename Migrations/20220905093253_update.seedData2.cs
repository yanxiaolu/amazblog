using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amazBlog.Migrations
{
    public partial class updateseedData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Blog",
                columns: new[] { "Id", "Description", "Name", "ShortName" },
                values: new object[] { 1, "Made amazing", "amazBlog", "AMZ" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blog",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
