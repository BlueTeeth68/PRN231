using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarProducers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProducerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarProducers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdentityCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarModelYear = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    RentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_CarProducers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "CarProducers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarRentals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    PickupDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarRentals_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarRentals_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    ReviewStar = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarProducers",
                columns: new[] { "Id", "Address", "Country", "ProducerName" },
                values: new object[,]
                {
                    { 1, "123 Main Street", "Japan", "Toyota" },
                    { 2, "456 Park Avenue", "Italy", "Ferrari" },
                    { 3, "789 Broadway", "Germany", "Mercedes-Benz" },
                    { 4, "321 Elm Street", "United States", "Tesla" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Birthday", "CustomerName", "Email", "IdentityCard", "LicenceDate", "LicenceNumber", "Mobile", "Password" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Doe", "john.doe@example.com", "ABC123", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "XYZ987", "1234567890", "123456" },
                    { 2, new DateTime(1985, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane Smith", "jane.smith@example.com", "DEF456", new DateTime(2019, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "PQR654", "0987654321", "123456" },
                    { 3, new DateTime(1992, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alice Johnson", "alice.johnson@example.com", "GHI789", new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "LMN321", "9876543210", "123456" },
                    { 4, new DateTime(1988, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bob Williams", "bob.williams@example.com", "JKL987", new DateTime(2021, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "STU456", "4567890123", "123456" },
                    { 5, new DateTime(1995, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eva Brown", "eva.brown@example.com", "MNO654", new DateTime(2023, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "VWX789", "9012345678", "123456" },
                    { 6, new DateTime(1983, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "David Wilson", "david.wilson@example.com", "PQR321", new DateTime(2020, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "YZA987", "6543210987", "123456" },
                    { 7, new DateTime(1991, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sophia Davis", "sophia.davis@example.com", "BCD789", new DateTime(2021, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "EFG654", "8901234567", "123456" },
                    { 8, new DateTime(1986, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Michael Johnson", "michael.johnson@example.com", "HIJ987", new DateTime(2018, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "KLM321", "3210987654", "123456" },
                    { 9, new DateTime(1993, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Olivia Taylor", "olivia.taylor@example.com", "NOP654", new DateTime(2021, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "QRS987", "7654321098", "123456" },
                    { 10, new DateTime(1987, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "William Anderson", "william.anderson@example.com", "RST321", new DateTime(2022, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "UVW987", "2345678901", "123456" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Capacity", "CarModelYear", "CarName", "Color", "Description", "ImportDate", "ProducerId", "RentPrice", "Status" },
                values: new object[,]
                {
                    { 1, 5, 2020, "Toyota Camry", "Silver", "A comfortable sedan with advanced features", new DateTime(2022, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 100.00m, 0 },
                    { 2, 2, 2021, "Ferrari 488 GTB", "Red", "A powerful and luxurious sports car", new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 500.00m, 0 },
                    { 3, 5, 2019, "Mercedes-Benz E-Class", "Black", "A luxurious and elegant sedan", new DateTime(2022, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 150.00m, 0 },
                    { 4, 5, 2022, "Tesla Model S", "White", "An electric car with cutting-edge technology", new DateTime(2022, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 200.00m, 0 },
                    { 5, 5, 2021, "Toyota RAV4", "Blue", "A versatile and spacious SUV", new DateTime(2022, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 120.00m, 0 },
                    { 6, 2, 2023, "Ferrari 812 Superfast", "Yellow", "An incredibly fast and powerful supercar", new DateTime(2022, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 600.00m, 0 },
                    { 7, 5, 2020, "Mercedes-Benz GLE", "Silver", "A stylish and comfortable SUV", new DateTime(2022, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 180.00m, 0 },
                    { 8, 5, 2022, "Tesla Model 3", "Red", "A popular electric car with great range", new DateTime(2022, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 180.00m, 0 },
                    { 9, 7, 2021, "Toyota Highlander", "Gray", "A spacious and reliable SUV", new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 140.00m, 0 },
                    { 10, 2, 2023, "Ferrari F8 Tributo", "Black", "A stunning and high-performance sports car", new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 550.00m, 0 },
                    { 11, 5, 2022, "Mercedes-Benz S-Class", "White", "A flagship luxury sedan with advanced technology", new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 200.00m, 0 },
                    { 12, 7, 2022, "Tesla Model X", "Blue", "An electric SUV with falcon-wing doors", new DateTime(2023, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 220.00m, 0 },
                    { 13, 8, 2022, "Toyota Land Cruiser", "Black", "A rugged and capable off-road SUV", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 180.00m, 0 },
                    { 14, 2, 2023, "Ferrari SF90 Stradale", "Red", "A high-performance hybrid supercar", new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 700.00m, 0 },
                    { 15, 5, 2021, "Mercedes-Benz G-Class", "White", "An iconic and luxurious SUV", new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 250.00m, 0 },
                    { 16, 2, 2023, "Tesla Roadster", "Red", "An all-electric sports car with incredible speed", new DateTime(2024, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 800.00m, 0 },
                    { 17, 5, 2020, "Toyota Camry", "Gray", "A reliable and fuel-efficient sedan", new DateTime(2024, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 110.00m, 0 },
                    { 18, 2, 2023, "Ferrari 488 Pista", "Yellow", "A track-focused and high-performance supercar", new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 750.00m, 0 },
                    { 19, 5, 2022, "Mercedes-Benz CLA", "Black", "A compact and stylish luxury sedan", new DateTime(2024, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 140.00m, 0 },
                    { 20, 5, 2023, "Tesla Model S Plaid", "White", "An ultra-fast and high-performance electric car", new DateTime(2024, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 350.00m, 0 }
                });

            migrationBuilder.InsertData(
                table: "CarRentals",
                columns: new[] { "Id", "CarId", "CustomerId", "PickupDate", "RentPrice", "ReturnDate", "Status" },
                values: new object[,]
                {
                    { 1, 12, 1, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 480.00m, new DateTime(2023, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 17, 2, new DateTime(2023, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 440.00m, new DateTime(2023, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, 13, 3, new DateTime(2023, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 640.00m, new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, 19, 4, new DateTime(2023, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 900.00m, new DateTime(2023, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, 14, 1, new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 300.00m, new DateTime(2023, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 6, 11, 3, new DateTime(2023, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2700.00m, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 7, 19, 2, new DateTime(2023, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 560.00m, new DateTime(2023, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 8, 20, 4, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2000.00m, new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "CarId", "Comment", "CustomerId", "ReviewStar" },
                values: new object[,]
                {
                    { 1, 12, "The car was clean and comfortable. The overall experience was great.", 1, 4 },
                    { 2, 17, "I loved the car! It was in excellent condition, and the service provided was outstanding.", 2, 5 },
                    { 3, 13, "The car was average. It could have been cleaner, and the pickup process was a bit slow.", 3, 3 },
                    { 4, 19, "The car was reliable and comfortable. Overall, a good experience.", 4, 4 },
                    { 5, 14, "The car exceeded my expectations! It was clean, fuel-efficient, and perfect for my trip.", 1, 5 },
                    { 6, 11, "I was disappointed with the car's condition. It had several maintenance issues and was not clean.", 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_CarId_CustomerId_PickupDate_ReturnDate",
                table: "CarRentals",
                columns: new[] { "CarId", "CustomerId", "PickupDate", "ReturnDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_CustomerId",
                table: "CarRentals",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ProducerId",
                table: "Cars",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CarId",
                table: "Reviews",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId_CarId",
                table: "Reviews",
                columns: new[] { "CustomerId", "CarId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarRentals");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "CarProducers");
        }
    }
}
