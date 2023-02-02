using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OperationTable.Migrations
{
    /// <inheritdoc />
    public partial class operationWork3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ItemTables",
                newName: "ItemID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemID",
                table: "ItemTables",
                newName: "Id");
        }
    }
}
