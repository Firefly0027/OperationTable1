using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OperationTable.Migrations
{
    /// <inheritdoc />
    public partial class operationWork2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxAmount",
                table: "OrderDetials",
                newName: "tax");

            migrationBuilder.RenameColumn(
                name: "DiscountAmount",
                table: "OrderDetials",
                newName: "discount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tax",
                table: "OrderDetials",
                newName: "TaxAmount");

            migrationBuilder.RenameColumn(
                name: "discount",
                table: "OrderDetials",
                newName: "DiscountAmount");
        }
    }
}
