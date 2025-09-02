using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Personal.Shopping.Services.Order.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserIdFieldOrderLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OrderLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderLogs");
        }
    }
}
