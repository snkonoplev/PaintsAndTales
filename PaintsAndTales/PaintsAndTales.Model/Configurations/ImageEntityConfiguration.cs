using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintsAndTales.Model.Entities;

namespace PaintsAndTales.Model.Configurations
{
	public class ImageEntityConfiguration : IEntityTypeConfiguration<ImageEntity>
	{
		public void Configure(EntityTypeBuilder<ImageEntity> builder)
		{
			builder.ToTable("images");

			builder.HasKey(t => t.Id);
			builder.Property(t => t.Id).IsRequired().HasColumnName("id").HasColumnType("int(11)").UseMySqlIdentityColumn();
			builder.Property(t => t.Created).IsRequired().HasColumnName("created").HasColumnType("DATETIME");
			builder.Property(t => t.Deleted).HasColumnName("deleted").HasColumnType("DATETIME");
			builder.Property(t => t.ProductId).HasColumnName("product_id").HasColumnType("int(11)");
			builder.Property(t => t.ColorId).HasColumnName("color_id").HasColumnType("int(11)");
			builder.Property(t => t.IsTitleImage).IsRequired().HasColumnName("is_title_image").HasColumnType("bit");
			builder.Property(t => t.FileName).IsRequired().HasColumnName("file_name").HasColumnType("varchar(100)");
			builder.Property(t => t.FileExtension).IsRequired().HasColumnName("file_extension").HasColumnType("varchar(5)");

			builder.HasIndex(a => a.ProductId);
			builder.HasIndex(a => a.ColorId);

			builder.HasOne(a => a.Product).WithMany(a => a.ProductImages).HasForeignKey(a => a.ProductId).OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(a => a.Color).WithMany(a => a.ProductImages).HasForeignKey(a => a.ColorId).OnDelete(DeleteBehavior.Restrict);
		}
	}
}
