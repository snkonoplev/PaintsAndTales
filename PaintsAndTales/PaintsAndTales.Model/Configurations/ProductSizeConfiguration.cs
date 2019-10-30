using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintsAndTales.Model.Entities;

namespace PaintsAndTales.Model.Configurations
{
	public class ProductSizeConfiguration : IEntityTypeConfiguration<ProductSize>
	{
		public void Configure(EntityTypeBuilder<ProductSize> builder)
		{
			builder.ToTable("product_sizes");

			builder.HasKey(t => t.Id);
			builder.Property(t => t.Id).IsRequired().HasColumnName("id").HasColumnType("int(11)").UseMySqlIdentityColumn();
			builder.Property(t => t.Created).IsRequired().HasColumnName("created").HasColumnType("DATETIME");
			builder.Property(t => t.Deleted).HasColumnName("deleted").HasColumnType("DATETIME");
			builder.Property(t => t.Name).IsRequired().HasColumnName("name").HasColumnType("varchar(100)");
			builder.Property(t => t.Size).IsRequired().HasColumnName("size").HasColumnType("varchar(100)");
		}
	}
}
