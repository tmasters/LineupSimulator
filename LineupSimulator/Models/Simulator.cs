using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineupSimulator.Models
{
    class Simulator
    {
        static int NUM_SEASONS = 401; //Number of seasons to simulate

        public static SimulationResult RunSimulation(List<Player> lineup)
        {
            BaseballGame game = new BaseballGame(lineup);
            var runsPerSeason = new List<long>();
            var playerRunsPerSeason = new long[9][];
            var playerRBIPerSeason = new long[9][];
            for (int pIndex = 0; pIndex < 9; pIndex++)
            {
                playerRunsPerSeason[pIndex] = new long[NUM_SEASONS];
                playerRBIPerSeason[pIndex] = new long[NUM_SEASONS];
            }

            //Simulate NUM_SEASONS seasons
            for (int season = 0; season < NUM_SEASONS; season++)
            {
                //Simulate this season
                long totalRuns = 0;
                for (int gameInd = 0; gameInd < 162; gameInd++)
                {
                    totalRuns += game.PlayBall();
                    for (int pIndex = 0; pIndex < 9; pIndex++)
                    {
                        playerRunsPerSeason[pIndex][season] += lineup[pIndex].ActualRun;
                        playerRBIPerSeason[pIndex][season] += lineup[pIndex].ActualRBI;
                    }
                }
                runsPerSeason.Add(totalRuns);
            }

            //Now determine average stats
            var result = new SimulationResult()
            {
                MinRuns = Percentile(runsPerSeason, 0.16),
                MedianRuns = Percentile(runsPerSeason, 0.5),
                MaxRuns = Percentile(runsPerSeason, 0.84),
                PlayerResults = new List<PlayerResult>()
            };
            //For each of the players
            for (int i = 0; i < 9; i++)
            {
                result.PlayerResults.Add(new PlayerResult()
                {
                   Name = lineup[i].Name,
                   MinRuns = Percentile(playerRunsPerSeason[i], 0.16),
                   MedianRuns = Percentile(playerRunsPerSeason[i], 0.5),
                   MaxRuns = Percentile(playerRunsPerSeason[i], 0.84),
                   MinRBI = Percentile(playerRBIPerSeason[i], 0.16),
                   MedianRBI = Percentile(playerRBIPerSeason[i], 0.5),
                   MaxRBI = Percentile(playerRBIPerSeason[i], 0.84)
                });
            }
            return result;
        }

        /// <summary>
        /// Return the specified percentile in the list
        /// </summary>
        /// <param name="input"></param>
        /// <param name="percentile"></param>
        /// <returns></returns>
        protected static long Percentile(IEnumerable<long> input, double percentile)
        {
            var sortedList = new List<long>(input);
            sortedList.Sort();
            return sortedList[(int) (percentile * sortedList.Count)];
        }
    }
}