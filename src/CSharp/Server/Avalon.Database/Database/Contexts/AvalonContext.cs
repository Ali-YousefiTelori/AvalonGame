using Avalon.Database.Entities;
using Avalon.Database.Entities.Relations;
using EasyMicroservices.Cores.Relational.EntityFrameworkCore;
using EasyMicroservices.Cores.Relational.EntityFrameworkCore.Intrerfaces;
using Microsoft.EntityFrameworkCore;

namespace Avalon.Database.Contexts
{
    public class AvalonContext : RelationalCoreContext
    {
        public AvalonContext(IEntityFrameworkCoreDatabaseBuilder builder) : base(builder) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<StageEntity> Stages { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<ProfileEntity> Profiles { get; set; }
        public DbSet<OfflineGameEntity> OfflineGames { get; set; }
        public DbSet<OfflineGameProfileRoleEntity> OfflineGameProfileRoles { get; set; }
        public DbSet<OfflineGameMissionProfileEntity> OfflineGameMissionProfiles { get; set; }
        public DbSet<OfflineGameMissionEntity> OfflineGameMissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.AutoModelCreating(modelBuilder);

            modelBuilder.Entity<OfflineGameMissionProfileEntity>(e =>
            {
                e.HasKey(x => new { x.OfflineGameMissionId, x.ProfileId});
            });
        }
    }
}
