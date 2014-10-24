using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineupSimulator.Models
{
    public class FullPlayerEntryModel
    {
        public string Name { get; set; }     //Name9
        public int AB { get; set; }       //At-Bats (no walks)
        public int BB { get; set; }
        public int H { get; set; }
        public int Double { get; set; }
        public int Triple { get; set; }
        public int HR { get; set; }
        public int SB { get; set; }
        public int CS { get; set; }
    }
}