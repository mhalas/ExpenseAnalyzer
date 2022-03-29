using DataAccess.Contracts.Model;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataAccess.EntityFramework
{
    public class ExpenseDbContext: DbContext
    {
        private readonly string _connectionString;

        public ExpenseDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ExpenseDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Expense> Expense { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserCategory> UserCategory { get; set; }
        public virtual DbSet<UserCategoryValue> UserCategoryValue { get; set; }
        public virtual DbSet<UserConfiguration> UserConfiguration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(x => x.Description)
                    .HasMaxLength(255);

                entity.Property(e => e.PayDate)
                    .IsRequired();

                entity.Property(e => e.Price)
                    .IsRequired();

                entity.Property(e => e.SellerName)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(e => e.UserCategoryId)
                    .IsRequired();

                entity.HasOne(e => e.UserCategory)
                    .WithMany(e => e.Expenses);

                entity.Property(e => e.UserId)
                    .IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany(e => e.Expenses);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(e => e.UserName)
                    .HasMaxLength(25)
                    .IsRequired();

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsRequired();
            });

            modelBuilder.Entity<UserCategory>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.UserId)
                    .IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany(e => e.UserCategories);
            });

            modelBuilder.Entity<UserCategoryValue>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(e => e.SellerName)
                    .IsRequired();

                entity.Property(e => e.UserCategoryId)
                    .IsRequired();

                entity.HasOne(e => e.UserCategory)
                    .WithMany(e => e.UserCategoryValues);
                    
            });

            modelBuilder.Entity<UserConfiguration>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(e => e.Key)
                    .HasMaxLength(25)
                    .IsRequired();

                entity.Property(e=>e.Value)
                    .HasMaxLength(25)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(e => e.UserId)
                    .IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany(e => e.UserConfigurations);
            });
        }
    }
}
