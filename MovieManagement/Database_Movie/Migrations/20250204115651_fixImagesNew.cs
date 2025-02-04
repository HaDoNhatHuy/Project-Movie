using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_Movie.Migrations
{
    /// <inheritdoc />
    public partial class fixImagesNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "NewsImage");

            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "MovieImage");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "NewsImage",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "MovieImage",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3453C2B8-58C8-439C-927F-BBB7B42A4C37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ec72e68-c7fb-418c-beb8-801140e81848", "AQAAAAIAAYagAAAAEEVgofvI4NyeeHiGtORb1AGN9c6GTKX/MypLu3plaO4XGSSZCrFbVFLzWMrBf6BlVA==", "079cc859-694b-479b-afec-8632e301c667" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "NewsImage");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "MovieImage");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "NewsImage",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "MovieImage",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3453C2B8-58C8-439C-927F-BBB7B42A4C37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3818e8b7-6b85-43f0-a080-eeafdac125ad", "AQAAAAIAAYagAAAAEMtxpUtGvqmO9cmGq344z940zEX/h+Qj7NOJZT6MMNQc/Rt6sub5y3CKV/vAc6RhUg==", "2f5a9936-ef34-4027-83a2-c95f3b9fe282" });
        }
    }
}
