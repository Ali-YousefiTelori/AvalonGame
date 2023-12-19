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
                e.HasKey(x => new { x.OfflineGameMissionId, x.ProfileId });
            });

            modelBuilder.Entity<UserEntity>(x =>
            {
                x.HasData(new UserEntity()
                {
                    Id = 1,
                    Key = Guid.NewGuid(),
                    UserName = "HelloAvalon"
                });
            });

            modelBuilder.Entity<ProfileEntity>(x =>
            {
                x.HasData(new ProfileEntity()
                {
                    Id = 1,
                    Name = "Ali",
                    UserId = 1
                });
                x.HasData(new ProfileEntity()
                {
                    Id = 2,
                    Name = "Yaghob",
                    UserId = 1
                });
                x.HasData(new ProfileEntity()
                {
                    Id = 3,
                    Name = "Ebrahim",
                    UserId = 1
                });
                x.HasData(new ProfileEntity()
                {
                    Id = 4,
                    Name = "Noah",
                    UserId = 1
                });
                x.HasData(new ProfileEntity()
                {
                    Id = 5,
                    Name = "Mosa",
                    UserId = 1
                });
            });

            modelBuilder.Entity<StageEntity>(x =>
            {
                x.HasData(new StageEntity()
                {
                    Id = 1,
                    Name = "1",
                    PlayerCount = 5
                });
            });
            
            modelBuilder.Entity<RoleEntity>(x =>
            {
                x.HasData(new RoleEntity()
                {
                    Id = 1,
                    Name = "Merlin",
                    IsMinionOfMordred = false
                });
                x.HasData(new RoleEntity()
                {
                    Id = 2,
                    Name = "Persival",
                    IsMinionOfMordred = false
                });
                x.HasData(new RoleEntity()
                {
                    Id = 3,
                    Name = "Mordred",
                    IsMinionOfMordred = true
                });
                x.HasData(new RoleEntity()
                {
                    Id = 4,
                    Name = "Assasin",
                    IsMinionOfMordred = true
                });
                x.HasData(new RoleEntity()
                {
                    Id = 5,
                    Name = "Morgana",
                    IsMinionOfMordred = true
                });
                x.HasData(new RoleEntity()
                {
                    Id = 6,
                    Name = "Oberon",
                    IsMinionOfMordred = true
                });
            });
        }
    }
}
