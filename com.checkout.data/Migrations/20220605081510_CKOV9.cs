using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace com.checkout.data.Migrations
{
    public partial class CKOV9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: new Guid("bcd71f3d-6b23-4fe1-927b-faa08a4b8908"));

            migrationBuilder.DeleteData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: new Guid("e2305095-8e72-4f43-ac7f-bf0d5e30a83b"));

            migrationBuilder.DeleteData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: new Guid("fac164b8-0c72-4147-9efa-4ce58c1a4dfd"));

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardDetailsID", "CardNumber", "Cvv", "ExpiryMonth", "ExpiryYear", "HolderName" },
                values: new object[] { 1, "123456789", "111", "11", "2024", "Valid Card" });

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("1c4352e9-beb6-4c7f-8bfc-9263de60238b"), "USA", "Github" },
                    { new Guid("3205a75c-259b-48a4-9eed-5bdce3c7ff18"), "UK", "Farfetch" },
                    { new Guid("836cc81c-66b7-444f-9860-850f3bcc4b88"), "USA", "Amazon" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionID", "Amount", "CardDetailsID", "CurrencyID", "MerchantID", "Status", "StatusCode" },
                values: new object[] { new Guid("ed9a5b76-b5cc-46f6-9372-7657a2812158"), 322m, 1, 1, new Guid("1c4352e9-beb6-4c7f-8bfc-9263de60238b"), "Created", "C_00001" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: new Guid("3205a75c-259b-48a4-9eed-5bdce3c7ff18"));

            migrationBuilder.DeleteData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: new Guid("836cc81c-66b7-444f-9860-850f3bcc4b88"));

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionID",
                keyValue: new Guid("ed9a5b76-b5cc-46f6-9372-7657a2812158"));

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "CardDetailsID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: new Guid("1c4352e9-beb6-4c7f-8bfc-9263de60238b"));

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[] { new Guid("bcd71f3d-6b23-4fe1-927b-faa08a4b8908"), "USA", "Amazon" });

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[] { new Guid("e2305095-8e72-4f43-ac7f-bf0d5e30a83b"), "USA", "Github" });

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[] { new Guid("fac164b8-0c72-4147-9efa-4ce58c1a4dfd"), "UK", "Farfetch" });
        }
    }
}
