using Microsoft.EntityFrameworkCore.Migrations;

namespace PaintsAndTales.Model.Migrations
{
    public partial class Colors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "color_code",
                table: "colors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "color_code",
                table: "colors",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");
        }
    }
}
