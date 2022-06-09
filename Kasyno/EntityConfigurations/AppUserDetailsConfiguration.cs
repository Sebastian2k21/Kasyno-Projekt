using Kasyno.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kasyno.EntityConfigurations
{
    /// <summary>
    /// konfiguracja encji szczegolowych danych uzytkownika
    /// </summary>
    class AppUserDetailsConfiguration:IEntityTypeConfiguration<AppUserDetails>
    {

        public void Configure(EntityTypeBuilder<AppUserDetails> builder)
        {
            builder.Property(p => p.FirstName).IsRequired();

            builder.Property(p => p.Surname).IsRequired();



        }
    }
}