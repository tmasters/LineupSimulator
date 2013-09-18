/*************************************************************
 * 
 *   File: Player.cs
 *   
 *   Purpose: Contains the definition for a baseball player
 * 
 *   Copyright 2008, Troy Masters
 * 
 * 
 * **********************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace BaseballLineupSimulator
{
    public class Player
    {
        public string UID;      //Unique ID (unused)
        public string Name;     //Name
        public int Position;    //Position (first base, catcher, etc) 1-9
        public int ABs;         //At-Bats (no walks)
        public int Walks;
        public int Singles;
        public int Doubles;
        public int Triples;
        public int Homers;
        public int Steals;
        public int CaughtStealing;

        //Bunting
        public bool UseBuntStats = false;
        public double BuntAttemptPercent = 0;
        public double BuntSuccessPercent = 0;

        //Simulated stats
        public int ActualRBI = 0;
        public int ActualRun = 0;
        
        
        public Player(string uid, string name, int position, int abs, int bbs,
            int singles, int doubles, int triples, int hrs, int steals, int cs)
        {
            this.UID = uid;
            this.Name = name;
            this.Position = position;
            this.ABs = abs;
            this.Walks = bbs;
            this.Singles = singles;
            this.Doubles = doubles;
            this.Triples = triples;
            this.Homers = hrs;
            this.Steals = steals;
            this.CaughtStealing = cs;
        }
        public Player()
        {

        }

        public Player(XmlNode playerNode)
        {
            XmlNode tempNode = playerNode.SelectSingleNode("UID");
            UID = tempNode.InnerText;

            tempNode = playerNode.SelectSingleNode("Name");
            Name = tempNode.InnerText;

            tempNode = playerNode.SelectSingleNode("Position");
            Position = Convert.ToInt32(tempNode.InnerText);

            tempNode = playerNode.SelectSingleNode("ABs");
            ABs = Convert.ToInt32(tempNode.InnerText);

            tempNode = playerNode.SelectSingleNode("Walks");
            Walks = Convert.ToInt32(tempNode.InnerText);

            tempNode = playerNode.SelectSingleNode("Singles");
            Singles = Convert.ToInt32(tempNode.InnerText);

            tempNode = playerNode.SelectSingleNode("Doubles");
            Doubles = Convert.ToInt32(tempNode.InnerText);

            tempNode = playerNode.SelectSingleNode("Triples");
            Triples = Convert.ToInt32(tempNode.InnerText);

            tempNode = playerNode.SelectSingleNode("Homers");
            Homers = Convert.ToInt32(tempNode.InnerText);

            tempNode = playerNode.SelectSingleNode("Steals");
            Steals = Convert.ToInt32(tempNode.InnerText);

            tempNode = playerNode.SelectSingleNode("CaughtStealing");
            CaughtStealing = Convert.ToInt32(tempNode.InnerText);
        }

        public void SetBuntStats(bool bUse, double attempt, double success)
        {
            this.UseBuntStats = bUse;
            this.BuntAttemptPercent = attempt;
            this.BuntSuccessPercent = success;
        }

        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Plate appearances (ABs + walks)
        /// </summary>
        public int PAs
        {
            get
            {
                return this.ABs + this.Walks;
            }
        }

        /// <summary>
        /// Batting average
        /// </summary>
        public double Avg
        {
            get
            {
                return (double)(this.Singles + this.Doubles + this.Triples + this.Homers) / (this.PAs - this.Walks);
            }
        }

        /// <summary>
        /// On-base Percentage
        /// </summary>
        public double OBP
        {
            get
            {
                return (double)(this.Singles + this.Doubles + this.Triples + this.Homers + this.Walks) / (this.PAs);
            }
        }

        /// <summary>
        /// Sluggin percentage
        /// </summary>
        public double SLG
        {
            get
            {
                return (double)(this.Singles + this.Doubles * 2 + this.Triples * 3 + this.Homers * 4) / (this.PAs - this.Walks);
            }
        }

        /// <summary>
        /// Percent of times a runner tries to steal when he is
        /// on first base
        /// </summary>
        public double StealAttemptPercent
        {
            get
            {
                return (this.Steals + this.CaughtStealing) / (double)(this.Walks + this.Singles);
            }
        }

        /// <summary>
        /// Percent success rate of steals
        /// </summary>
        public double StealSuccessPercent
        {
            get
            {
                return (this.Steals) / (double)(this.Steals + this.CaughtStealing);
            }
        }

        /// <summary>
        /// The speed index for this player
        /// </summary>
        public double SpeedIndex
        {
            get
            {
                return Math.Max(100, (this.StealAttemptPercent * this.StealSuccessPercent) / 2000.0);
            }
        }
    }
}
