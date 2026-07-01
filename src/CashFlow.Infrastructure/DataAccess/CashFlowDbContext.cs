using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;

internal class CashFlowDbContext : DbContext
{
    public CashFlowDbContext(DbContextOptions<CashFlowDbContext> options) : base(options)
    {
    }

    public DbSet<Expense> Expenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>(entity =>
        {
            entity.ToTable("Expenses");

            entity.HasKey(expense => expense.Id);

            entity.Property(expense => expense.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(expense => expense.Description)
                .HasMaxLength(500);

            entity.Property(expense => expense.Amount)
                .HasPrecision(18, 2);

            entity.Property(expense => expense.Paymenttype)
                .IsRequired();
        });
    }
}
