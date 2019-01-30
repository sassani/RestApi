using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EF_V0.Helpers;
using System;
using Microsoft.Extensions.Options;
using EF_V0.Extensions;

namespace EF_V0.DataBase.Core.Domain.Configurations
{
    public class UserDbConfig : EntityConfiguration<UserDb>
    {
        private readonly IOptions<AppSettingsModel> config;

        public UserDbConfig(IOptions<AppSettingsModel> config)
        {
            this.config = config;
        }
        public override void Config(EntityTypeBuilder<UserDb> builder)
        {
            builder.Property(f => f.Email).IsUnicode().IsRequired().HasMaxLength(25);
            builder.Property(f => f.Password).IsRequired().HasMaxLength(75);
            builder.Property(f => f.IsActive).HasColumnType("TINYINT(1)");
            builder.Property(f => f.IsEmailVerified).HasColumnType("TINYINT(1)");
        }

        public override void Seed(EntityTypeBuilder<UserDb> builder)
        {
            var admin = config.Value.BaseAdmin;
            builder.HasData(new UserDb
            {
                Id = 1,
                PublicId = StringHelper.GenerateRandom(11),
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                Password = StringHelper.StringToHash(admin.Password),
                IsActive = true,
                IsEmailVerified = true,
                LastLoginAt = DateTime.Now
            });
        }
    }
}
