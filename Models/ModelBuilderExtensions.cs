using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Command>().HasData( 
                new Command 
                {
                    Id = 1,
                    HowTo = "How to create migrations in .NET Core",
                    Line = "Add-Migration MigrationName",
                    Platform = "EF Core"
                });
        }
    }
}
