using Microsoft.EntityFrameworkCore.Migrations;

namespace ClippingKKModel.Migrations
{
    public partial class sha256 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DataId",
                table: "Clippings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataId",
                table: "Clippings");
        }
    }
}
