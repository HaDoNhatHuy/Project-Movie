using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_Movie.Migrations
{
    /// <inheritdoc />
    public partial class fixDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Movie",
                newName: "Age");

            migrationBuilder.AddColumn<string>(
                name: "Quality",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Movie",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3453C2B8-58C8-439C-927F-BBB7B42A4C37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "167e11f4-00a4-40d6-a93e-33c0ca86cd9c", "AQAAAAIAAYagAAAAEMXKO0qAGJUcZ4ane7WW/LZp7oZ6ku/AS5kOOzdM0WxP3ED4jd/od4aQBcXslkZoeQ==", "ad3dc200-f36f-4396-96c3-67284b5eec8d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quality",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Movie");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Movie",
                newName: "Year");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3453C2B8-58C8-439C-927F-BBB7B42A4C37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e24148ff-9e3d-47a7-ac2d-e3f4d38355d4", "AQAAAAIAAYagAAAAEBai90U+YKr+yuCi0osu9/Z3gqrH4wPhzYUGAnXQWeaX/x2NYpaekx5SqU2Zak59cQ==", "d3d19eeb-a73b-4dfc-b176-a369e0317589" });
        }
    }
}
