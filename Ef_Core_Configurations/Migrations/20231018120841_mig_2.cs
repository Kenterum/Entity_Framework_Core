using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ef_Core_Configurations.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Entities",
                newName: "Ayirici");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ayirici",
                table: "Entities",
                newName: "Discriminator");
        }
    }
}
