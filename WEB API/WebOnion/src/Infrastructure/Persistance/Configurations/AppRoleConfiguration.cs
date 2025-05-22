using Domain.Entities.Identity;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Configurations
{
    internal class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.ToTable("AppRoles");

            builder.HasData(new AppRole
            {
                Id = 1,
                Name = Roles.Admin,
                NormalizedName =Roles.Admin.ToUpper(new CultureInfo("en-US")),
                IsActive = true,
                IsDeleted = false

            },
            new AppRole
            {
                Id = 2,
                Name = Roles.User,
                NormalizedName = Roles.User.ToUpper(new CultureInfo("en-US")),
                IsActive = true,
                IsDeleted = false
            }

            );
        }
    }
}
