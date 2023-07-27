using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsyaCekilisV3.Web.Migrations
{
    /// <inheritdoc />
    public partial class productmodelownedusername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnedUser",
                table: "ProductModels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "OwnedUserName",
                table: "ProductModels",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnedUserName",
                table: "ProductModels");

            migrationBuilder.AlterColumn<string>(
                name: "OwnedUser",
                table: "ProductModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
