using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Models
{
    public partial class KoiFishTransportationDBContext : DbContext
    {
        public KoiFishTransportationDBContext()
        {
        }

        public KoiFishTransportationDBContext(DbContextOptions<KoiFishTransportationDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CmsContent> CmsContents { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<KoiOrder> KoiOrders { get; set; } = null!;
        public virtual DbSet<OrderDocument> OrderDocuments { get; set; } = null!;
        public virtual DbSet<Pricing> Pricings { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<TransportProcess> TransportProcesses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                string ConnectionStr = config.GetConnectionString("local");

                optionsBuilder.UseSqlServer(ConnectionStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CmsContent>(entity =>
            {
                entity.HasKey(e => e.ContentId);

                entity.ToTable("CMS_Content");

                entity.Property(e => e.ContentId).HasColumnName("content_id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.ContentType)
                    .HasMaxLength(50)
                    .HasColumnName("content_type");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Image)
                    .HasMaxLength(50)
                    .HasColumnName("image");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.CmsContents)
                    .HasForeignKey(d => d.CreateBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CMS_Content_Users");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");

                entity.Property(e => e.Comments).HasColumnName("comments");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.FeedbackDate)
                    .HasColumnType("datetime")
                    .HasColumnName("feedback_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Feedback_Users");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Feedback_Orders");
            });

            modelBuilder.Entity<KoiOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.ToTable("Koi_Orders");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.AdditionalServices).HasColumnName("additional_services");

                entity.Property(e => e.CompletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("completed_date");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Destination)
                    .HasMaxLength(255)
                    .HasColumnName("destination");

                entity.Property(e => e.PickupLocation)
                    .HasMaxLength(255)
                    .HasColumnName("pickup_location");

                entity.Property(e => e.PlacedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("placed_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PricingId).HasColumnName("pricing_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('Pending')");

                entity.Property(e => e.Total)
                    .HasColumnType("money")
                    .HasColumnName("total");

                entity.Property(e => e.TransportMethod)
                    .HasMaxLength(255)
                    .HasColumnName("transport_method");

                entity.Property(e => e.Weight)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("weight");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.KoiOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Koi_Orders_Users");

                entity.HasOne(d => d.Pricing)
                    .WithMany(p => p.KoiOrders)
                    .HasForeignKey(d => d.PricingId)
                    .HasConstraintName("FK_Koi_Orders_Pricing");
            });

            modelBuilder.Entity<OrderDocument>(entity =>
            {
                entity.HasKey(e => e.DocumentId);

                entity.ToTable("Order_Documents");

                entity.Property(e => e.DocumentId).HasColumnName("document_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DocumentPath)
                    .HasMaxLength(255)
                    .HasColumnName("document_path");

                entity.Property(e => e.DocumentType)
                    .HasMaxLength(50)
                    .HasColumnName("document_type");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDocuments)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Order_Documents_Orders");
            });

            modelBuilder.Entity<Pricing>(entity =>
            {
                entity.HasKey(e => e.PriceId);

                entity.ToTable("Pricing");

                entity.Property(e => e.PriceId).HasColumnName("price_id");

                entity.Property(e => e.AdditionalServicePrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("additional_service_price");

                entity.Property(e => e.PricePerKg)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price_per_kg");

                entity.Property(e => e.TransportMethod)
                    .HasMaxLength(255)
                    .HasColumnName("transport_method");

                entity.Property(e => e.WeightRange)
                    .HasMaxLength(255)
                    .HasColumnName("weight_range");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<TransportProcess>(entity =>
            {
                entity.HasKey(e => e.ProcessId);

                entity.ToTable("Transport_Process");

                entity.Property(e => e.ProcessId).HasColumnName("process_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProcessDate)
                    .HasColumnType("datetime")
                    .HasColumnName("process_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('Pending')");

                entity.Property(e => e.Step)
                    .HasMaxLength(255)
                    .HasColumnName("step");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TransportProcesses)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Transport_Process_Orders");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(255)
                    .HasColumnName("password_hash");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Users_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
