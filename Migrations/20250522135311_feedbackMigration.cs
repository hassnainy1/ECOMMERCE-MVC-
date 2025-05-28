using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce_Website.Migrations
{
    /// <inheritdoc />
    public partial class feedbackMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_feedback",
                columns: table => new
                {
                    feedback_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_feedback", x => x.feedback_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_feedback");
        }
    }
}
