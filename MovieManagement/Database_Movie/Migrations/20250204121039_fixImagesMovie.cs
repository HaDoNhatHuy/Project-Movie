using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_Movie.Migrations
{
    /// <inheritdoc />
    public partial class fixImagesMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Movie",
                newName: "PrimaryImage");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryImage",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3453C2B8-58C8-439C-927F-BBB7B42A4C37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ac0d156-ad5d-4e43-8b88-6afa4775e917", "AQAAAAIAAYagAAAAED6bp5DSoC8hk7W/P7Mc5wkue/1nGJ1N46QY5FVVCL54HwF4xF/MEi4uPbotAOwOrQ==", "f48da18e-1097-4f7e-969a-66429b97a306" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrimaryImage",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "PrimaryImage",
                table: "Movie",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3453C2B8-58C8-439C-927F-BBB7B42A4C37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ec72e68-c7fb-418c-beb8-801140e81848", "AQAAAAIAAYagAAAAEEVgofvI4NyeeHiGtORb1AGN9c6GTKX/MypLu3plaO4XGSSZCrFbVFLzWMrBf6BlVA==", "079cc859-694b-479b-afec-8632e301c667" });
        }
    }
}
