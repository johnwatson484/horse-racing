namespace HorseRacing.Migrations
{
    using HorseRacing.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HorseRacing.DAL.HorseRacingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HorseRacing.DAL.HorseRacingContext context)
        {
            context.Horses.AddOrUpdate(
              p => p.Name,
              new Horse { HorseId = 1, Name = "Bill Pegsby", Skill = 10, Description = "Rock solid." },
              new Horse { HorseId = 2, Name = "Lynx Magnus", Skill = 5, Description = "Old tired horse, addicted to push pops." },
              new Horse { HorseId = 3, Name = "Windy Simon", Skill = 2, Description = "Strong finisher, former derby winner." },
              new Horse { HorseId = 4, Name = "Raging Jayne", Skill = 7, Description = "Prone to sudden bursts of aggression." },
              new Horse { HorseId = 5, Name = "Jeff's Grace", Skill = 4, Description = "Found behind a large supermarket chain looking worried." },
              new Horse { HorseId = 6, Name = "Turn the Sound Down", Skill = 10, Description = "Multiple championship winner." },
              new Horse { HorseId = 7, Name = "Be Mine", Skill = 1, Description = "Often confused for a donkey." },
              new Horse { HorseId = 8, Name = "Britannia", Skill = 9, Description = "All hail." }
            );

            context.Roles.AddOrUpdate(
                p => p.Name,
                    new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin" }
                );            
        }
    }
}
