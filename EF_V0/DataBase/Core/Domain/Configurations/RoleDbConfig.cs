using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EF_V0.DataBase.Core.Domain.Configurations
{
    public class RoleDbConfig : EntityConfiguration<RoleDb>
    {
        public override void Config(EntityTypeBuilder<RoleDb> builder)
        {

        }

        public override void Seed(EntityTypeBuilder<RoleDb> builder)
        {
            builder.HasData(new RoleDb
            {
                Id = 1,
                Name = "super_admin",
            });
            builder.HasData(new RoleDb
            {
                Id = 2,
                Name = "admin",
            });
            builder.HasData(new RoleDb
            {
                Id = 3,
                Name = "reader",
            });
            builder.HasData(new RoleDb
            {
                Id = 4,
                Name = "writer",
            });
        }
    }
}
