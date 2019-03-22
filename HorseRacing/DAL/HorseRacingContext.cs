using HorseRacing.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Threading.Tasks;

namespace HorseRacing.DAL
{
    public class HorseRacingContext:IdentityDbContext<ApplicationUser>
    {
        public HorseRacingContext():base("HorseRacingContext", throwIfV1Schema:false)
        {
        }

        public virtual DbSet<Horse> Horses { get; set; }

        public static HorseRacingContext Create()
        {
            return new HorseRacingContext();
        }

        public virtual void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}