using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace PaintsAndTales.Model
{
	public class SoftDeletedApplicationContext : ApplicationContext
	{
		static readonly MethodInfo SetGlobalQueryMethod = typeof(SoftDeletedApplicationContext).GetMethods(BindingFlags.Public | BindingFlags.Instance).Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");
		private static IList<Type> _entityTypeCache;

		public SoftDeletedApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			foreach (var type in GetEntityTypes())
			{
				MethodInfo method = SetGlobalQueryMethod.MakeGenericMethod(type);
				method.Invoke(this, new object[] { modelBuilder });
			}

			base.OnModelCreating(modelBuilder);
		}

		private static IList<Type> GetEntityTypes()
		{
			if (_entityTypeCache != null)
			{
				return _entityTypeCache.ToList();
			}

			_entityTypeCache = Assembly.GetExecutingAssembly().DefinedTypes
				.Where(a => a.GetInterfaces().Any(i => i == typeof(IEntity)))
				.Select(a => a.AsType())
				.ToList();

			return _entityTypeCache;
		}

		public void SetGlobalQuery<T>(ModelBuilder builder) where T : class, IEntity
		{
			builder.Entity<T>().HasKey(e => e.Id);
			builder.Entity<T>().HasQueryFilter(e => e.Deleted == null);
		}
	}
}
