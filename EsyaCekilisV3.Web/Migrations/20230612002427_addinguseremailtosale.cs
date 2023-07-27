using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsyaCekilisV3.Web.Migrations
{
    /// <inheritdoc />
    public partial class addinguseremailtosale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "SaleModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "SaleModels");
        }
    }
}
