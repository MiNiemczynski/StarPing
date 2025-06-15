using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarPingData.Migrations
{
    /// <inheritdoc />
    public partial class CartItemFixMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Offers_OfferId",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "OfferId",
                table: "CartItems",
                newName: "SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_OfferId",
                table: "CartItems",
                newName: "IX_CartItems_SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Subscriptions_SubscriptionId",
                table: "CartItems",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Subscriptions_SubscriptionId",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "SubscriptionId",
                table: "CartItems",
                newName: "OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_SubscriptionId",
                table: "CartItems",
                newName: "IX_CartItems_OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Offers_OfferId",
                table: "CartItems",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
