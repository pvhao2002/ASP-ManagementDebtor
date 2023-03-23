using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Management_Debt.Models
{
	public partial class DBContext1 : DbContext
	{
		public DBContext1()
			: base("name=DBContext1")
		{
		}

		public virtual DbSet<DebitNote> DebitNote { get; set; }
		public virtual DbSet<Debtor> Debtor { get; set; }
		public virtual DbSet<DebtorUser> DebtorUser { get; set; }
		public virtual DbSet<User> User { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DebitNote>()
				.Property(e => e.money)
				.HasPrecision(19, 4);

			modelBuilder.Entity<Debtor>()
				.HasMany(e => e.DebitNote)
				.WithRequired(e => e.Debtor)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Debtor>()
				.HasMany(e => e.DebtorUser)
				.WithRequired(e => e.Debtor)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.DebitNote)
				.WithRequired(e => e.User)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.DebtorUser)
				.WithRequired(e => e.User)
				.WillCascadeOnDelete(false);
		}
	}
}
