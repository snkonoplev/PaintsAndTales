using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintsAndTales.Model.Entities;

namespace PaintsAndTales.Model.Configurations
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.ToTable("order_items");

			builder.HasKey(t => t.Id);
			builder.Property(t => t.Id).IsRequired().HasColumnName("id").HasColumnType("int(11)").UseMySqlIdentityColumn();
			builder.Property(t => t.Created).IsRequired().HasColumnName("created").HasColumnType("DATETIME");
			builder.Property(t => t.Deleted).HasColumnName("deleted").HasColumnType("DATETIME");
			builder.Property(t => t.OrderId).IsRequired().HasColumnName("order_id").HasColumnType("int(11)");
			builder.Property(t => t.ProductId).IsRequired().HasColumnName("product_id").HasColumnType("int(11)");
			builder.Property(t => t.SizeId).IsRequired().HasColumnName("size_id").HasColumnType("int(11)");
			builder.Property(t => t.ColorId).IsRequired().HasColumnName("color_id").HasColumnType("int(11)");
			builder.Property(t => t.Price).IsRequired().HasColumnName("price").HasColumnType("DECIMAL(5,2)");

			builder.HasIndex(a => a.OrderId);

			builder.HasOne(a => a.Order).WithMany(a => a.OrderItems).HasForeignKey(a => a.OrderId);
			builder.HasOne(a => a.Product).WithMany().HasForeignKey(a => a.ProductId);
			builder.HasOne(a => a.ProductSize).WithMany().HasForeignKey(a => a.SizeId);
			builder.HasOne(a => a.ColorEntity).WithMany().HasForeignKey(a => a.ColorId);
		}
	}
}
