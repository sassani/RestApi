using EF_V0.DataBase.Core.Domain;
using EF_V0.DataBase.Core.Domain.Configurations;
using Microsoft.EntityFrameworkCore;
using System;

namespace EF_V0.DataBase.Persistence
{
    public partial class ApiContext
    {
        public virtual DbSet<ClientDb> Client { get; set; }
        public virtual DbSet<UserDb> User { get; set; }
        public virtual DbSet<RoleDb> Role { get; set; }
        public virtual DbSet<UserClientDb> UserClient { get; set; }
        public virtual DbSet<UserRoleDb> UserRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientDbConfig(config));
            modelBuilder.ApplyConfiguration(new UserDbConfig(config));
            modelBuilder.ApplyConfiguration(new RoleDbConfig());
            modelBuilder.ApplyConfiguration(new UserRoleDbConfig());
        }
    }
}