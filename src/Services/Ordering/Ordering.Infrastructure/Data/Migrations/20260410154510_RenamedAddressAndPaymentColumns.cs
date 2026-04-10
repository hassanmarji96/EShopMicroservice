using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamedAddressAndPaymentColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Payment_Cvv",
                table: "Orders",
                newName: "Payment_CVV");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_Email",
                table: "Orders",
                newName: "ShippingAddress_EmailAddress");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_AddresLine",
                table: "Orders",
                newName: "ShippingAddress_AddressLine");

            migrationBuilder.RenameColumn(
                name: "BillingAddress_Email",
                table: "Orders",
                newName: "BillingAddress_EmailAddress");

            migrationBuilder.RenameColumn(
                name: "BillingAddress_AddresLine",
                table: "Orders",
                newName: "BillingAddress_AddressLine");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Payment_CVV",
                table: "Orders",
                newName: "Payment_Cvv");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_EmailAddress",
                table: "Orders",
                newName: "ShippingAddress_Email");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_AddressLine",
                table: "Orders",
                newName: "ShippingAddress_AddresLine");

            migrationBuilder.RenameColumn(
                name: "BillingAddress_EmailAddress",
                table: "Orders",
                newName: "BillingAddress_Email");

            migrationBuilder.RenameColumn(
                name: "BillingAddress_AddressLine",
                table: "Orders",
                newName: "BillingAddress_AddresLine");
        }
    }
}
