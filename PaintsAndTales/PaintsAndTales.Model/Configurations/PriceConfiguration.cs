using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintsAndTales.Model.Entities;

namespace PaintsAndTales.Model.Configurations
{
	public class PriceConfiguration : IEntityTypeConfiguration<Price>
	{
		public void Configure(EntityTypeBuilder<Price> builder)
		{
			builder.ToTable("prices");

			builder.HasKey(t => t.Id);
			builder.Property(t => t.Id).IsRequired().HasColumnName("id").HasColumnType("int(11)").UseMySqlIdentityColumn();
			builder.Property(t => t.Created).IsRequired().HasColumnName("created").HasColumnType("DATETIME");
			builder.Property(t => t.Deleted).HasColumnName("deleted").HasColumnType("DATETIME");
			builder.Property(t => t.ProductId).IsRequired().HasColumnName("product_id").HasColumnType("int(11)");
			builder.Property(t => t.ProductSizeId).IsRequired().HasColumnName("product_size_id").HasColumnType("int(11)");
			builder.Property(t => t.Value).IsRequired().HasColumnName("value").HasColumnType("DECIMAL(5,2)");

			builder.HasIndex(a => a.ProductId);
			builder.HasIndex(a => a.ProductSizeId);

			builder.HasOne(a => a.Product).WithMany(a => a.Prices).HasForeignKey(a => a.ProductId).OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(a => a.ProductSize).WithMany(a => a.Prices).HasForeignKey(a => a.ProductSizeId).OnDelete(DeleteBehavior.Restrict);
		}
	}
}
