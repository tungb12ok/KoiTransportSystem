﻿// <auto-generated />
using System;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(KoiFishTransportationDBContext))]
    [Migration("20241030174403_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Infrastructure.Models.CmsContent", b =>
                {
                    b.Property<int>("ContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("content_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContentId"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("content");

                    b.Property<string>("ContentType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("content_type");

                    b.Property<int>("CreateBy")
                        .HasColumnType("int")
                        .HasColumnName("create_by");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Image")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("image");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("title");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at");

                    b.HasKey("ContentId");

                    b.HasIndex("CreateBy");

                    b.ToTable("CMS_Content", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Models.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("feedback_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"), 1L, 1);

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("comments");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("customer_id");

                    b.Property<DateTime?>("FeedbackDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("feedback_date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("order_id");

                    b.Property<int>("Rating")
                        .HasColumnType("int")
                        .HasColumnName("rating");

                    b.HasKey("FeedbackId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderId");

                    b.ToTable("Feedback", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Models.KoiOrder", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("order_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<string>("AdditionalServices")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("additional_services");

                    b.Property<DateTime?>("CompletedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("completed_date");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("customer_id");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("destination");

                    b.Property<string>("PickupLocation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("pickup_location");

                    b.Property<DateTime?>("PlacedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("placed_date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("PricingId")
                        .HasColumnType("int")
                        .HasColumnName("pricing_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("status")
                        .HasDefaultValueSql("('Pending')");

                    b.Property<decimal?>("Total")
                        .HasColumnType("money")
                        .HasColumnName("total");

                    b.Property<string>("TransportMethod")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("transport_method");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("weight");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PricingId");

                    b.ToTable("Koi_Orders", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Models.OrderDocument", b =>
                {
                    b.Property<int>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("document_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DocumentId"), 1L, 1);

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("DocumentPath")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("document_path");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("document_type");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("image");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("order_id");

                    b.HasKey("DocumentId");

                    b.HasIndex("OrderId");

                    b.ToTable("Order_Documents", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Models.Pricing", b =>
                {
                    b.Property<int>("PriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("price_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PriceId"), 1L, 1);

                    b.Property<decimal?>("AdditionalServicePrice")
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("additional_service_price");

                    b.Property<decimal>("PricePerKg")
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("price_per_kg");

                    b.Property<string>("TransportMethod")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("transport_method");

                    b.Property<string>("WeightRange")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("weight_range");

                    b.HasKey("PriceId");

                    b.ToTable("Pricing", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("role_name");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Infrastructure.Models.TransportProcess", b =>
                {
                    b.Property<int>("ProcessId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("process_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProcessId"), 1L, 1);

                    b.Property<int?>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("order_id");

                    b.Property<DateTime?>("ProcessDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("process_date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("status")
                        .HasDefaultValueSql("('Pending')");

                    b.Property<string>("Step")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("step");

                    b.HasKey("ProcessId");

                    b.HasIndex("OrderId");

                    b.ToTable("Transport_Process", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("address");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("password_hash");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("phone");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Infrastructure.Models.CmsContent", b =>
                {
                    b.HasOne("Infrastructure.Models.User", "CreateByNavigation")
                        .WithMany("CmsContents")
                        .HasForeignKey("CreateBy")
                        .IsRequired()
                        .HasConstraintName("FK_CMS_Content_Users");

                    b.Navigation("CreateByNavigation");
                });

            modelBuilder.Entity("Infrastructure.Models.Feedback", b =>
                {
                    b.HasOne("Infrastructure.Models.User", "Customer")
                        .WithMany("Feedbacks")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Feedback_Users");

                    b.HasOne("Infrastructure.Models.KoiOrder", "Order")
                        .WithMany("Feedbacks")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_Feedback_Orders");

                    b.Navigation("Customer");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Infrastructure.Models.KoiOrder", b =>
                {
                    b.HasOne("Infrastructure.Models.User", "Customer")
                        .WithMany("KoiOrders")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Koi_Orders_Users");

                    b.HasOne("Infrastructure.Models.Pricing", "Pricing")
                        .WithMany("KoiOrders")
                        .HasForeignKey("PricingId")
                        .HasConstraintName("FK_Koi_Orders_Pricing");

                    b.Navigation("Customer");

                    b.Navigation("Pricing");
                });

            modelBuilder.Entity("Infrastructure.Models.OrderDocument", b =>
                {
                    b.HasOne("Infrastructure.Models.KoiOrder", "Order")
                        .WithMany("OrderDocuments")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_Order_Documents_Orders");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Infrastructure.Models.TransportProcess", b =>
                {
                    b.HasOne("Infrastructure.Models.KoiOrder", "Order")
                        .WithMany("TransportProcesses")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_Transport_Process_Orders");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Infrastructure.Models.User", b =>
                {
                    b.HasOne("Infrastructure.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_Users_Roles");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Infrastructure.Models.KoiOrder", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("OrderDocuments");

                    b.Navigation("TransportProcesses");
                });

            modelBuilder.Entity("Infrastructure.Models.Pricing", b =>
                {
                    b.Navigation("KoiOrders");
                });

            modelBuilder.Entity("Infrastructure.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Infrastructure.Models.User", b =>
                {
                    b.Navigation("CmsContents");

                    b.Navigation("Feedbacks");

                    b.Navigation("KoiOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
