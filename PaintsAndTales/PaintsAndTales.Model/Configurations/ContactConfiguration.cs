using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintsAndTales.Model.Entities;

namespace PaintsAndTales.Model.Configurations
{
	public class ContactConfiguration : IEntityTypeConfiguration<Contact>
	{
		public void Configure(EntityTypeBuilder<Contact> builder)
		{
			builder.ToTable("contacts");

			builder.HasKey(t => t.Id);
			builder.Property(t => t.Id).IsRequired().HasColumnName("id").HasColumnType("int(11)").UseMySqlIdentityColumn();
			builder.Property(t => t.Created).IsRequired().HasColumnName("created").HasColumnType("DATETIME");
			builder.Property(t => t.Deleted).HasColumnName("deleted").HasColumnType("DATETIME");
			builder.Property(t => t.OrderId).IsRequired().HasColumnName("order_id").HasColumnType("int(11)");
			builder.Property(t => t.Name).IsRequired().HasColumnName("name").HasColumnType("varchar(500)");
			builder.Property(t => t.Address).IsRequired().HasColumnName("address").HasColumnType("TEXT");
			builder.Property(t => t.MobilePhone).IsRequired().HasColumnName("mobile_phone").HasColumnType("varchar(50)");
			builder.Property(t => t.Comment).HasColumnName("comment").HasColumnType("TEXT");

			builder.HasIndex(a => a.OrderId);

			builder.HasOne(a => a.Order).WithOne(a => a.Contact).HasForeignKey<Contact>(a => a.OrderId);
		}
	}
}
