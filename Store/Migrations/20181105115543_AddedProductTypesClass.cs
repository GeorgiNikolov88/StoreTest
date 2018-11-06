using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class AddedProductTypesClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductTypePropertyId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    PropertyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PropertyName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.PropertyId);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypePropertyId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductTypePropertyId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductTypePropertyId",
                table: "Products");
        }
    }
}
