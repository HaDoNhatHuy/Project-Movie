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
            migrationBuilder.AlterColumn<string>(
                name: "MovieLink",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3453C2B8-58C8-439C-927F-BBB7B42A4C37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6394b270-ff80-4f10-b2d3-3d3b6eafa773", "AQAAAAIAAYagAAAAEDTqHbdn0/HHmulvAPJK07DyP1wiLl6pKv8u2UYdjO3JFclTqSrypMhKdlKdOIsX5g==", "252f8fdb-b83d-4ff9-855f-57c8558166c2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MovieLink",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3453C2B8-58C8-439C-927F-BBB7B42A4C37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a9f68bf-e6a0-4d78-b457-46e05b9e3a48", "AQAAAAIAAYagAAAAEKGSbaCEbRjYyvuIiBrCy7ous5R9BD6hRrpVlbIAF2lDYm9gzyQaAU253fnXvClKGg==", "faf5be35-2cb5-484f-9d4d-6c1ec87e102c" });
        }
    }
}
