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
                name: "colors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    deleted = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    color_code = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_colors", x => x.id);
                });

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
                name: "product_colors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    deleted = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    product_id = table.Column<int>(type: "int(11)", nullable: false),
                    color_id = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_colors", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_colors_colors_color_id",
                        column: x => x.color_id,
                        principalTable: "colors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_product_colors_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product_images",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    deleted = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    product_id = table.Column<int>(type: "int(11)", nullable: false),
                    is_title_image = table.Column<bool>(type: "bit", nullable: false),
                    file_name = table.Column<string>(type: "varchar(100)", nullable: false),
                    file_extension = table.Column<string>(type: "varchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_images", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_images_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_prices_product_id",
                table: "prices",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_prices_product_size_id",
                table: "prices",
                column: "product_size_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_colors_color_id",
                table: "product_colors",
                column: "color_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_colors_product_id",
                table: "product_colors",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_images_product_id",
                table: "product_images",
                column: "product_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prices");

            migrationBuilder.DropTable(
                name: "product_colors");

            migrationBuilder.DropTable(
                name: "product_images");

            migrationBuilder.DropTable(
                name: "product_sizes");

            migrationBuilder.DropTable(
                name: "colors");

            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
