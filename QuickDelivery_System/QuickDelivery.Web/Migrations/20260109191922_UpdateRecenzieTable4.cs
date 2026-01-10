using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickDelivery.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRecenzieTable4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comanda_Client_ClientId",
                table: "Comanda");

            migrationBuilder.DropForeignKey(
                name: "FK_Comanda_Produs_ProdusId",
                table: "Comanda");

            migrationBuilder.DropForeignKey(
                name: "FK_Comanda_Restaurant_RestaurantId",
                table: "Comanda");

            migrationBuilder.DropForeignKey(
                name: "FK_Produs_Categorie_CategorieId",
                table: "Produs");

            migrationBuilder.DropForeignKey(
                name: "FK_Produs_Restaurant_RestaurantId",
                table: "Produs");

            migrationBuilder.DropForeignKey(
                name: "FK_Recenzii_Restaurant_RestaurantId",
                table: "Recenzii");

            migrationBuilder.DropTable(
                name: "DetaliiComanda");

            migrationBuilder.DropColumn(
                name: "Statut",
                table: "Comanda");

            migrationBuilder.DropColumn(
                name: "TotalPlata",
                table: "Comanda");

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "Recenzii",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Recenzii",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Comanda",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AdresaLivrare",
                table: "Comanda",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Cantitate",
                table: "Comanda",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recenzii_ClientId",
                table: "Recenzii",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comanda_Client_ClientId",
                table: "Comanda",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comanda_Produs_ProdusId",
                table: "Comanda",
                column: "ProdusId",
                principalTable: "Produs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comanda_Restaurant_RestaurantId",
                table: "Comanda",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produs_Categorie_CategorieId",
                table: "Produs",
                column: "CategorieId",
                principalTable: "Categorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produs_Restaurant_RestaurantId",
                table: "Produs",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recenzii_Client_ClientId",
                table: "Recenzii",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recenzii_Restaurant_RestaurantId",
                table: "Recenzii",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comanda_Client_ClientId",
                table: "Comanda");

            migrationBuilder.DropForeignKey(
                name: "FK_Comanda_Produs_ProdusId",
                table: "Comanda");

            migrationBuilder.DropForeignKey(
                name: "FK_Comanda_Restaurant_RestaurantId",
                table: "Comanda");

            migrationBuilder.DropForeignKey(
                name: "FK_Produs_Categorie_CategorieId",
                table: "Produs");

            migrationBuilder.DropForeignKey(
                name: "FK_Produs_Restaurant_RestaurantId",
                table: "Produs");

            migrationBuilder.DropForeignKey(
                name: "FK_Recenzii_Client_ClientId",
                table: "Recenzii");

            migrationBuilder.DropForeignKey(
                name: "FK_Recenzii_Restaurant_RestaurantId",
                table: "Recenzii");

            migrationBuilder.DropIndex(
                name: "IX_Recenzii_ClientId",
                table: "Recenzii");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Recenzii");

            migrationBuilder.DropColumn(
                name: "AdresaLivrare",
                table: "Comanda");

            migrationBuilder.DropColumn(
                name: "Cantitate",
                table: "Comanda");

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "Recenzii",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Comanda",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Statut",
                table: "Comanda",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPlata",
                table: "Comanda",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "DetaliiComanda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComandaId = table.Column<int>(type: "int", nullable: false),
                    ProdusId = table.Column<int>(type: "int", nullable: false),
                    Cantitate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetaliiComanda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetaliiComanda_Comanda_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comanda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetaliiComanda_Produs_ProdusId",
                        column: x => x.ProdusId,
                        principalTable: "Produs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetaliiComanda_ComandaId",
                table: "DetaliiComanda",
                column: "ComandaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetaliiComanda_ProdusId",
                table: "DetaliiComanda",
                column: "ProdusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comanda_Client_ClientId",
                table: "Comanda",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Produs_Categorie_CategorieId",
                table: "Produs",
                column: "CategorieId",
                principalTable: "Categorie",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Produs_Restaurant_RestaurantId",
                table: "Produs",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recenzii_Restaurant_RestaurantId",
                table: "Recenzii",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
