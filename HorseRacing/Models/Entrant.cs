using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HorseRacing.Models
{
    public class Entrant
    {
        public Race Race { get; set; }

        public int Number { get; set; }

        public Horse Horse { get; set; }

        public bool Selected { get; set; }

        public int Position { get; set; }

        public decimal Stake { get; set; }

        public decimal Returns
        {
            get
            {
                return Stake * Odds;
            }
        }

        private int raceTime = 15;

        public int RaceTime
        {
            get
            {
                return raceTime;
            }
        }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Odds
        {
            get
            {
                return Race == null || Race.Rating == 0 ? 0 : 1 / (decimal.Divide(Horse.Skill, Race.Rating));
            }
        }

        public Entrant() {}

        public Entrant(Race race, Horse horse, int number)
        {
            Race = race;
            Number = number;
            Horse = horse;
            Selected = false;
        }

        public void Select()
        {
            Selected = true;
        }

        public void Deselect()
        {
            Selected = false;
        }

        public void SetPosition(int position)
        {
            Position = position;
        }

        public void Start()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            raceTime = raceTime - Horse.Skill + random.Next(11);
        }
    }
}