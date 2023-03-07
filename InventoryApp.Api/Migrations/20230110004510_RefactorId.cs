using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class RefactorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceDetails",
                table: "InvoiceDetails");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "Invoices",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "InvoiceDetails",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceDetails",
                table: "InvoiceDetails",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_ProductId",
                table: "InvoiceDetails",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceDetails",
                table: "InvoiceDetails");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDetails_ProductId",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "InvoiceDetails");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Invoices",
                newName: "InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceDetails",
                table: "InvoiceDetails",
                columns: new[] { "ProductId", "InvoiceId" });
        }
    }
}
