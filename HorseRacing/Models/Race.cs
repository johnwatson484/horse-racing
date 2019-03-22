using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HorseRacing.Models
{
    public class Race
    {
        public List<Entrant> Entrants { get; set; }

        public int Rating
        {
            get
            {
                return Entrants.Sum(x => x.Horse.Skill);
            }
        }

        public Race()
        {
            Entrants = new List<Entrant>();
        }

        public Race(List<Horse> horses) : this()
        {
            foreach (var horse in horses)
            {
                AddEntrant(horse);
            }
        }

        public int HorsesSelected
        {
            get
            {
                return Entrants.Where(x => x.Selected).Count();
            }
        }

        public bool IsValid
        {
            get
            {
                return HorsesSelected == 1 ? true : false;
            }
        }

        public bool IsWin
        {
            get
            {
                return Entrants.Where(x => x.Selected).FirstOrDefault()?.Position == 1 ? true : false;
            }                
        }

        public decimal Returns
        {
            get
            {
                return Entrants.Where(x => x.Position == 1).Select(x => x.Returns).FirstOrDefault();
            }
        }

        public void AddEntrant(Horse horse)
        {
            Entrant entrant = new Entrant(this, horse, Entrants.Count + 1);
            Entrants.Add(entrant);
            Entrants = Entrants.OrderBy(x => x.Odds).ToList();
        }

        public void Start()
        {
            int position = 1;

            foreach (var entrant in Entrants)
            {
                entrant.Start();
            }

            Entrants = Entrants.OrderBy(x => x.RaceTime).ToList();

            foreach (var entrant in Entrants)
            {
                entrant.SetPosition(position);
                position++;
            }

            Entrants = Entrants.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}