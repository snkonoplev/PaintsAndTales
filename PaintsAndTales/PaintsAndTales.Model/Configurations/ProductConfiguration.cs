using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintsAndTales.Model.Entities;

namespace PaintsAndTales.Model.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasKey(t => t.Id);
			builder.Property(t => t.Id).IsRequired().HasColumnName("id").HasColumnType("int(11)").UseMySqlIdentityColumn();
			builder.Property(t => t.Name).IsRequired().HasColumnName("name").HasColumnType("varchar(100)");
			builder.Property(t => t.Description).HasColumnName("description").HasColumnType("text");
			builder.Property(t => t.Price).IsRequired().HasColumnName("price").HasColumnType("DECIMAL(5,2)");
		}
	}
}
