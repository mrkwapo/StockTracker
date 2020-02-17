using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiUserMVC.Migrations.ApplicationDB
{
    public partial class AddStockToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(nullable: true),
                    Last_Price = table.Column<string>(nullable: true),
                    Change = table.Column<string>(nullable: true),
                    Change_Percentage = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    Market_Time = table.Column<string>(nullable: true),
                    Volume = table.Column<string>(nullable: true),
                    Average_Volume = table.Column<string>(nullable: true),
                    Market_Cap = table.Column<string>(nullable: true),
                    ScrapeDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
