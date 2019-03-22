using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HorseRacing.Models
{
    [Table("Horses")]
    public class Horse
    {
        public int HorseId { get; set; }

        public string Name { get; set; }

        [Range(0, 10)]
        public int Skill { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public byte[] Image { get; set; }
    }
}