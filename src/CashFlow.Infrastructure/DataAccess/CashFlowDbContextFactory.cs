using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CashFlow.Infrastructure.DataAccess;

public class CashFlowDbContextFactory : IDesignTimeDbContextFactory<CashFlowDbContext>
{
    public CashFlowDbContext CreateDbContext(string[] args)
    {
        const string defaultConnectionString = "Host=localhost;Port=5432;Database=cashflow;Username=cashflow;Password=cashflow123";
        var connectionString = Environment.GetEnvironmentVariable("CASHFLOW_CONNECTION_STRING") ?? defaultConnectionString;

        var optionsBuilder = new DbContextOptionsBuilder<CashFlowDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new CashFlowDbContext(optionsBuilder.Options);
    }
}
