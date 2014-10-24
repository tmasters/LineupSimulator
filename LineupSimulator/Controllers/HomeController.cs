using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LineupSimulator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Return a list of errors for this line-up input (if any found)
        /// </summary>
        /// <param name="lineup"></param>
        /// <returns></returns>
        protected List<String> ValidateEntries(List<Models.FullPlayerEntryModel> lineup)
        {
            var errors = new List<String>();

            if (lineup == null)
            {
                errors.Add("No players sent.");
                return errors;
            }

            //Ensure enough in the lineup
            if (lineup.Count < 9)
                errors.Add("Not enough players (9 required)");

            //Validate individual player entries    
            List<double> obps = new List<double>();
            for (int i = 0; i < Math.Min(9, lineup.Count); i++)
            {   //Make sure player has > 0 at-bats
                if (lineup[i].AB + lineup[i].BB <= 0)
                {
                    errors.Add(lineup[i].Name + " must have more than 0 plate appearances.");
                }
                else if (lineup[i].H > lineup[i].AB)
                {   //Make sure player's OBP is less than 1.000
                    errors.Add(lineup[i].Name + " cannot have an average greater than 1.000");
                }
                else
                    obps.Add((lineup[i].H + lineup[i].BB) / (double)(lineup[i].AB + lineup[i].BB));
            }
            if (obps.Count > 0 && obps.Average() > 0.7)
            {
                errors.Add("Average OBP for line-up is greater than .700");
            }

            return errors;
        }

        /// <summary>
        /// Perform simulation using "full" entries
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SimulateFull(List<Models.FullPlayerEntryModel> players)
        {
            var response = new LineupSimulator.Models.SimulationResponse()
            {
                Errors = ValidateEntries(players)
            };

            if (response.Errors.Count == 0)
            {

                //Create the lineup
                var lineup = new List<Models.Player>();
                for (int i = 0; i < 9; i++)
                    lineup.Add(new Models.Player(players[i]));

                //Perform actual simulation
                response.Result = LineupSimulator.Models.Simulator.RunSimulation(lineup);
            }

            return Json(response);
        }
    }
}
