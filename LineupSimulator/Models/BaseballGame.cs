/*************************************************************
 * 
 *   File: BaseballGame.cs
 *   
 *   Purpose: Contains the logic for simulating 
 *              one side (offensive) of a baseball game
 * 
 *   Copyright 2008, Troy Masters
 * 
 * 
 * **********************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace LineupSimulator.Models
{
    class BaseballGame
    {
        List<Player> Lineup;            //The lineup to simulate
        Player[] Bases = new Player[3]; //Reference to the player on each of the bases
        int Inning = 0;                 //Current inning
        int Runs = 0;                   //Amount of runs scored this game
        int AtBat = 0;                  //Index of the batter currently hitting
        int Outs = 0;                   //Number of outs in the inning
        //Constants
        const double PercentBuntSuccess = .7;
        Baserunning baserunning = new Baserunning();  //Baserunning object to calculate base-running stats
        Random RandomGenerator = new Random(0);       //Random number generator, start seed at 0

        public BaseballGame(List<Player> lineup)
        {
            this.Lineup = lineup;
        }

        /// <summary>
        /// Simulates one game
        /// </summary>
        /// <returns>number of runs the lineup scores in the game</returns>
        public int PlayBall()
        {
	        InitGame();  //Reset everything for the game
	        this.AtBat = -1;
            while (this.Inning <= 9) //Loop for simulating all 9 innings
	        {   
		        this.AtBat++;
		        if (this.AtBat > 8)
			        this.AtBat = 0;
		        
                //Consider possible steal
                if (this.Outs < 3) CheckSteal();
		        
                //Should we go to the next inning?
                if (this.Outs >= 3)
		        {
			        ResetInning();
			        this.Inning++;
			        if (this.Inning > 9)
				        break;
		        }
        		
                //Action number determines what happens for this batter
		        double actionNum =  RandomGenerator.NextDouble();

                
		        //Bunt
                /*
		        if (this.Outs < 2 && this.Lineup[this.AtBat].Position == 1 && (this.Bases[0] != null || this.Bases[1] != null || this.Bases[2] != null))
		        {
			        Bunt();
			        continue;
		        } */

		        //WALK
		        if (actionNum < this.Lineup[this.AtBat].Walks / this.Lineup[this.AtBat].PAs)
		        {
			        Walk();
			        continue;
		        }
                actionNum -= this.Lineup[this.AtBat].Walks / this.Lineup[this.AtBat].PAs;
        		
		        //SINGLE
		        if (actionNum < this.Lineup[this.AtBat].Singles / this.Lineup[this.AtBat].PAs)
		        {
			        Single();
			        continue;
		        }
		        actionNum -= this.Lineup[this.AtBat].Singles / this.Lineup[this.AtBat].PAs;
        		
		        //DOUBLE
		        if (actionNum < this.Lineup[this.AtBat].Doubles / this.Lineup[this.AtBat].PAs)
		        {
			        Double();
			        continue;
		        }
		        actionNum -= this.Lineup[this.AtBat].Doubles / this.Lineup[this.AtBat].PAs;
        		
		        //TRIPLE
		        if (actionNum < this.Lineup[this.AtBat].Triples  / this.Lineup[this.AtBat].PAs)
		        {
			        Triple();
			        continue;
		        }
		        actionNum -= this.Lineup[this.AtBat].Triples / this.Lineup[this.AtBat].PAs;
        		
		        //HOME RUN
		        if (actionNum < this.Lineup[this.AtBat].Homers / this.Lineup[this.AtBat].PAs)
		        {
			        Homer();
			        continue;
		        }
        	
		        //Otherwise, an OUT
		        Out();
	        }
	        return this.Runs;
        }

        /// <summary>
        /// Reset everything for the game
        /// </summary>
        private void InitGame()
        {
	        ResetInning();
	        this.Inning = 1;
	        this.Runs = 0;
	        this.AtBat = 0;
            this.Outs = 0;
            //Clear out runs / RBI stats
            foreach (Player player in Lineup)
            {
                player.ActualRBI = player.ActualRun = 0;
            }
        }

        /// <summary>
        /// Clear bases and outs
        /// </summary>
        private void ResetInning()
        {
	        for (int i=0; i < 3; i++)
		        this.Bases[i] = null;
	        this.Outs = 0;
        }

        /// <summary>
        /// Perform logic for making an out
        /// TODO: Consider fly-outs for some of the time
        /// </summary>
        private void Out()
        {
	        this.Outs++;
        	
            //Force if there is a runner on first 
	        bool bForce = (this.Bases[0] != null);
	       
	        //Force outs
	        if (this.Bases[2] != null && this.Bases[1] != null && this.Bases[0] != null)
	        {
		        //Force out at home
		        this.Bases[2] = this.Bases[1];
		        this.Bases[1] = this.Bases[0];
		        this.Bases[0] = this.Lineup[this.AtBat];
	        }
	        else if (this.Bases[1] != null && this.Bases[0] != null)
	        {
		        //Force out at third
		        this.Bases[2] = null;
		        this.Bases[1] = this.Bases[0];
		        this.Bases[0] = this.Lineup[this.AtBat];
	        }
	        else if (this.Bases[0] != null)  
	        {
		        //Force out at second
		        this.Bases[1] = null;
		        this.Bases[0] = this.Lineup[this.AtBat];
	        }

	        //Consider possibility of double-play
	        if (bForce)
	        {
                if (RandomGenerator.NextDouble() < Baserunning.DoublePlayPercent)
		        {
			        this.Outs++;
			        this.Bases[0] = null;
		        }
	        }

	        //Return if 3 or more outs
	        if (this.Outs >= 3)
	        {
		        return;
	        }

	        //Check for guy moving third to home
	        if (this.Bases[2] != null && !bForce)
	        {
                if (RandomGenerator.NextDouble() < Baserunning.PercentMoveThirdToHome)
		        {   //Actually scores from third
			        //Update stats
                    this.Bases[2].ActualRun++;
                    this.Lineup[this.AtBat].ActualRBI++;

                    //Update state
                    this.Bases[2] = null;
			        this.Runs++;	
		        }
                else if (RandomGenerator.NextDouble() < Baserunning.PercentThrownOutAdvancing)
		        {
			        //Thrown out trying to score
			        this.Bases[2] = null;
			        this.Outs++;
		        }
	        }

	        //See if this guy moves second to third
	        if (this.Bases[1] != null && !bForce)
	        {
                if (RandomGenerator.NextDouble() < Baserunning.PercentMoveThirdToHome)
		        {
			        //Makes it from second to third
			        this.Bases[2] = this.Bases[1];
			        this.Bases[1] = null;
		        }
                else if (RandomGenerator.NextDouble() < Baserunning.PercentThrownOutAdvancing)
		        {
			        //Thrown out trying to advance
			        this.Bases[1] = null;
			        this.Outs++;
		        }
	        }
        }

        /// <summary>
        /// Simulate a walk
        /// </summary>
        private void Walk()
        {
	        this.PushOneBase(0, this.Lineup[this.AtBat]);
        }

        /// <summary>
        /// Push the player specified to the base specified (0=1st base, 1=2nd base, etc)
        /// </summary>
        /// <param name="index">the base to push to</param>
        /// <param name="hitter">the player to move</param>
        private void PushOneBase(int index, Player hitter)
        {
	        if (index > 2)
	        {   //A player walks home
                //Update stats
                this.Bases[2].ActualRun++;
                this.Lineup[this.AtBat].ActualRBI++;

		        this.Runs++;
	        }
	        else if (this.Bases[index] == null)
		        this.Bases[index] = hitter; //Can simply put the player on the base
	        else
	        {   //The base is already occupied, push another play forward
		        PushOneBase(index+1, this.Bases[index]);
		        this.Bases[index] = hitter;
	        }
        }

        /// <summary>
        /// Simulate a single
        /// </summary>
        private void Single()
        {
	        if (this.Bases[2] != null) //Scores from third
	        {
                //Update stats
                this.Bases[2].ActualRun++;
                this.Lineup[this.AtBat].ActualRBI++;

                //Update state
                this.Bases[2] = null;
		        this.Runs++;
	        }
	        if (this.Bases[1] != null) 
	        {
                //Runner on second with this single
                Baserunning.Outcome outcome = baserunning.SingleWithRunnerOnSecond(RandomGenerator.NextDouble(), 
                    this.Outs, (int) this.Bases[1].SpeedIndex);
                if (outcome == Baserunning.Outcome.AdvanceExtraBase) //Scores from second
		        {   //Runner scores from second

                    //Update Stats
                    this.Bases[1].ActualRun++;
                    this.Lineup[this.AtBat].ActualRBI++;
                    
                    //Update state
                    this.Bases[1] = null;
			        this.Runs++;
		        }
                else if (outcome == Baserunning.Outcome.ThrownOut)
		        {
			        //Runner thrown out advancing
			        this.Bases[1] = null;
			        this.Outs++;
		        }
		        else  //Only goes to third
                {
			        this.Bases[2] = this.Bases[1];
			        this.Bases[1] = null;
		        }
	        }
	        if (this.Bases[0] != null) //Runner on first 
	        {
                if (this.Bases[2] == null) //Third is clear, attempt to advance maybe?
                {
                    //Runner on second with this single
                    Baserunning.Outcome outcome = baserunning.SingleWithRunnerOnFirst(RandomGenerator.NextDouble(),
                        this.Outs, (int) this.Bases[0].SpeedIndex);
                    if (outcome == Baserunning.Outcome.AdvanceExtraBase) 
                    {
                        //Advance to third
                        this.Bases[2] = this.Bases[0];
                    }
                    else if (outcome == Baserunning.Outcome.ThrownOut)
                    {
                        //Thrown OUT at third.
                        this.Outs++;
                    }
                    else  //Only go to second
                    {
                        this.Bases[1] = this.Bases[0];
                    }
                }
                else
                {   
                    //Just go to second
                    //sOutput += this.Bases[0].sName + " advances to second.  ";
                    this.Bases[1] = this.Bases[0];
                }
	        }

	        //Put the hitter that just signled onto first
	        this.Bases[0] = this.Lineup[this.AtBat];
        }

        /// <summary>
        /// Simulate a double
        /// </summary>
        private void Double()
        {
	        if (this.Bases[2] != null) //Scores from third
            {
                //Update Stats
                this.Bases[2].ActualRun++;
                this.Lineup[this.AtBat].ActualRBI++;
               
                //Update state
                this.Bases[2] = null;
		        this.Runs++;
	        }
	        if (this.Bases[1] != null) //Scores from second
	        {
                //Update Stats
                this.Bases[1].ActualRun++;
                this.Lineup[this.AtBat].ActualRBI++;

                //Update state
                this.Bases[1] = null;
		        this.Runs++;
	        }
	        if (this.Bases[0] != null) 
	        {
                //Consider event when runner on first with a double
                Baserunning.Outcome outcome = baserunning.DoubleWithRunnerOnFirst(RandomGenerator.NextDouble(),
                    this.Outs, (int)this.Bases[0].SpeedIndex);
                if (outcome == Baserunning.Outcome.AdvanceExtraBase) //Scores from first
		        {
                    //Update Stats
                    this.Bases[0].ActualRun++;
                    this.Lineup[this.AtBat].ActualRBI++;

                    //Update state
                    this.Bases[0] = null;
			        this.Runs++;
		        }
                else if (outcome == Baserunning.Outcome.ThrownOut)
		        {
			        //Thrown out trying to advance
			        this.Bases[0] = null;
			        this.Outs++;
		        }
		        else  //First to third
		        {
			        //Only takes the normal two bases
			        this.Bases[2] = this.Bases[0];
			        this.Bases[0] = null;
		        }
        		
	        }

	        //Place the batter at second
	        this.Bases[1] = this.Lineup[this.AtBat];
        }

        /// <summary>
        /// Simulate a triple
        /// </summary>
        private void Triple()
        {
	        if (this.Bases[2] != null) //Scores from third
	        {
                //Update Stats
                this.Bases[2].ActualRun++;
                this.Lineup[this.AtBat].ActualRBI++;
                
                this.Bases[2] = null;
		        this.Runs++;
	        }
	        if (this.Bases[1] != null) //Scores from second
	        {
                //Update Stats
                this.Bases[1].ActualRun++;
                this.Lineup[this.AtBat].ActualRBI++;
                
                this.Bases[1] = null;
		        this.Runs++;
	        }
	        if (this.Bases[0] != null) //scores from first
	        {
                //Update Stats
                this.Bases[0].ActualRun++;
                this.Lineup[this.AtBat].ActualRBI++;
                
                this.Bases[0] = null;
		        this.Runs++;
	        }

	        //Place batter on third
	        this.Bases[2] = this.Lineup[this.AtBat];
        }


        /// <summary>
        /// Simulate a homerun
        /// </summary>
        private void Homer()
        {
            //Runners from all the bases will score
	        if (this.Bases[2] != null) //Scores from third
	        {
                //Update Stats
                this.Bases[2].ActualRun++;
                this.Lineup[this.AtBat].ActualRBI++;
                
                this.Bases[2] = null;
		        this.Runs++;
	        }
	        if (this.Bases[1] != null) //Scores from second
	        {
                //Update Stats
                this.Bases[1].ActualRun++;
                this.Lineup[this.AtBat].ActualRBI++;
                
                this.Bases[1] = null;
		        this.Runs++;
	        }
	        if (this.Bases[0] != null) //scores from first
	        {
                //Update Stats
                this.Bases[0].ActualRun++;
                this.Lineup[this.AtBat].ActualRBI++;

                this.Bases[0] = null;
		        this.Runs++;
	        }

            //Update Stats
            this.Lineup[this.AtBat].ActualRun++;
            this.Lineup[this.AtBat].ActualRBI++;

	        this.Runs++;  //Add run for current batter
        }

        /// <summary>
        /// Simulate a bunt
        /// </summary>
        private void Bunt()
        {
	        this.Outs++;
	        int nLeadRunner=2;
	        while (this.Bases[nLeadRunner] == null)
		        nLeadRunner--;
            if (RandomGenerator.NextDouble() < BaseballGame.PercentBuntSuccess)
	        {
		        //Bunt succeeds, move runner(s) over
		        PushOneBase(nLeadRunner+1, this.Bases[nLeadRunner]);
		        this.Bases[nLeadRunner] = null;
	        }
	        else
	        {
                //Bunt fails, lead runner thrown out
                this.Bases[nLeadRunner] = null;
		        this.Bases[0] = this.Lineup[this.AtBat];
	        }
        }

        /// <summary>
        /// Consider stealing, and possibly do so
        /// </summary>
        private void CheckSteal()
        {
	        //Consider stealing if nobody on third 
	        if (this.Bases[2] == null && (this.Bases[1] != null || this.Bases[0] != null))
	        {
		        bool bSteal;
                if (this.Bases[1] != null)
                    bSteal = RandomGenerator.NextDouble() < this.Bases[1].StealAttemptPercent;
                else
                    bSteal = RandomGenerator.NextDouble() < this.Bases[0].StealAttemptPercent;

		        //Steal second to third
		        if (this.Bases[1] != null && bSteal)
		        {
                    bool bStealSuccess = RandomGenerator.NextDouble() < this.Bases[1].StealSuccessPercent;
			        if (bStealSuccess)
			        {
				        /*sOutput += this.Bases[1].sName + " steals third. ";
				        if (this.Bases[0] != null)
					        sOutput += this.Bases[0].sName + " steals second."; */
				        this.Bases[2] = this.Bases[1];
				        this.Bases[1] = this.Bases[0];
				        this.Bases[0] = null;
			        }
			        else
			        {
				        //sOutput += this.Bases[1].sName + " OUT trying to steal third.";
				        this.Bases[1] = this.Bases[0];
				        this.Bases[0] = null;
				        this.Outs++;
			        }
		        }
		        else if (this.Bases[0] != null && bSteal)
		        {
                    bool bStealSuccess = RandomGenerator.NextDouble() < this.Bases[0].StealSuccessPercent;
			        if (bStealSuccess)
			        {
				        //sOutput += this.Bases[0].sName + " steals second.";
				        this.Bases[1] = this.Bases[0];
				        this.Bases[0] = null;
			        }
			        else
			        {
				        //sOutput += this.Bases[0].sName + " OUT trying to steal second.";
				        this.Bases[0] = null;
				        this.Outs++;
			        }
		        }
	        }	
        }
    }
}
