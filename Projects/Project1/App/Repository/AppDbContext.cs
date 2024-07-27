using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace App.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}

    public AppDbContext(){}


    public DbSet<Workday> Workdays {get;set;}


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("Service/appsettings.json")
                                            .Build();
            var connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    //I don't believe I need an OnModelCreating, but this is where it would go.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Workday>().HasKey(w => w.Date);
    }

    public static Workday GetLastWorkday(DbSet<Workday> day){
        return day.LastOrDefault();
    }
}