using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Many_To_Many_Relationship.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KitapYazar_Kitaplar_KitaplarId",
                table: "KitapYazar");

            migrationBuilder.DropForeignKey(
                name: "FK_KitapYazar_Yazarlar_YazarlarId",
                table: "KitapYazar");

            migrationBuilder.RenameColumn(
                name: "YazarlarId",
                table: "KitapYazar",
                newName: "YazarId");

            migrationBuilder.RenameColumn(
                name: "KitaplarId",
                table: "KitapYazar",
                newName: "KitapId");

            migrationBuilder.RenameIndex(
                name: "IX_KitapYazar_YazarlarId",
                table: "KitapYazar",
                newName: "IX_KitapYazar_YazarId");

            migrationBuilder.AddForeignKey(
                name: "FK_KitapYazar_Kitaplar_KitapId",
                table: "KitapYazar",
                column: "KitapId",
                principalTable: "Kitaplar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KitapYazar_Yazarlar_YazarId",
                table: "KitapYazar",
                column: "YazarId",
                principalTable: "Yazarlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KitapYazar_Kitaplar_KitapId",
                table: "KitapYazar");

            migrationBuilder.DropForeignKey(
                name: "FK_KitapYazar_Yazarlar_YazarId",
                table: "KitapYazar");

            migrationBuilder.RenameColumn(
                name: "YazarId",
                table: "KitapYazar",
                newName: "YazarlarId");

            migrationBuilder.RenameColumn(
                name: "KitapId",
                table: "KitapYazar",
                newName: "KitaplarId");

            migrationBuilder.RenameIndex(
                name: "IX_KitapYazar_YazarId",
                table: "KitapYazar",
                newName: "IX_KitapYazar_YazarlarId");

            migrationBuilder.AddForeignKey(
                name: "FK_KitapYazar_Kitaplar_KitaplarId",
                table: "KitapYazar",
                column: "KitaplarId",
                principalTable: "Kitaplar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KitapYazar_Yazarlar_YazarlarId",
                table: "KitapYazar",
                column: "YazarlarId",
                principalTable: "Yazarlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
