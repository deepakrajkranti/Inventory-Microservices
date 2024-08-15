using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data;

public class DataContext :DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<ExampleMessage> Orders => Set<ExampleMessage>();
}