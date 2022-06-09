using Kasyno.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Kasyno.EntityConfigurations
{
    /// <summary>
    /// konfiguracja encji uzytkownika
    /// </summary>
    class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(p => p.Login).IsRequired();

            builder.Property(p => p.Passsword).IsRequired();

            builder.HasIndex(p => p.Login).IsUnique();
        }
    }
}
