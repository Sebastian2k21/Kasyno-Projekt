using Kasyno.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kasyno.EntityConfigurations
{
    /// <summary>
    /// konfiguracja encji gry
    /// </summary>
    class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.Property(p => p.Name).IsRequired();    
        }
    }
}