using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GuitarShopApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsHome = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "Guitar", "guitar" },
                    { 2, "Amplifier", "amplifier" },
                    { 3, "Pedal", "pedal" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Image", "IsHome", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Harkening back to the early '90s when import Jackson® guitars were manufactured exclusively in Japan, we introduce the all-new Jackson MJ Series - an exciting and innovative collection of instruments attentively crafted in Japan. The MJ Series blends Jackson's world-renowned legacy of designing high-performance instruments with an assortment of top-tier features at a competitive price point.", "1.png", false, "Jackson RR24", 4500.0 },
                    { 2, 1, "The Dean Dimebag CHF Electric Guitar puts Dime's favorite body design behind a stunning graphic. The sleek, set mahogany neck with pau ferro fretboard is designed for speed, while the Seymour Duncan SH13 Dimebucker and  DMT Design neck humbuckers deliver all the high-output sonics you'll need. It also features dot inlays, classic V headstock shape, 24-3/4' scale, and Grover tuners. The Floyd Rose Special bridge will keep you in fine dive bombing form.", "4.png", true, "Dean Dimebag CHF", 3000.0 },
                    { 3, 1, "Gibson and Slash are proud to present the Slash Collection Gibson Les Paul™ Standard. It represents influential Gibson guitars Slash has used during his career, inspiring multiple generations of players around the world. The Slash Collection of Gibson guitars can be seen live on stage with Slash today.", "2.png", true, "Gibson Slash Les Paul", 6000.0 },
                    { 4, 1, "Created by hand, one at a time by the artisans of the ESP Custom Shop in Japan, the ESP Alexi Hexed is the identical model designed and played by one of the most beloved and influential figures in metal music: Alexi Laiho of Children of Bodom/Bodom After Midnight. The ESP Alexi Hexed is offered in an offset V shape with neck-thru-body construction at 25.5” scale.", "3.png", true, "ESP Alexi Hexed", 4000.0 },
                    { 5, 1, "Guitar virtuoso Jake E Lee blazed trails in the 1980s with Ratt and Rough Cutt before landing his legendary gig as Ozzy Osbourne's lead guitarist. His acclaimed career continued with Badlands and now Red Dragon Cartel, and Charvel has been there every step of the way. Charvel honors the dynamic guitarist with the new Jake E Lee Signature Pro-Mod So-Cal, based on the distinctive white 'Charvel-ized' instrument he acquired back in 1975.", "5.png", false, "Charvel HSS HT RW", 5000.0 },
                    { 6, 2, "The Marshall MG30GFX Gold 30W Guitar Combo features the iconic 'gold' front panel design and delivers 30 watts of portable Marshall tone with added sound effects and reverb. It is the ideal amplifier for band rehearsals and for small/medium gigs, with some added features making it perfect for practice.", "6.png", false, "Marshall MG30GFX 30W", 1500.0 },
                    { 7, 3, "The Boss TR-2 Tremolo Guitar effects pedal delivers a vintage-style tremolo that can be fully controlled thanks to its intuitive controls. A designated Wave, Rate, and depth dial ensure every parameter can be intuitively altered to the performer's preference", "7.png", false, "Boss TR-2 Tremolo Pedal", 1000.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Url",
                table: "Categories",
                column: "Url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
