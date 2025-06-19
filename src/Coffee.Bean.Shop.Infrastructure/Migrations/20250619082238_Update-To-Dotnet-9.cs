using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coffee.Bean.Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateToDotnet9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CoffeeBeans_Name",
                table: "CoffeeBeans",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CoffeeBeans_Name",
                table: "CoffeeBeans");
        }
    }
}
