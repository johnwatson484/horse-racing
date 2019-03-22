using System;
using NUnit.Framework;
using HorseRacing.Models;
using Moq;
using HorseRacing.DAL;
using System.Data.Entity;
using System.Collections.Generic;
using HorseRacing.Controllers;
using System.Threading.Tasks;
using System.Linq;

namespace HorseRacing.Tests.Controllers
{
    [TestFixture]
    [Category("Horses Controller")]
    public class HorsesControllerTest
    {
        HorsesController controller;

        Mock<HorseRacingContext> context;

        List<Horse> horseData;
        Mock<DbSet<Horse>> horses;

        [SetUp]
        public void Setup()
        {
            horseData = new List<Horse>();
            horses = new Mock<DbSet<Horse>>().SetupData(horseData);

            context = new Mock<HorseRacingContext>() { CallBase = true };
            context.Setup(x => x.Horses).Returns(horses.Object);
            context.Setup(x => x.SetModified(It.IsAny<object>()));

            controller = new HorsesController(context.Object);
        }

        [Test]
        public async Task Test_New_Horses_Saved()
        {
            var horse = new Horse { Name = "Horse 1" };

            await controller.Create(horse);

            horses.Verify(x => x.Add(horse), Times.Once);
            context.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Test_Horse_Updated()
        {            
            var horse = new Horse { HorseId = 1, Name = "Horse 1" };

            await controller.Edit(horse);

            context.Verify(x => x.SetModified(horse), Times.Once);
            context.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Test_Horse_Deleted()
        {
            var horse = new Horse { HorseId = 1, Name = "Horse 1" };
            horses.Setup(x => x.FindAsync(It.IsAny<int>())).ReturnsAsync(horse);
            
            await controller.DeleteConfirmed(1);

            horses.Verify(x => x.Remove(horse), Times.Once);
            context.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
