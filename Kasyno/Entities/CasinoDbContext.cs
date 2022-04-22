using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Kasyno.Entities
{
    class CasinoDbContext:DbContext
    {

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<History> History{get; set; }
        public DbSet<AppUserDetails> UsersDetails { get; set; }
        public CasinoDbContext(DbContextOptions options) : base(options) { }

        public CasinoDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer("Server = .\\SQLEXPRESS; Database = Casino; Trusted_Connection = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}



