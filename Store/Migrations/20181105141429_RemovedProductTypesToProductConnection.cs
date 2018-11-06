using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class RemovedProductTypesToProductConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
