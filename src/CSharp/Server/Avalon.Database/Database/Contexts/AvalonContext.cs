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

        public DbSet<StageEntity> Stages { get; set; }
        public DbSet<AvalonRoleEntity> AvalonRoles { get; set; }
        public DbSet<AvalonProfileEntity> AvalonProfiles { get; set; }
        public DbSet<OfflineGameEntity> OfflineGames { get; set; }
        public DbSet<OfflineGameProfileRoleEntity> OfflineGameProfileRoles { get; set; }
        public DbSet<OfflineGameMissionProfileEntity> OfflineGameMissionProfiles { get; set; }
        public DbSet<OfflineGameMissionEntity> OfflineGameMissions { get; set; }
        public DbSet<FinishUpGameEntity> FinishUpGames { get; set; }
        public DbSet<AvalonUserFeedbackEntity> AvalonUserFeedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //new AuthenticationDatabaseBuilder().OnModelCreating(modelBuilder);
            base.AutoModelCreating(modelBuilder);
            modelBuilder.Entity<OfflineGameMissionProfileEntity>(e =>
            {
                e.HasKey(x => new { x.OfflineGameMissionId, x.AvalonProfileId });
            });

            modelBuilder.Entity<StageEntity>(x =>
            {
                x.HasData(new StageEntity()
                {
                    Id = 1,
                    Name = "5 Players",
                    PlayerCount = 5,
                    MinionOfMerlinCount = 3,
                    MinionOfMordredCount = 2,
                    Mission1PlayerCount = 2,
                    Mission2PlayerCount = 3,
                    Mission3PlayerCount = 2,
                    Mission4PlayerCount = 3,
                    Mission5PlayerCount = 3,
                },
                new StageEntity()
                {
                    Id = 2,
                    Name = "6 Players",
                    PlayerCount = 6,
                    MinionOfMerlinCount = 4,
                    MinionOfMordredCount = 2,
                    Mission1PlayerCount = 2,
                    Mission2PlayerCount = 3,
                    Mission3PlayerCount = 4,
                    Mission4PlayerCount = 3,
                    Mission5PlayerCount = 4,
                }, new StageEntity()
                {
                    Id = 3,
                    Name = "7 Players",
                    PlayerCount = 7,
                    MinionOfMerlinCount = 4,
                    MinionOfMordredCount = 3,
                    Mission1PlayerCount = 2,
                    Mission2PlayerCount = 3,
                    Mission3PlayerCount = 3,
                    Mission4PlayerCount = 4,
                    Mission5PlayerCount = 4,
                    DoNeedsTwoOfFailsAtMission4 = true
                },
                new StageEntity()
                {
                    Id = 4,
                    Name = "8 Players",
                    PlayerCount = 8,
                    MinionOfMerlinCount = 5,
                    MinionOfMordredCount = 3,
                    Mission1PlayerCount = 3,
                    Mission2PlayerCount = 4,
                    Mission3PlayerCount = 4,
                    Mission4PlayerCount = 5,
                    Mission5PlayerCount = 5,
                    DoNeedsTwoOfFailsAtMission4 = true
                },
                new StageEntity()
                {
                    Id = 5,
                    Name = "9 Players",
                    PlayerCount = 9,
                    MinionOfMerlinCount = 6,
                    MinionOfMordredCount = 3,
                    Mission1PlayerCount = 3,
                    Mission2PlayerCount = 4,
                    Mission3PlayerCount = 4,
                    Mission4PlayerCount = 5,
                    Mission5PlayerCount = 5,
                    DoNeedsTwoOfFailsAtMission4 = true
                },
                new StageEntity()
                {
                    Id = 6,
                    Name = "10 Players",
                    PlayerCount = 10,
                    MinionOfMerlinCount = 6,
                    MinionOfMordredCount = 4,
                    Mission1PlayerCount = 3,
                    Mission2PlayerCount = 4,
                    Mission3PlayerCount = 4,
                    Mission4PlayerCount = 5,
                    Mission5PlayerCount = 5,
                    DoNeedsTwoOfFailsAtMission4 = true
                },
                new StageEntity()
                {
                    Id = 7,
                    Name = "11 Players",
                    PlayerCount = 11,
                    MinionOfMerlinCount = 6,
                    MinionOfMordredCount = 5,
                    Mission1PlayerCount = 3,
                    Mission2PlayerCount = 4,
                    Mission3PlayerCount = 4,
                    Mission4PlayerCount = 5,
                    Mission5PlayerCount = 6,
                    DoNeedsTwoOfFailsAtMission4 = true
                },
                new StageEntity()
                {
                    Id = 8,
                    Name = "12 Players",
                    PlayerCount = 12,
                    MinionOfMerlinCount = 7,
                    MinionOfMordredCount = 5,
                    Mission1PlayerCount = 3,
                    Mission2PlayerCount = 4,
                    Mission3PlayerCount = 4,
                    Mission4PlayerCount = 5,
                    Mission5PlayerCount = 6,
                    DoNeedsTwoOfFailsAtMission4 = true
                },
                new StageEntity()
                {
                    Id = 9,
                    Name = "13 Players",
                    PlayerCount = 13,
                    MinionOfMerlinCount = 7,
                    MinionOfMordredCount = 6,
                    Mission1PlayerCount = 3,
                    Mission2PlayerCount = 4,
                    Mission3PlayerCount = 4,
                    Mission4PlayerCount = 5,
                    Mission5PlayerCount = 7,
                    DoNeedsTwoOfFailsAtMission4 = true
                },
                new StageEntity()
                {
                    Id = 10,
                    Name = "14 Players",
                    PlayerCount = 14,
                    MinionOfMerlinCount = 8,
                    MinionOfMordredCount = 6,
                    Mission1PlayerCount = 3,
                    Mission2PlayerCount = 4,
                    Mission3PlayerCount = 4,
                    Mission4PlayerCount = 5,
                    Mission5PlayerCount = 7,
                    DoNeedsTwoOfFailsAtMission4 = true
                },
                new StageEntity()
                {
                    Id = 11,
                    Name = "15 Players",
                    PlayerCount = 15,
                    MinionOfMerlinCount = 8,
                    MinionOfMordredCount = 7,
                    Mission1PlayerCount = 3,
                    Mission2PlayerCount = 4,
                    Mission3PlayerCount = 4,
                    Mission4PlayerCount = 5,
                    Mission5PlayerCount = 8,
                    DoNeedsTwoOfFailsAtMission4 = true
                });
            });

            modelBuilder.Entity<AvalonRoleEntity>(x =>
            {
                x.HasData(new AvalonRoleEntity()
                {
                    Id = 1,
                    Name = "Merlin",
                    IsMinionOfMordred = false
                },
                new AvalonRoleEntity()
                {
                    Id = 2,
                    Name = "Percival",
                    IsMinionOfMordred = false
                },
                new AvalonRoleEntity()
                {
                    Id = 3,
                    Name = "Mordred",
                    IsMinionOfMordred = true
                },
                new AvalonRoleEntity()
                {
                    Id = 4,
                    Name = "Assassin",
                    IsMinionOfMordred = true
                },
                new AvalonRoleEntity()
                {
                    Id = 5,
                    Name = "Morgana",
                    IsMinionOfMordred = true
                },
                new AvalonRoleEntity()
                {
                    Id = 6,
                    Name = "Oberon",
                    IsMinionOfMordred = true
                },
                new AvalonRoleEntity()
                {
                    Id = 7,
                    Name = "Evil",
                    IsMinionOfMordred = true
                },
                new AvalonRoleEntity()
                {
                    Id = 8,
                    Name = "Evil",
                    IsMinionOfMordred = true
                },
                new AvalonRoleEntity()
                {
                    Id = 9,
                    Name = "Evil",
                    IsMinionOfMordred = true
                },
                new AvalonRoleEntity()
                {
                    Id = 10,
                    Name = "Evil",
                    IsMinionOfMordred = true
                },
                new AvalonRoleEntity()
                {
                    Id = 11,
                    Name = "Evil",
                    IsMinionOfMordred = true
                },
                new AvalonRoleEntity()
                {
                    Id = 12,
                    Name = "Evil",
                    IsMinionOfMordred = true
                },
                new AvalonRoleEntity()
                {
                    Id = 13,
                    Name = "People",
                    IsMinionOfMordred = false
                },
                new AvalonRoleEntity()
                {
                    Id = 14,
                    Name = "People",
                    IsMinionOfMordred = false
                },
                new AvalonRoleEntity()
                {
                    Id = 15,
                    Name = "People",
                    IsMinionOfMordred = false
                },
                new AvalonRoleEntity()
                {
                    Id = 16,
                    Name = "People",
                    IsMinionOfMordred = false
                },
                new AvalonRoleEntity()
                {
                    Id = 17,
                    Name = "People",
                    IsMinionOfMordred = false
                },
                new AvalonRoleEntity()
                {
                    Id = 18,
                    Name = "People",
                    IsMinionOfMordred = false
                },
                new AvalonRoleEntity()
                {
                    Id = 19,
                    Name = "People",
                    IsMinionOfMordred = false
                },
                new AvalonRoleEntity()
                {
                    Id = 20,
                    Name = "People",
                    IsMinionOfMordred = false
                });
            });
        }
    }
}
