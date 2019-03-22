using System;
using NUnit.Framework;
using HorseRacing.Controllers;
using HorseRacing.DAL;
using Moq;
using HorseRacing.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using HorseRacing.Exceptions;

namespace HorseRacing.Tests.Controllers
{
    [TestFixture]
    [Category("Race Controller")]
    public class RaceControllerTest
    {
        RaceController controller;

        Mock<HorseRacingContext> context;

        List<Horse> horseData;
        Mock<DbSet<Horse>> horses;

        [SetUp]
        public void Setup()
        {
            horseData = new List<Horse>
            {
                new Horse{HorseId = 1, Name = "Horse 1", Skill = 10 },
                new Horse{HorseId = 2, Name = "Horse 2", Skill = 9 },
                new Horse{HorseId = 3, Name = "Horse 3", Skill = 8 },
                new Horse{HorseId = 4, Name = "Horse 4", Skill = 7 },
                new Horse{HorseId = 5, Name = "Horse 5", Skill = 6 },
                new Horse{HorseId = 6, Name = "Horse 6", Skill = 5 },
                new Horse{HorseId = 7, Name = "Horse 7", Skill = 4 },
                new Horse{HorseId = 8, Name = "Horse 8", Skill = 3 },
                new Horse{HorseId = 9, Name = "Horse 9", Skill = 2 },
                new Horse{HorseId = 10, Name = "Horse 10", Skill = 1 }
            };
            horses = new Mock<DbSet<Horse>>().SetupData(horseData);

            context = new Mock<HorseRacingContext>() { CallBase = true };
            context.Setup(x => x.Horses).Returns(horses.Object);
            context.Setup(x => x.SetModified(It.IsAny<object>()));

            controller = new RaceController(context.Object);
        }

        [Test]
        public void Test_Race_Has_Entrants()
        {
            var result = controller.Index() as ViewResult;

            var race = (Race)result.ViewData.Model;

            Assert.Greater(race.Entrants.Count, 0);
        }

        [Test]
        public void Test_Horse_Selected_Does_Not_Error()
        {
            var race = new Race(horseData);

            race.Entrants[0].Select();

            Assert.DoesNotThrow(() => controller.Index(race));
        }

        [Test]
        public void Test_No_Horse_Selected_Does_Error()
        {
            var race = new Race(horseData);

            Assert.Throws<RaceException>(() => controller.Index(race));
        }

        [Test]
        public void Test_Only_One_Horse_Selected()
        {
            var race = new Race(horseData);

            race.Entrants[0].Select();
            race.Entrants[1].Select();

            Assert.Throws<RaceException>(() => controller.Index(race));
        }
    }
}

