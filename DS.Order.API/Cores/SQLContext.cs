using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

using DS.Common.Models;
using DS.Common.Entities;

namespace DS.Order.API.Cores;

public class SQLContext : DbContext
{

    public DbSet<UserAccount> UserAccounts { get; set; }

    public SQLContext() : base()
    {

    }

    public SQLContext(DbContextOptions<SQLContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //載入組件
        Assembly assembly = Assembly.Load("DS.Common");

        foreach (Type type in assembly.GetTypes())
        {
            if (type.IsClass == true)
            {
                if (type.FullName.Contains("DS.Common.Entities") == true)
                {
                    var instance = Activator.CreateInstance(type) as EntityBase;
                    if (instance != null)
                    {
                        instance.ModelBuilder(modelBuilder);
                    }
                }
            }
        }
    }
}