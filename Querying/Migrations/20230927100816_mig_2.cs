using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Querying.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Parcalar",
                table: "Parcalar");

            migrationBuilder.AddColumn<int>(
                name: "UrunId",
                table: "Parcalar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UrunDetay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fiyat = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunDetay", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parcalar_UrunId",
                table: "Parcalar",
                column: "UrunId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcalar_Urunler_UrunId",
                table: "Parcalar",
                column: "UrunId",
                principalTable: "Urunler",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcalar_Urunler_UrunId",
                table: "Parcalar");

            migrationBuilder.DropTable(
                name: "UrunDetay");

            migrationBuilder.DropIndex(
                name: "IX_Parcalar_UrunId",
                table: "Parcalar");

            migrationBuilder.DropColumn(
                name: "UrunId",
                table: "Parcalar");

            migrationBuilder.AddColumn<string>(
                name: "Parcalar",
                table: "Parcalar",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
