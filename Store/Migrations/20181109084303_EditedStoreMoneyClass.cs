using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class EditedStoreMoneyClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StoreMoney",
                table: "StoreMoney");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StoreMoney",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoreMoney",
                table: "StoreMoney",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StoreMoney",
                table: "StoreMoney");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StoreMoney");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoreMoney",
                table: "StoreMoney",
                column: "StoreCashSupply");
        }
    }
}
