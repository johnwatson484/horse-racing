using HorseRacing.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HorseRacing.Tests.Models
{
    [TestFixture]
    [Category("Race")]
    public class RaceTest
    {
        Race race;

        [SetUp]
        public void Setup()
        {
            race = new Race(new List<Horse>
            {
                new Horse { HorseId = 1, Name = "Horse 1", Skill = 10},
                new Horse { HorseId = 2, Name = "Horse 2", Skill = 9},
                new Horse { HorseId = 3, Name = "Horse 3", Skill = 8},
                new Horse { HorseId = 4, Name = "Horse 4", Skill = 7},
                new Horse { HorseId = 5, Name = "Horse 5", Skill = 6},
                new Horse { HorseId = 6, Name = "Horse 6", Skill = 5},
                new Horse { HorseId = 7, Name = "Horse 7", Skill = 4},
            });
        }

        [Test]
        public void Test_Add_Entrant()
        {
            Race race = new Race();
            Entrant entrant = new Entrant(race, new Horse { Name = "Horse 1", Skill = 10 }, 1);
                        
            race.AddEntrant(entrant.Horse);

            Assert.AreEqual(1, race.Entrants.Count);
        }

        [Test]
        public void Test_Race_Numbers_Unique()
        {
            var duplicates = race.Entrants.GroupBy(x => x.Number).Where(g => g.Count() > 1).Select(y => y.Key).Count();

            Assert.AreEqual(0, duplicates);
        }

        [Test]
        public void Test_All_Horses_Placed()
        {
            race.Start();

            Assert.IsTrue(race.Entrants[0].Position > 0);
            Assert.IsTrue(race.Entrants[1].Position > 0);
            Assert.IsTrue(race.Entrants[2].Position > 0);
            Assert.IsTrue(race.Entrants[3].Position > 0);
            Assert.IsTrue(race.Entrants[4].Position > 0);
            Assert.IsTrue(race.Entrants[5].Position > 0);
            Assert.IsTrue(race.Entrants[6].Position > 0);
        }

        [Test]
        public void Test_Every_Position_Unique()
        {
            race.Start();

            var duplicates = race.Entrants.GroupBy(x => x.Position).Where(g => g.Count() > 1).Select(y => y.Key).Count();

            Assert.AreEqual(0, duplicates);
        }

        [Test]
        public void Test_Every_Entrant_Has_Odds()
        {
            var result = race.Entrants.Where(x => x.Odds == 0).Count();

            Assert.AreEqual(0, result);
        }
        
        [Test]
        public void Test_Odds_Within_Range_Min()
        {
            var result = race.Entrants.Where(x => x.Odds <= 0).Count();

            Assert.AreEqual(0, result);
        }

        [Test]
        public void Test_Higher_Skill_Equals_Lower_Odds()
        {
            race = new Race(new List<Horse>
            {
                new Horse { HorseId = 1, Name = "Horse 1", Skill = 10},
                new Horse { HorseId = 2, Name = "Horse 2", Skill = 5},
                new Horse { HorseId = 3, Name = "Horse 3", Skill = 4}
            });           

            Assert.Less(race.Entrants.Where(x=>x.Horse.HorseId == 1).First().Odds, race.Entrants.Where(x => x.Horse.HorseId == 2).First().Odds);
            Assert.Less(race.Entrants.Where(x => x.Horse.HorseId == 2).First().Odds, race.Entrants.Where(x => x.Horse.HorseId == 3).First().Odds);
        }
    }
}
