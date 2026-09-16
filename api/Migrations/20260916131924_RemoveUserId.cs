using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMovement_Purchase_PurchaseId",
                table: "StockMovement");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StockMovement");

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseId",
                table: "StockMovement",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovement_Purchase_PurchaseId",
                table: "StockMovement",
                column: "PurchaseId",
                principalTable: "Purchase",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMovement_Purchase_PurchaseId",
                table: "StockMovement");

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseId",
                table: "StockMovement",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "StockMovement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovement_Purchase_PurchaseId",
                table: "StockMovement",
                column: "PurchaseId",
                principalTable: "Purchase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
