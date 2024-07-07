using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using TeamsMasterDetail.Domain.Entities;

namespace TeamsMasterDetail.Domain.Context
{
    public class AppDbContext : DbContext
    {
        #region Public DbSet
        public DbSet<Team> Teams { get; set; }

        public DbSet<Member> Members { get; set; }
        #endregion

        #region Constructors
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        #endregion

        #region Protected Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().HasData(
                new Team { TeamId = 1, Name = "Santos", Description = "Santos - time-base - 1960" }
            );

            modelBuilder.Entity<Member>().HasData(
                new Member { TeamId = 1, Name = "Ana Cristina", Email = "ana@email.com", MemberId = 1 }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //Microsoft.Extensions.Configuration.Json
                var configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", false)
                    .Build();

                optionsBuilder
                    .UseLazyLoadingProxies()
                    .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning))
                    .UseSqlServer(configurationBuilder.GetConnectionString("DefaultConnection"));
            }
        }
        #endregion
    }
}