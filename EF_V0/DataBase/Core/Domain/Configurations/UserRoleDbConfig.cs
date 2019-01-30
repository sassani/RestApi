using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EF_V0.DataBase.Core.Domain.Configurations
{
    public class UserRoleDbConfig : EntityConfiguration<UserRoleDb>
    {
        public override void Config(EntityTypeBuilder<UserRoleDb> builder)
        {

        }

        public override void Seed(EntityTypeBuilder<UserRoleDb> builder)
        {
            builder.HasData(new UserRoleDb
            {
                Id = 1,
                UserId = 1,
                RoleId = 1
            });

        }
    }
}
