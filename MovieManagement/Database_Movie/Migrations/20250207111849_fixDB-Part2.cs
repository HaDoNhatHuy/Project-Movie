using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_Movie.Migrations
{
    /// <inheritdoc />
    public partial class fixDBPart2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Trailer_TrailerId",
                table: "Movie");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Trailer_TrailerId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_TrailerId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_Movie_TrailerId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "TrailerId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "TrailerId",
                table: "Movie");

            migrationBuilder.AddColumn<string>(
                name: "TrailerLink",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3453C2B8-58C8-439C-927F-BBB7B42A4C37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "921e8813-de33-4723-af66-5fa887952611", "AQAAAAIAAYagAAAAEDtWC4/G7dHn8GdsqGKt7JaJlNsLY+y3zNwESLoPQ/B9mKUqwnwhlhCYDfsjuH2RJQ==", "baba0ca0-3c38-4673-bfb9-22aa2a0fd972" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrailerLink",
                table: "Movie");

            migrationBuilder.AddColumn<Guid>(
                name: "TrailerId",
                table: "News",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TrailerId",
                table: "Movie",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3453C2B8-58C8-439C-927F-BBB7B42A4C37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "167e11f4-00a4-40d6-a93e-33c0ca86cd9c", "AQAAAAIAAYagAAAAEMXKO0qAGJUcZ4ane7WW/LZp7oZ6ku/AS5kOOzdM0WxP3ED4jd/od4aQBcXslkZoeQ==", "ad3dc200-f36f-4396-96c3-67284b5eec8d" });

            migrationBuilder.CreateIndex(
                name: "IX_News_TrailerId",
                table: "News",
                column: "TrailerId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_TrailerId",
                table: "Movie",
                column: "TrailerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Trailer_TrailerId",
                table: "Movie",
                column: "TrailerId",
                principalTable: "Trailer",
                principalColumn: "TrailerId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Trailer_TrailerId",
                table: "News",
                column: "TrailerId",
                principalTable: "Trailer",
                principalColumn: "TrailerId");
        }
    }
}
