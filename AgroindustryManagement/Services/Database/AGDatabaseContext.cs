using AgroindustryManagement.Models;
using Microsoft.EntityFrameworkCore;
namespace AgroindustryManagement.Services.Database;

public class AGDatabaseContext: DbContext
{
    public DbSet<Field> Fields { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<InventoryItem> InventoryItems { get; set; }
    public DbSet<WorkerTask> WorkerTasks { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=agroindustry_management.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Field>().ToTable("Fields");
        modelBuilder.Entity<Worker>().ToTable("Workers");
        modelBuilder.Entity<Machine>().ToTable("Machines");
        modelBuilder.Entity<InventoryItem>().ToTable("InventoryItems");
        modelBuilder.Entity<WorkerTask>().ToTable("WorkerTasks");
        modelBuilder.Entity<Resource>().ToTable("Resources");
        modelBuilder.Entity<Warehouse>().ToTable("Warehouses");

        //relationships and constraints 
        modelBuilder.Entity<Field>()
            .HasMany(f => f.Workers)
            .WithMany();

        modelBuilder.Entity<Field>()
            .HasMany(f => f.Machines)
            .WithOne(m => m.Field);

        modelBuilder.Entity<Field>()
            .HasMany(f => f.Tasks)
            .WithOne(t => t.Field);

        modelBuilder.Entity<Warehouse>()
            .HasMany(w => w.InventoryItems)
            .WithOne(i => i.Warehouse);

        modelBuilder.Entity<Resource>()
            .HasMany(r => r.RequiredMachines)
            .WithOne(m => m.Resource);

        modelBuilder.Entity<WorkerTask>()
            .HasOne(t => t.Worker)
            .WithMany(w => w.Tasks);

    }
}