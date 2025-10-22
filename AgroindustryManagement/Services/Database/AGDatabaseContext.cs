using Microsoft.EntityFrameworkCore;
namespace AgroindustryManagement.Services.Database;

public class AGDatabaseContext: DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=agroindustry_management.db");
    }
}