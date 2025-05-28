using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce_Website.Migrations
{
    /// <inheritdoc />
    public partial class cartMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_cart",
                columns: table => new
                {
                    cart_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pro_id = table.Column<int>(type: "int", nullable: false),
                    Cust_id = table.Column<int>(type: "int", nullable: false),
                    product_Quantity = table.Column<int>(type: "int", nullable: false),
                    cart_status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_cart", x => x.cart_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_cart");
        }
    }
}
