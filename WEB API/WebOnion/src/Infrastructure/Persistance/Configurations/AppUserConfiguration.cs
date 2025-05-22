using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            builder.Ignore(x => x.AccessFailedCount);
            builder.Ignore(x => x.LockoutEnd);
            builder.Ignore(x => x.LockoutEnabled);
            builder.Ignore(x => x.PhoneNumber);
            builder.Ignore(x => x.PhoneNumberConfirmed);

            var hasher = new PasswordHasher<AppUser>();

            builder.HasData(new AppUser
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin1@gmail.com",
                NormalizedEmail ="ADMIN1@GMAIL.COM",
                FirstName = "Admin",
                LastName = "Kuzey",
                PasswordHash = /*"AQAAAAEAACcQAAAAEGMzXZ0Y4Gkv7A==",*/hasher.HashPassword(null, "admin123"),
                SecurityStamp = /*"d3b2cb2e-f2be-4995-9b8e-32793f7cbec1",*/Guid.NewGuid().ToString(),
                IsActive = true,
                IsDeleted = false
                
                
            },
     
            new AppUser
            {
                Id = 2,
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user1@gmail.com",
                NormalizedEmail ="USER1@GMAIL.COM",
                FirstName = "User",
                LastName = "Kuzey",
                PasswordHash = /*"AQAAAAEAACcQAAAAEExlUSY5ZtNdiQ==",*/  hasher.HashPassword(null, "user123"),
                SecurityStamp = /*"e2fbbbaf-2055-4eaa-8ebd-1de3d3a7d63c",*/Guid.NewGuid().ToString(),
                IsActive = true,
                IsDeleted = false
            }



                );

        }
    }
}
