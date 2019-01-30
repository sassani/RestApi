using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EF_V0.Helpers;
using Microsoft.Extensions.Options;
using EF_V0.Extensions;

namespace EF_V0.DataBase.Core.Domain.Configurations
{
    public class ClientDbConfig : EntityConfiguration<ClientDb>
    {
        private readonly IOptions<AppSettingsModel> config;

        public ClientDbConfig(IOptions<AppSettingsModel> config)
        {
            this.config = config;
        }
        public override void Config(EntityTypeBuilder<ClientDb> builder)
        {
            builder.Property(f => f.ClientPublicId).IsRequired().HasMaxLength(25);
            builder.Property(f => f.ClientSecret).IsRequired().HasMaxLength(75);
        }

        public override void Seed(EntityTypeBuilder<ClientDb> builder)
        {
            var client = config.Value.BaseClient;
            builder.HasData(new ClientDb
            {
                Id = 1,
                ClientPublicId = client.ClientId,
                ClientSecret = StringHelper.StringToHash(client.ClientSecret),
                Name = client.Name,
                Type = client.Type
            });
        }
    }
}
