using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameExpenseNameToTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Expenses",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Expenses",
                newName: "Name");
        }
    }
}
