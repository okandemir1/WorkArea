namespace WorkArea.Persistence
{
    using Domain.Entities;
    using Domain.Entities.Base;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class WorkAreaDbContext : DbContext
    {
        public DbSet<Archive> Archives { get; set; }
        public DbSet<ArchiveType> ArchiveTypes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<InstallmentItem> InstallmentItems { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteHashtag> NoteHashtags { get; set; }
        public DbSet<NoteMention> NoteMentions { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonDebt> PersonDebts { get; set; }
        public DbSet<PersonDebtHistory> PersonDebtHistories { get; set; }
        public DbSet<PersonIban> PersonIbans { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }
        public DbSet<ShoppingListUser> ShoppingListUsers { get; set; }
        public DbSet<User> Users { get; set; }

        public WorkAreaDbContext(DbContextOptions<WorkAreaDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
            
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AddTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntityWithDate && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntityWithDate)entity.Entity).CreateDate = DateTime.Now;
                }

                ((BaseEntityWithDate)entity.Entity).UpdateDate = DateTime.Now;
            }
        }

    }
}