using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_Movie.Migrations
{
    /// <inheritdoc />
    public partial class addGroupRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupRole_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3453C2B8-58C8-439C-927F-BBB7B42A4C37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e6cd6475-bc1c-4443-9133-68dbc63df466", "AQAAAAIAAYagAAAAEFDNDuSYahYHC6JdDZTSZvnugbUe3xQTsFum+GvABj/TBbPc8/UC+38AF5EmUiTmKQ==", "145747ca-48ea-4656-b193-fdfc1ee05477" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupRole_GroupId",
                table: "GroupRole",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupRole_RoleId",
                table: "GroupRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupRole");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3453C2B8-58C8-439C-927F-BBB7B42A4C37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6588d9fb-76f8-479d-888d-f2934b94c6d6", "AQAAAAIAAYagAAAAELaU4lUVKYwDOCfsz7ihM/sBko1npr7M0HDITaTGRuiFVKrinjTX+E7taufbAJIH+g==", "60387e4e-6367-4016-ad7f-b6642df139e3" });
        }
    }
}
