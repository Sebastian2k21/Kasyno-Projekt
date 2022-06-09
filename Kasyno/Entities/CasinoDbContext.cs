using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Kasyno.Entities
{
    /// <summary>
    /// kontekst aplikacji - dostepu do bazy
    /// </summary>
    public class CasinoDbContext:DbContext
    {
        private bool test;
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<History> History{get; set; }
        public DbSet<AppUserDetails> UsersDetails { get; set; }
        public CasinoDbContext(DbContextOptions options, bool test = false) : base(options) 
        {
            this.test = test;
        }

        public CasinoDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if(test == false)
            {
                optionBuilder.UseSqlServer("Server = localhost\\SQLEXPRESS; Database = Casino; Trusted_Connection = True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}



