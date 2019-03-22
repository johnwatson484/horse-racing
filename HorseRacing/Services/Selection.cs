using HorseRacing.DAL;
using HorseRacing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorseRacing.Services
{
    public class Selection : ISelection
    {
        HorseRacingContext db;

        public Selection(HorseRacingContext context)
        {
            db = context;
        }

        public List<Horse> Get(int total = 6)
        {
            return db.Horses.OrderBy(x => Guid.NewGuid()).Take(total).ToList();
        }
    }
}