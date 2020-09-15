using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace CanvasAPI.Models
{
    public class AppDatabase : DbContext
    {
        public AppDatabase(DbContextOptions<AppDatabase> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Poll> Poll { get; set; }
        public DbSet<PollOption> PollOption { get; set; }
        public DbSet<Vote> Vote { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .Property(p => p.User_ID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Poll>()
                .Property(p => p.Poll_ID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<PollOption>()
                .Property(p => p.Option_ID)
                .ValueGeneratedOnAdd();


        }

        
    }

}

