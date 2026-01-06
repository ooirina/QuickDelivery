using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickDelivery.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateComandaTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Statut",
                table: "Comanda",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Statut",
                table: "Comanda");
        }
    }
}
