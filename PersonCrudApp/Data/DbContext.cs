using Microsoft.EntityFrameworkCore;
using PersonCrudApp.Models;
using System.Configuration;
using System.Diagnostics;

namespace PersonCrudApp.Data;

public class PeopleDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=../../../database.db");
    }
    public DbSet<Person> People { get; set; }
}
