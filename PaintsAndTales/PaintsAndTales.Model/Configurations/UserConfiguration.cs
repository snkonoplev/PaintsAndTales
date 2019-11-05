using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintsAndTales.Model.Entities;

namespace PaintsAndTales.Model.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("users");

			builder.HasKey(t => t.Id);
			builder.Property(t => t.Id).IsRequired().HasColumnName("id").HasColumnType("int(11)").UseMySqlIdentityColumn();
			builder.Property(t => t.Created).IsRequired().HasColumnName("created").HasColumnType("DATETIME");
			builder.Property(t => t.Deleted).HasColumnName("deleted").HasColumnType("DATETIME");
			builder.Property(t => t.Email).IsRequired().HasColumnName("email").HasColumnType("varchar(50)");
			builder.Property(t => t.Password).IsRequired().HasColumnName("password").HasColumnType("varchar(50)");
			builder.Property(t => t.MobilePhone).IsRequired().HasColumnName("mobile_phone").HasColumnType("varchar(50)");
			builder.Property(t => t.FirstName).IsRequired().HasColumnName("first_name").HasColumnType("varchar(50)");
			builder.Property(t => t.MiddleName).HasColumnName("middle_name").HasColumnType("varchar(50)");
			builder.Property(t => t.LastName).HasColumnName("last_name").HasColumnType("varchar(50)");

			builder.HasIndex(a => a.Email).IsUnique();
		}
	}
}
