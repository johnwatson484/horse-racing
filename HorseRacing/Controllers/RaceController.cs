using HorseRacing.DAL;
using HorseRacing.Exceptions;
using HorseRacing.Models;
using HorseRacing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HorseRacing.Controllers
{
    public class RaceController : Controller
    {
        HorseRacingContext db;
        ISelection selection;

        public RaceController()
        {
            db = new HorseRacingContext();
            selection = new Selection(db);
        }

        public RaceController(HorseRacingContext context)
        {
            db = context;
            selection = new Selection(db);
        }

        public RaceController(HorseRacingContext context, ISelection selection)
        {
            db = context;
            this.selection = selection;
        }

        // GET: Race
        public ActionResult Index()
        {
            return View(new Race(selection.Get()));
        }

        [HttpPost]
        public ActionResult Index(Race race)
        {
            if (race.IsValid)
            {
                race.Entrants.ForEach(x => x.Race = race);                

                race.Start();

                return View("Result", race);
            }

            throw new RaceException("One horse must be selected");
        }        
    }
}