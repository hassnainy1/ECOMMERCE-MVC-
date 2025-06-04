using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce_Website.Migrations
{
    /// <inheritdoc />
    public partial class ordermigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_tbl_cart_CartId",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "tbl_order");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CartId",
                table: "tbl_order",
                newName: "IX_tbl_order_CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_order",
                table: "tbl_order",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_order_tbl_cart_CartId",
                table: "tbl_order",
                column: "CartId",
                principalTable: "tbl_cart",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_order_tbl_cart_CartId",
                table: "tbl_order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_order",
                table: "tbl_order");

            migrationBuilder.RenameTable(
                name: "tbl_order",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_order_CartId",
                table: "Order",
                newName: "IX_Order_CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_tbl_cart_CartId",
                table: "Order",
                column: "CartId",
                principalTable: "tbl_cart",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
