using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameDB_recode.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Awards",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Awards",
                table: "Games");
        }
    }
}
