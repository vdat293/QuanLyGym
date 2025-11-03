using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongGym.Migrations
{
    /// <inheritdoc />
    public partial class AddCCCDField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CCCD",
                table: "Members",
                type: "TEXT",
                maxLength: 12,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CCCD",
                table: "Members");
        }
    }
}
