using Microsoft.EntityFrameworkCore.Migrations;

namespace PaintsAndTales.Model.Migrations
{
    public partial class Reletions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contacts_orders_order_id",
                table: "contacts");

            migrationBuilder.DropIndex(
                name: "IX_orders_contact_id",
                table: "orders");

            migrationBuilder.CreateIndex(
                name: "IX_orders_contact_id",
                table: "orders",
                column: "contact_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_contacts_contact_id",
                table: "orders",
                column: "contact_id",
                principalTable: "contacts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_contacts_contact_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_contact_id",
                table: "orders");

            migrationBuilder.CreateIndex(
                name: "IX_orders_contact_id",
                table: "orders",
                column: "contact_id");

            migrationBuilder.AddForeignKey(
                name: "FK_contacts_orders_order_id",
                table: "contacts",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
