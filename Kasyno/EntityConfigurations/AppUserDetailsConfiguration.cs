using Kasyno.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kasyno.EntityConfigurations
{
    class AppUserDetailsConfiguration:IEntityTypeConfiguration<AppUserDetails>
    {

        public void Configure(EntityTypeBuilder<AppUserDetails> builder)
        {
            builder.Property(p => p.FirstName).IsRequired();

            builder.Property(p => p.Surname).IsRequired();



        }
    }
}