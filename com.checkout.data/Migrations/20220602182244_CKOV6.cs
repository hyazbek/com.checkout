using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace com.checkout.data.Migrations
{
    public partial class CKOV6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentRequests");

            migrationBuilder.DeleteData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: new Guid("20f96956-e5df-4bf8-ac24-65c9f76fbe16"));

            migrationBuilder.DeleteData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: new Guid("4fa68f3b-d4e7-4245-8786-31e8a66566fa"));

            migrationBuilder.DeleteData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: new Guid("88703fb9-aa3b-4718-8a73-21a4eb5e7f6c"));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "PaymentRequests",
                columns: table => new
                {
                    PaymentRequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardDetailsID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Bank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyID = table.Column<int>(type: "int", nullable: false),
                    MerchantID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRequests", x => x.PaymentRequestID);
                    table.ForeignKey(
                        name: "FK_PaymentRequests_Cards_CardDetailsID",
                        column: x => x.CardDetailsID,
                        principalTable: "Cards",
                        principalColumn: "CardDetailsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[] { new Guid("20f96956-e5df-4bf8-ac24-65c9f76fbe16"), "UK", "Farfetch" });

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[] { new Guid("4fa68f3b-d4e7-4245-8786-31e8a66566fa"), "USA", "Github" });

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[] { new Guid("88703fb9-aa3b-4718-8a73-21a4eb5e7f6c"), "USA", "Amazon" });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_CardDetailsID",
                table: "PaymentRequests",
                column: "CardDetailsID");
        }
    }
}
