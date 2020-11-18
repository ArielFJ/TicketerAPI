using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketerAPI.Migrations
{
    public partial class TicketTableHasClienteForeign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ClienteId",
                table: "Tickets",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Clientes_ClienteId",
                table: "Tickets",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Clientes_ClienteId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ClienteId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Tickets");
        }
    }
}
