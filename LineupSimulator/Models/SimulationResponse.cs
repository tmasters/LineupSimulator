using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineupSimulator.Models
{
    public class SimulationResponse
    {
        public List<String> Errors { get; set; }
        public SimulationResult Result { get; set; }
    }
}