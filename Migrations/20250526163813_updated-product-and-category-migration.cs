using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce_Website.Migrations
{
    /// <inheritdoc />
    public partial class updatedproductandcategorymigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "cat_id",
                table: "tbl_product",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_product_cat_id",
                table: "tbl_product",
                column: "cat_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_product_tbl_category_cat_id",
                table: "tbl_product",
                column: "cat_id",
                principalTable: "tbl_category",
                principalColumn: "category_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_product_tbl_category_cat_id",
                table: "tbl_product");

            migrationBuilder.DropIndex(
                name: "IX_tbl_product_cat_id",
                table: "tbl_product");

            migrationBuilder.AlterColumn<string>(
                name: "cat_id",
                table: "tbl_product",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
