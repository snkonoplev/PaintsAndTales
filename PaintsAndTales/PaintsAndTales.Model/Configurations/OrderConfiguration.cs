using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintsAndTales.Model.Entities;

namespace PaintsAndTales.Model.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.ToTable("orders");

			builder.HasKey(t => t.Id);
			builder.Property(t => t.Id).IsRequired().HasColumnName("id").HasColumnType("int(11)").UseMySqlIdentityColumn();
			builder.Property(t => t.Created).IsRequired().HasColumnName("created").HasColumnType("DATETIME");
			builder.Property(t => t.Deleted).HasColumnName("deleted").HasColumnType("DATETIME");
			builder.Property(t => t.UserId).HasColumnName("user_id").HasColumnType("int(11)");
			builder.Property(t => t.ContactId).HasColumnName("contact_id").HasColumnType("int(11)");

			builder.HasIndex(a => a.ContactId);
			builder.HasIndex(a => a.UserId);

			builder.HasOne(a => a.User).WithMany(a => a.Orders).HasForeignKey(a => a.UserId);
			builder.HasOne(a => a.Contact).WithOne(a => a.Order).HasForeignKey<Order>(a => a.ContactId);
		}
	}
}
