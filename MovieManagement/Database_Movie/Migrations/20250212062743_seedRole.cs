using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Database_Movie.Migrations
{
    /// <inheritdoc />
    public partial class seedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1335b967-3b79-4966-b5e1-c6876c214208", null, "delete-comments", "DELETE-COMMENTS" },
                    { "9d158b8e-0c8e-4168-8d22-ef4432d3532f", null, "view-movies", "VIEW-MOVIES" },
                    { "a4538b26-d0f1-48b4-9404-555ae8bca8de", null, "edit-news", "EDIT-NEWS" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3453C2B8-58C8-439C-927F-BBB7B42A4C37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e375dc4f-2d9c-47d1-8bde-c0105ecf6f5d", "AQAAAAIAAYagAAAAEPt78CqxLQuWqTA1YLjfDStQKrxB1yClSbQiesbmaANRaPBVeKYJ5dEAT8OLrsIUiA==", "0c2ef7b2-ad4c-4a83-ae61-caac175788af" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "1335b967-3b79-4966-b5e1-c6876c214208");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9d158b8e-0c8e-4168-8d22-ef4432d3532f");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a4538b26-d0f1-48b4-9404-555ae8bca8de");

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3453C2B8-58C8-439C-927F-BBB7B42A4C37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleId", "SecurityStamp", "Token" },
                values: new object[] { "e6cd6475-bc1c-4443-9133-68dbc63df466", "AQAAAAIAAYagAAAAEFDNDuSYahYHC6JdDZTSZvnugbUe3xQTsFum+GvABj/TBbPc8/UC+38AF5EmUiTmKQ==", null, "145747ca-48ea-4656-b193-fdfc1ee05477", null });
        }
    }
}
