using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pricing",
                columns: table => new
                {
                    price_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transport_method = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    weight_range = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    price_per_kg = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    additional_service_price = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pricing", x => x.price_id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_Users_Roles",
                        column: x => x.role_id,
                        principalTable: "Roles",
                        principalColumn: "role_id");
                });

            migrationBuilder.CreateTable(
                name: "CMS_Content",
                columns: table => new
                {
                    content_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    create_by = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CMS_Content", x => x.content_id);
                    table.ForeignKey(
                        name: "FK_CMS_Content_Users",
                        column: x => x.create_by,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Koi_Orders",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_id = table.Column<int>(type: "int", nullable: true),
                    pickup_location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    destination = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    weight = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    transport_method = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    total = table.Column<decimal>(type: "money", nullable: true),
                    additional_services = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pricing_id = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "('Pending')"),
                    placed_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    completed_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Koi_Orders", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_Koi_Orders_Pricing",
                        column: x => x.pricing_id,
                        principalTable: "Pricing",
                        principalColumn: "price_id");
                    table.ForeignKey(
                        name: "FK_Koi_Orders_Users",
                        column: x => x.customer_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    feedback_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: true),
                    customer_id = table.Column<int>(type: "int", nullable: true),
                    rating = table.Column<int>(type: "int", nullable: false),
                    comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    feedback_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.feedback_id);
                    table.ForeignKey(
                        name: "FK_Feedback_Orders",
                        column: x => x.order_id,
                        principalTable: "Koi_Orders",
                        principalColumn: "order_id");
                    table.ForeignKey(
                        name: "FK_Feedback_Users",
                        column: x => x.customer_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Order_Documents",
                columns: table => new
                {
                    document_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: true),
                    document_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    document_path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Documents", x => x.document_id);
                    table.ForeignKey(
                        name: "FK_Order_Documents_Orders",
                        column: x => x.order_id,
                        principalTable: "Koi_Orders",
                        principalColumn: "order_id");
                });

            migrationBuilder.CreateTable(
                name: "Transport_Process",
                columns: table => new
                {
                    process_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: true),
                    step = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "('Pending')"),
                    process_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transport_Process", x => x.process_id);
                    table.ForeignKey(
                        name: "FK_Transport_Process_Orders",
                        column: x => x.order_id,
                        principalTable: "Koi_Orders",
                        principalColumn: "order_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CMS_Content_create_by",
                table: "CMS_Content",
                column: "create_by");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_customer_id",
                table: "Feedback",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_order_id",
                table: "Feedback",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_Koi_Orders_customer_id",
                table: "Koi_Orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Koi_Orders_pricing_id",
                table: "Koi_Orders",
                column: "pricing_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Documents_order_id",
                table: "Order_Documents",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_Process_order_id",
                table: "Transport_Process",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_role_id",
                table: "Users",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CMS_Content");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Order_Documents");

            migrationBuilder.DropTable(
                name: "Transport_Process");

            migrationBuilder.DropTable(
                name: "Koi_Orders");

            migrationBuilder.DropTable(
                name: "Pricing");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
