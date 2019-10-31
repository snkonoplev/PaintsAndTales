using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaintsAndTales.Model.Migrations
{
    public partial class Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product_sizes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    deleted = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    size = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_sizes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    deleted = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "prices",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    deleted = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    product_id = table.Column<int>(type: "int(11)", nullable: false),
                    product_size_id = table.Column<int>(type: "int(11)", nullable: false),
                    value = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prices", x => x.id);
                    table.ForeignKey(
                        name: "FK_prices_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_prices_product_sizes_product_size_id",
                        column: x => x.product_size_id,
                        principalTable: "product_sizes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    deleted = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    product_id = table.Column<int>(type: "int(11)", nullable: true),
                    color_id = table.Column<int>(type: "int(11)", nullable: true),
                    is_title_image = table.Column<bool>(type: "bit", nullable: false),
                    file_name = table.Column<string>(type: "varchar(100)", nullable: false),
                    file_extension = table.Column<string>(type: "varchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.id);
                    table.ForeignKey(
                        name: "FK_images_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "colors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    deleted = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    image_id = table.Column<int>(type: "int(11)", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    color_code = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_colors", x => x.id);
                    table.ForeignKey(
                        name: "FK_colors_images_image_id",
                        column: x => x.image_id,
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_colors_image_id",
                table: "colors",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "IX_images_color_id",
                table: "images",
                column: "color_id");

            migrationBuilder.CreateIndex(
                name: "IX_images_product_id",
                table: "images",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_prices_product_id",
                table: "prices",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_prices_product_size_id",
                table: "prices",
                column: "product_size_id");

            migrationBuilder.AddForeignKey(
                name: "FK_images_colors_color_id",
                table: "images",
                column: "color_id",
                principalTable: "colors",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_colors_images_image_id",
                table: "colors");

            migrationBuilder.DropTable(
                name: "prices");

            migrationBuilder.DropTable(
                name: "product_sizes");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "colors");

            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
