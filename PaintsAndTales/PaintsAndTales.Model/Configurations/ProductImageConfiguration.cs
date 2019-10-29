using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintsAndTales.Model.Entities;

namespace PaintsAndTales.Model.Configurations
{
	public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
	{
		public void Configure(EntityTypeBuilder<ProductImage> builder)
		{
			builder.HasKey(t => t.Id);
			builder.Property(t => t.Id).IsRequired().HasColumnName("id").HasColumnType("int(11)").UseMySqlIdentityColumn();
			builder.Property(t => t.ProductId).IsRequired().HasColumnName("product_id").HasColumnType("int(11)");
			builder.Property(t => t.IsTitleImage).IsRequired().HasColumnName("is_title_image").HasColumnType("bit");
			builder.Property(t => t.Path).IsRequired().HasColumnName("path").HasColumnType("varchar(100)");

			builder.HasIndex(a => a.ProductId);

			builder.HasOne(a => a.Product).WithMany(a => a.ProductImages).HasForeignKey(a => a.ProductId);
		}
	}
}
