using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class StoreInfoReplacedWithStoreLogAndStoreMoney : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreInfo");

            migrationBuilder.CreateTable(
                name: "StoreLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SalesLog = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreMoney",
                columns: table => new
                {
                    StoreCashSupply = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreMoney", x => x.StoreCashSupply);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreLog");

            migrationBuilder.DropTable(
                name: "StoreMoney");

            migrationBuilder.CreateTable(
                name: "StoreInfo",
                columns: table => new
                {
                    StoreCash = table.Column<decimal>(nullable: false),
                    StoreLog = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreInfo", x => x.StoreCash);
                });
        }
    }
}
