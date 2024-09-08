using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryControl.API.Migrations
{
    /// <inheritdoc />
    public partial class v101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CotegoryId",
                table: "Book");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CotegoryId",
                table: "Book",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
