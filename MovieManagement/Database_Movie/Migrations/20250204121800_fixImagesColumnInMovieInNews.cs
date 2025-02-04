using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_Movie.Migrations
{
    /// <inheritdoc />
    public partial class fixImagesColumnInMovieInNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MoreImage",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MoreImage",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3453C2B8-58C8-439C-927F-BBB7B42A4C37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a9f68bf-e6a0-4d78-b457-46e05b9e3a48", "AQAAAAIAAYagAAAAEKGSbaCEbRjYyvuIiBrCy7ous5R9BD6hRrpVlbIAF2lDYm9gzyQaAU253fnXvClKGg==", "faf5be35-2cb5-484f-9d4d-6c1ec87e102c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoreImage",
                table: "News");

            migrationBuilder.DropColumn(
                name: "MoreImage",
                table: "Movie");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3453C2B8-58C8-439C-927F-BBB7B42A4C37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ac0d156-ad5d-4e43-8b88-6afa4775e917", "AQAAAAIAAYagAAAAED6bp5DSoC8hk7W/P7Mc5wkue/1nGJ1N46QY5FVVCL54HwF4xF/MEi4uPbotAOwOrQ==", "f48da18e-1097-4f7e-969a-66429b97a306" });
        }
    }
}
