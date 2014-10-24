using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineupSimulator.Models
{
    public class PlayerResult
    {
        public String Name { get; set; }
        public long MinRBI { get; set; }
        public long MedianRBI { get; set; }
        public long MaxRBI { get; set; }
        public long MinRuns { get; set; }
        public long MedianRuns { get; set; }
        public long MaxRuns { get; set; }
    }

    public class SimulationResult
    {
        public long MinRuns { get; set; }
        public long MedianRuns { get; set; }
        public long MaxRuns { get; set; }
        public List<PlayerResult> PlayerResults { get; set; }

    }
}