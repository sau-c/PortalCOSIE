using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace PortalCOSIE.Infrastructure.Data.Configurations.Identity
{
    public class AspNetUsersConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        private readonly string _adminPasswordHash;
        private readonly string _adminEmail;

        public AspNetUsersConfiguration(IConfiguration configuration)
        {
            _adminEmail = configuration["AdminUser:Email"];
            var adminPassword = configuration["AdminUser:Password"];
            _adminPasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(null, adminPassword);
        }

        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            builder.HasData(
                new IdentityUser
                {
                    Id = "00000000-0000-0000-0000-000000000000",
                    UserName = _adminEmail,
                    NormalizedUserName = _adminEmail.ToUpper(),
                    Email = _adminEmail,
                    NormalizedEmail = _adminEmail.ToUpper(),
                    EmailConfirmed = true,
                    PasswordHash = _adminPasswordHash,
                    SecurityStamp = "AAAAAAAAAAAAAA",
                    ConcurrencyStamp = "dddddddd-dddd-dddd-dddd-dddddddddddd",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
            );
        }
    }
}