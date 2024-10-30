using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseWorkBDV.Migrations
{
    /// <inheritdoc />
    public partial class revert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalIndex",
                table: "Trains");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocalIndex",
                table: "Trains",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
