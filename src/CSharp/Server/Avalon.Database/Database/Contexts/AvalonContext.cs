using Avalon.Database.Entities;
using Avalon.Database.Entities.Relations;
using EasyMicroservices.Cores.Relational.EntityFrameworkCore;
using EasyMicroservices.Cores.Relational.EntityFrameworkCore.Intrerfaces;
using Microsoft.EntityFrameworkCore;

namespace Avalon.Database.Contexts
{
    public class AvalonContext : RelationalCoreContext
    {
        IEntityFrameworkCoreDatabaseBuilder _builder;
        public AvalonContext(IEntityFrameworkCoreDatabaseBuilder builder)
        {
            _builder = builder;
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<StageEntity> Stages { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<ProfileEntity> Profiles { get; set; }
        public DbSet<OfflineGameEntity> OfflineGames { get; set; }
        public DbSet<OfflineGameProfileRoleEntity> OfflineGameProfileRoles { get; set; }
        public DbSet<OfflineGameMissionProfileEntity> OfflineGameMissionProfiles { get; set; }
        public DbSet<OfflineGameMissionEntity> OfflineGameMissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_builder != null)
                _builder.OnConfiguring(optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.UserName).IsUnique();
            });

            modelBuilder.Entity<StageEntity>(e =>
            {
                e.HasKey(x => x.Id);
            });

            modelBuilder.Entity<RoleEntity>(e =>
            {
                e.HasKey(x => x.Id);
            });

            modelBuilder.Entity<ProfileEntity>(e =>
            {
                e.HasKey(x => x.Id);

                e.HasOne(x => x.User)
                .WithMany(x => x.Profiles)
                .HasForeignKey(x => x.UserId);
            });

            modelBuilder.Entity<OfflineGameEntity>(e =>
            {
                e.HasKey(x => x.Id);

                e.HasOne(x => x.CreatorUser)
                .WithMany(x => x.OfflineGames)
                .HasForeignKey(x => x.CreatorUserId);

                e.HasOne(x => x.Stage)
                .WithMany(x => x.OfflineGames)
                .HasForeignKey(x => x.StageId);
            });

            modelBuilder.Entity<OfflineGameProfileRoleEntity>(e =>
            {
                e.HasKey(x => x.Id);

                e.HasOne(x => x.OfflineGame)
                .WithMany(x => x.OfflineGameProfileRoles)
                .HasForeignKey(x => x.OfflineGameId);

                e.HasOne(x => x.Profile)
                .WithMany(x => x.OfflineGameProfileRoles)
                .HasForeignKey(x => x.ProfileId);

                e.HasOne(x => x.Role)
                .WithMany(x => x.OfflineGameProfileRoles)
                .HasForeignKey(x => x.Roled);
            });

            modelBuilder.Entity<OfflineGameMissionProfileEntity>(e =>
            {
                e.HasKey(x => new { x.OfflineGameMissionId, x.ProfileId});

                e.HasOne(x => x.OfflineGameMission)
                .WithMany(x => x.OfflineGameMissionProfiles)
                .HasForeignKey(x => x.OfflineGameMissionId);

                e.HasOne(x => x.Profile)
                .WithMany(x => x.OfflineGameMissionProfiles)
                .HasForeignKey(x => x.ProfileId);
            });

            modelBuilder.Entity<OfflineGameMissionEntity>(e =>
            {
                e.HasKey(x => x.Id);

                e.HasOne(x => x.OfflineGame)
                .WithMany(x => x.OfflineGameMissions)
                .HasForeignKey(x => x.OfflineGameId);
            });
        }
    }
}
