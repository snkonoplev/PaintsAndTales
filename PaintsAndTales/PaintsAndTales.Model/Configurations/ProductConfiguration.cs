using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintsAndTales.Model.Entities;

namespace PaintsAndTales.Model.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.ToTable("products");

			builder.HasKey(t => t.Id);
			builder.Property(t => t.Id).IsRequired().HasColumnName("id").HasColumnType("int(11)").UseMySqlIdentityColumn();
			builder.Property(t => t.Created).IsRequired().HasColumnName("created").HasColumnType("DATETIME");
			builder.Property(t => t.Deleted).HasColumnName("deleted").HasColumnType("DATETIME");
			builder.Property(t => t.IsActive).IsRequired().HasColumnName("is_active").HasColumnType("bit");
			builder.Property(t => t.Name).IsRequired().HasColumnName("name").HasColumnType("varchar(100)");
			builder.Property(t => t.Description).HasColumnName("description").HasColumnType("text");
			builder.Property(t => t.CategoryId).HasColumnName("category_id").HasColumnType("int(11)");

			builder.HasIndex(a => a.CategoryId);

			builder.HasOne(a => a.Category).WithMany(a => a.Products).HasForeignKey(a => a.CategoryId).OnDelete(DeleteBehavior.Restrict);
		}
	}
}
