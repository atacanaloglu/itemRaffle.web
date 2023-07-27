using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsyaCekilisV3.Web.Migrations
{
    /// <inheritdoc />
    public partial class sonhaliforeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleModels_AspNetUsers_appUserId",
                table: "SaleModels");

            migrationBuilder.DropIndex(
                name: "IX_SaleModels_appUserId",
                table: "SaleModels");

            migrationBuilder.DropColumn(
                name: "appUserId",
                table: "SaleModels");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SaleModels",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "SaleModels",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "appUserId",
                table: "SaleModels",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleModels_appUserId",
                table: "SaleModels",
                column: "appUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleModels_AspNetUsers_appUserId",
                table: "SaleModels",
                column: "appUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
