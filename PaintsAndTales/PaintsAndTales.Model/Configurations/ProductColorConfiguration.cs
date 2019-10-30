using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintsAndTales.Model.Entities;

namespace PaintsAndTales.Model.Configurations
{
	public class ProductColorConfiguration : IEntityTypeConfiguration<ProductColor>
	{
		public void Configure(EntityTypeBuilder<ProductColor> builder)
		{
			builder.ToTable("product_colors");

			builder.HasKey(t => t.Id);
			builder.Property(t => t.Id).IsRequired().HasColumnName("id").HasColumnType("int(11)").UseMySqlIdentityColumn();
			builder.Property(t => t.Created).IsRequired().HasColumnName("created").HasColumnType("DATETIME");
			builder.Property(t => t.Deleted).HasColumnName("deleted").HasColumnType("DATETIME");
			builder.Property(t => t.ProductId).IsRequired().HasColumnName("product_id").HasColumnType("int(11)");
			builder.Property(t => t.ColorEntityId).IsRequired().HasColumnName("color_id").HasColumnType("int(11)");

			builder.HasIndex(a => a.ProductId);
			builder.HasIndex(a => a.ColorEntityId);

			builder.HasOne(a => a.Product).WithMany(a => a.ProductColors).HasForeignKey(a => a.ProductId).OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(a => a.ColorEntity).WithMany(a => a.ProductColors).HasForeignKey(a => a.ColorEntityId).OnDelete(DeleteBehavior.Restrict);
		}
	}
}
