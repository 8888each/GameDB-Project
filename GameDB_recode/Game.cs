using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDB_recode
{
    public class Game
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Creator { get; set; }
        public string? Genre { get; set; }
        public DateTime? Release { get; set; }
        public string? Mode { get; set; } = "Single Player";
        public int Sales { get; set; } = 1000;
        public string? Awards { get; set; } = "None";
    }
}
