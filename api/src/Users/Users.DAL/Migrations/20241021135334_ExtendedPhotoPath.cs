using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedPhotoPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "photo_path",
                table: "users",
                type: "VARCHAR(128)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(64)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "photo_path",
                table: "users",
                type: "VARCHAR(64)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(128)");
        }
    }
}
