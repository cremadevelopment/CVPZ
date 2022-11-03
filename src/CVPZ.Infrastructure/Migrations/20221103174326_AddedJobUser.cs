using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVPZ.Infrastructure.Migrations
{
    public partial class AddedJobUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Jobs");
        }
    }
}
