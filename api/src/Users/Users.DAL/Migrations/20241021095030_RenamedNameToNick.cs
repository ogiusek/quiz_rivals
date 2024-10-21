using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenamedNameToNick : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PhotoPath",
                table: "users",
                newName: "photo_path");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "users",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "users",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "users",
                newName: "nick");

            migrationBuilder.RenameIndex(
                name: "IX_users_Name",
                table: "users",
                newName: "IX_users_nick");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email",
                table: "users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "photo_path",
                table: "users",
                newName: "PhotoPath");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "nick",
                table: "users",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_users_nick",
                table: "users",
                newName: "IX_users_Name");
        }
    }
}
