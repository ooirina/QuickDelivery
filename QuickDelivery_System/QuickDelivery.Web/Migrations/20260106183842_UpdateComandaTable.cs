using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickDelivery.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateComandaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProdusId",
                table: "Comanda",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Comanda",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comanda_ProdusId",
                table: "Comanda",
                column: "ProdusId");

            migrationBuilder.CreateIndex(
                name: "IX_Comanda_RestaurantId",
                table: "Comanda",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comanda_Produs_ProdusId",
                table: "Comanda",
                column: "ProdusId",
                principalTable: "Produs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comanda_Restaurant_RestaurantId",
                table: "Comanda",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comanda_Produs_ProdusId",
                table: "Comanda");

            migrationBuilder.DropForeignKey(
                name: "FK_Comanda_Restaurant_RestaurantId",
                table: "Comanda");

            migrationBuilder.DropIndex(
                name: "IX_Comanda_ProdusId",
                table: "Comanda");

            migrationBuilder.DropIndex(
                name: "IX_Comanda_RestaurantId",
                table: "Comanda");

            migrationBuilder.DropColumn(
                name: "ProdusId",
                table: "Comanda");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Comanda");
        }
    }
}
