using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class TryingStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypePropertyId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductTypePropertyId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductTypePropertyId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "ProductTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_ProductID",
                table: "ProductTypes",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypes_Products_ProductID",
                table: "ProductTypes",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypes_Products_ProductID",
                table: "ProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypes_ProductID",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "ProductTypes");

            migrationBuilder.AddColumn<int>(
                name: "ProductTypePropertyId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypePropertyId",
                table: "Products",
                column: "ProductTypePropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypePropertyId",
                table: "Products",
                column: "ProductTypePropertyId",
                principalTable: "ProductTypes",
                principalColumn: "PropertyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
