/*************************************************************
 * 
 *   File: Baseballrunning.cs
 *   
 *   Purpose: Contains the logic for determining whether
 *              to take an extra base, and if the runner
 *              gets thrown out or not
 * 
 *   Copyright 2008, Troy Masters
 * 
 * 
 * **********************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseballLineupSimulator
{
    class Baserunning
    {
        //First part refers to number of outs
        //Second part 0=chances of advancing extra base, 1= chances of thrown out advancing
        double[,] FirstToThirdOutcomes = new double[3, 2];
        double[,] SecondToHomeOutcomes = new double[3, 2];
        double[,] FirstToHomeOutcomes = new double[3, 2];

        double dLowest = .71; //Mutliplier for attempt percentage
        double dHighest = 1.67; //Multiplier to attempt percentage

        //Constants for baserunning
        public const double PercentMoveThirdToHome = .7;
        public const double DoublePlayPercent = .25;
        public const double PercentThrownOutAdvancing = .04;

        public enum Outcome
        {
            AdvanceNormal,
            AdvanceExtraBase,
            ThrownOut
        };

        public Baserunning()
        {
            //Initialize the arrays
            FirstToThirdOutcomes[0,0] = .28;
            FirstToThirdOutcomes[1,0] = .30;
            FirstToThirdOutcomes[2,0] = .32;
            FirstToThirdOutcomes[0,1] = .01;
            FirstToThirdOutcomes[1,1] = .01;
            FirstToThirdOutcomes[2,1] = .01;

            SecondToHomeOutcomes[0,0] = .45;
            SecondToHomeOutcomes[1,0] = .60;
            SecondToHomeOutcomes[2,0] = .82;
            SecondToHomeOutcomes[0,1] = .01;
            SecondToHomeOutcomes[1,1] = .05;
            SecondToHomeOutcomes[2,1] = .05;

            FirstToHomeOutcomes[0,0] = .36;
            FirstToHomeOutcomes[1,0] = .39;
            FirstToHomeOutcomes[2,0] = .59;
            FirstToHomeOutcomes[0,1] = .01;
            FirstToHomeOutcomes[1,1] = .03;
            FirstToHomeOutcomes[2,1] = .05;
        }

        /// <summary>
        /// A single occurs with runner on first
        /// </summary>
        /// <param name="random">a random number (0-99)</param>
        /// <param name="outs">number of outs</param>
        /// <param name="speedIndex">speed index of the player</param>
        /// <returns>the outcome of the event</returns>
        public Outcome SingleWithRunnerOnFirst(int random, int outs, int speedIndex)
        {
            if (outs > 2)
                return Outcome.ThrownOut;

            if (random < FirstToThirdOutcomes[outs,0] * 100)
                return Outcome.AdvanceExtraBase;
            else if (random < (FirstToThirdOutcomes[outs,0] + FirstToThirdOutcomes[outs,1]) * 100)
                return Outcome.ThrownOut;
            else
                return Outcome.AdvanceNormal;
        }

        /// <summary>
        /// Simulate event of a single with a runner on second
        /// </summary>
        /// <param name="random">a random number (0-99)</param>
        /// <param name="outs">number of outs</param>
        /// <param name="speedIndex">speed index of the player</param>
        /// <returns>the outcome of the event</returns>
        public Outcome SingleWithRunnerOnSecond(int random, int outs, int speedIndex)
        {
            if (random < SecondToHomeOutcomes[outs,0] * 100)
                return Outcome.AdvanceExtraBase;
            else if (random < (SecondToHomeOutcomes[outs,0] + SecondToHomeOutcomes[outs, 1]) * 100)
                return Outcome.ThrownOut;
            else
                return Outcome.AdvanceNormal;
        }

        /// <summary>
        /// Simulate event of baserunner on first with a double hit
        /// </summary>
        /// <param name="random">a random number (0-99)</param>
        /// <param name="outs">number of outs</param>
        /// <param name="speedIndex">speed index of the player</param>
        /// <returns>the outcome of the event</returns>
        public Outcome DoubleWithRunnerOnFirst(int random, int outs, int speedIndex)
        {
            if (random < FirstToHomeOutcomes[outs,0] * 100)
                return Outcome.AdvanceExtraBase;
            else if (random < (FirstToHomeOutcomes[outs,0] + FirstToHomeOutcomes[outs, 1]) * 100)
                return Outcome.ThrownOut;
            else
                return Outcome.AdvanceNormal;
        }
    }
}
