using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickDelivery.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriiTableNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategorieId",
                table: "Produs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Iconita = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produs_CategorieId",
                table: "Produs",
                column: "CategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produs_Categorie_CategorieId",
                table: "Produs",
                column: "CategorieId",
                principalTable: "Categorie",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produs_Categorie_CategorieId",
                table: "Produs");

            migrationBuilder.DropTable(
                name: "Categorie");

            migrationBuilder.DropIndex(
                name: "IX_Produs_CategorieId",
                table: "Produs");

            migrationBuilder.DropColumn(
                name: "CategorieId",
                table: "Produs");
        }
    }
}
