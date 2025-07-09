using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Personal.Shopping.Services.ShoppingCart.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCartHeaderNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_CartHeaders_CartHeaderId",
                table: "CartDetails");

            migrationBuilder.DropIndex(
                name: "IX_CartDetails_CartHeaderId",
                table: "CartDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_CartHeaderId",
                table: "CartDetails",
                column: "CartHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_CartHeaders_CartHeaderId",
                table: "CartDetails",
                column: "CartHeaderId",
                principalTable: "CartHeaders",
                principalColumn: "CartHeaderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
