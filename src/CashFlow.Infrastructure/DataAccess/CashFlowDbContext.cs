using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;

public class CashFlowDbContext : DbContext
{
    public CashFlowDbContext(DbContextOptions<CashFlowDbContext> options) : base(options){}

    public DbSet<Expense> Expenses { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(user => user.Id);

            entity.Property(user => user.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(user => user.Password)
                .IsRequired()
                .HasMaxLength(2000);

            entity.Property(user => user.UserIdentifier)
                .IsRequired();

            entity.Property(user => user.Role)
                .IsRequired()
                .HasMaxLength(20);

            entity.HasIndex(user => user.Email)
                .IsUnique();

            entity.HasIndex(user => user.UserIdentifier)
                .IsUnique();
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.ToTable("Expenses");

            entity.HasKey(expense => expense.Id);

            entity.Property(expense => expense.Title)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(expense => expense.Description)
                .HasMaxLength(500);

            entity.Property(expense => expense.Amount)
                .HasPrecision(18, 2);

            entity.Property(expense => expense.Paymenttype)
                .IsRequired();

            entity.HasOne(expense => expense.User)
                .WithMany()
                .HasForeignKey(expense => expense.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
