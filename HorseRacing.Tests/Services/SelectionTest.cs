using HorseRacing.DAL;
using HorseRacing.Models;
using HorseRacing.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace HorseRacing.Tests.Services
{
    [TestFixture]
    [Category("Selection Service")]
    public class SelectionTest
    {
        Selection selection;

        Mock<HorseRacingContext> context;

        Mock<DbSet<Horse>> horses;
        List<Horse> horseData;

        [SetUp]
        public void Setup()
        {
            horseData = new List<Horse>
            {
                new Horse{HorseId = 1, Name = "Horse 1"},
                new Horse{HorseId = 2, Name = "Horse 2"},
                new Horse{HorseId = 3, Name = "Horse 3"},
                new Horse{HorseId = 4, Name = "Horse 4"},
                new Horse{HorseId = 5, Name = "Horse 5"},
                new Horse{HorseId = 6, Name = "Horse 6"},
                new Horse{HorseId = 7, Name = "Horse 7"},
                new Horse{HorseId = 8, Name = "Horse 8"},
                new Horse{HorseId = 9, Name = "Horse 9"},
                new Horse{HorseId = 10, Name = "Horse 11"},
            };
            horses = new Mock<DbSet<Horse>>().SetupData(horseData);

            context = new Mock<HorseRacingContext>() { CallBase = true };
            context.Setup(x => x.Horses).Returns(horses.Object);
            context.Setup(x => x.SetModified(It.IsAny<object>()));

            selection = new Selection(context.Object);
        }

        [Test]
        public void Test_Selection_Returns_Number_Of_Horses_Requested()
        {
            var result = selection.Get(6);

            Assert.AreEqual(6, result.Count);
        }

        [Test]
        public void Test_No_Horses_Duplicated()
        {
            var result = selection.Get(6);

            var duplicates = result.GroupBy(x => x.HorseId).Where(g => g.Count() > 1).Select(y => y.Key).Count();

            Assert.AreEqual(0, duplicates);
        }

        [Test]
        public void Test_Number_Of_Horses_Requested_Does_Not_Exceed_Repository()
        {
            var result = selection.Get(15);

            Assert.AreEqual(10, result.Count);
        }
    }
}
