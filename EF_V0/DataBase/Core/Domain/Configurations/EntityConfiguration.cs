using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_V0.DataBase.Core.Domain.Configurations
{
    public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            Config(builder);
            Seed(builder);
        }

        public abstract void Config(EntityTypeBuilder<TEntity> builder);

        public virtual void Seed(EntityTypeBuilder<TEntity> builder)
        {

        }
    }
}
