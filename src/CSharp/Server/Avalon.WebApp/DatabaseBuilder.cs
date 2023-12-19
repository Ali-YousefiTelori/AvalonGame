using EasyMicroservices.Cores.Relational.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Avalon.WebApp;

public class DatabaseBuilder : EntityFrameworkCoreDatabaseBuilder
{
    public DatabaseBuilder(IConfiguration configuration) : base(configuration)
    {
    }

    public override void OnConfiguring(DbContextOptionsBuilder optionsBuilder, string name)
    {
        if (name == "SqlServer")
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("local"));
        else
            optionsBuilder.UseInMemoryDatabase("Avalon");
    }
}
