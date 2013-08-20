/*************************************************************
 * 
 *   File: Default.aspx.cs
 *   
 *   Purpose: Contains the logic for the "Simulator" page
 * 
 *   Copyright 2008, Troy Masters
 * 
 * 
 * **********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;
using BaseballLineupSimulator;
using System.Xml;

namespace RunCalculator
{
    public partial class _Default : System.Web.UI.Page
    {
        string NoTeam = "(None)";  //The string for representing No team in the list
        string AnyTeam = "(Any)";  //The string for representing Any team in the list
        string FileHeader = "LineupSimulator.com FileVersion=1.0"; //Header text for saving/loading lineup
        ContentPlaceHolder thisContent = null; //The content template associated with this page

        
        protected void Page_Load(object sender, EventArgs e)
        {
            //Set the content template to this page
            thisContent = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
            
            if (!IsPostBack) //Loading page for the first time
            {   
                this.pnlStats.Visible = false;  //Hide results
                this.lblNoResults.Visible = true; //Show "Hit simulate for results..." message

                //Load the team list:
                this.LoadTeamList();
                //this.ddlTeams.SelectedIndex = 0;

                //Set the properties for the "Order" controls
                for (int i = 1; i <= 12; i++)
                {
                    DropDownList list = (DropDownList)thisContent.FindControl("ddlOrder" + i.ToString());
                    for (int j = 1; j <= 12; j++)
                        list.Items.Add(j.ToString());
                    list.AutoPostBack = true;
                    list.SelectedIndex = i - 1;
                }
            }
        }

        /// <summary>
        /// The user changes the selection in the Order drop-down list
        /// to change the batting order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList) sender;
            int index = Char.IsDigit(ddl.ID[ddl.ID.Length-2]) ? 
                Convert.ToInt32(ddl.ID.Substring(ddl.ID.Length-2)) 
                : Convert.ToInt32(ddl.ID.Substring(ddl.ID.Length-1));
            
            //Read all the players as enter
            List<Player> cellData = new List<Player>();
            for (int i = 1; i <= 12; i++)
            {
                cellData.Add(this.ReadPlayerData(i));
            }

            //Move the chosen player
            Player movingPlayer = cellData[index-1];
            cellData.RemoveAt(index-1);
            cellData.Insert(ddl.SelectedIndex, movingPlayer);

            //Write the players back out
            for (int i=1; i<= 12; i++)
            {
                this.WritePlayerData(i, cellData[i - 1]);
            }

            //Reset the "Order" controls
            for (int i = 1; i <= 12; i++)
            {
                DropDownList list = (DropDownList)thisContent.FindControl("ddlOrder" + i.ToString());
                list.SelectedIndex = i - 1;
            }
        }

        /// <summary>
        /// The user hits the "simulate" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSimulate_Click(object sender, EventArgs e)
        {
            //Make sure to get that Simulate button back
            ScriptManager.RegisterStartupScript(Page, GetType(), "doneSimulate", "OnDoneSimulate();", true);

            double gamesToSimulate = 200000.0;
            
            //Read our lineup from their entry 
            List<Player> lineup = new List<Player>();
            for (int i = 1; i <= 9; i++)
                lineup.Add(this.ReadPlayerData(i));

            //Validate the lineup
            if (!this.ValidateEntries(lineup))
                return;
            else
                this.lblErrors.Text = "";
            
            //Simulate all the games
            BaseballGame game = new BaseballGame(lineup);
            long totalRuns = 0;
            for (int i = 0; i < gamesToSimulate; i++)
                totalRuns += game.PlayBall();

            //Display the results in the right pane area
            this.pnlStats.Visible = true;
            this.lblNoResults.Visible = false;
            double avgRuns = totalRuns / gamesToSimulate;
            this.lblTotalRuns.Text = avgRuns.ToString("N2");
            int seasonRuns = (int) (avgRuns * 162);
            this.lblSeasonRuns.Text = seasonRuns.ToString();

            //Display the player stats as well 
            int gamesPerSeason;
            if (!Int32.TryParse(this.tbGamesPerSeason.Text, out gamesPerSeason))
                gamesPerSeason = 0;
            for (int i = 0; i < lineup.Count; i++)
            {
                long rbi = (long) (lineup[i].ActualRBI * gamesPerSeason / gamesToSimulate);
                long runs = (long) (lineup[i].ActualRun * gamesPerSeason / gamesToSimulate);
                ((Label)thisContent.FindControl("lblName" + (i+1).ToString())).Text = lineup[i].Name;
                ((Label)thisContent.FindControl("lblGames" + (i+1).ToString())).Text = gamesPerSeason.ToString();
                ((Label)thisContent.FindControl("lblRBI" + (i+1).ToString())).Text = rbi.ToString();
                ((Label)thisContent.FindControl("lblRuns" + (i+1).ToString())).Text = runs.ToString();
            }
        }

        /// <summary>
        /// Validate the line-up, writing out any errors to lblErrors
        /// </summary>
        /// <param name="lineup">the line-up entered by the user</param>
        /// <returns>true if line-up is okay, false otherwise</returns>
        protected bool ValidateEntries(List<Player> lineup)
        {
            bool bResult = true;
            string errorText = "";
                   
            //Validate individual player entries    
            for (int i = 0; i < lineup.Count; i++)
            {   //Make sure player has > 0 at-bats
                if (lineup[i].Total_ABs <= 0)
                {
                    errorText += "*Batter " + (i + 1).ToString() + " must have more than 0 At-Bats<br>";
                    bResult = false;
                }
                else if (lineup[i].OBP > 1.0)
                {   //Make sure player's OBP is less than 1.000
                    errorText += "*Batter " + (i + 1).ToString() + " has an On-Base percentage greater than 1.000<br>";
                    bResult = false;
                }              
            }
            
            //Now validate team entry if necessary
            if (bResult)
            {   //Make sure average obp is less than or equal to .700
                double totalOBP = 0.0;
                for (int i = 0; i < lineup.Count; i++)
                {
                    totalOBP += lineup[i].OBP;
                }
                if (totalOBP / 9.0 > 0.7)
                {
                    errorText += "*Average OBP for line-up is greater than .700<br>";
                    bResult = false;
                }
            }

            if (!bResult)
            {   //Display the errors
                this.lblErrors.Text = "Invalid input(s):<br>" + errorText;
            }

            return bResult;
        }

        /// <summary>
        /// Write out the player data to the specified controls
        /// </summary>
        /// <param name="index">the order in the line-up (1-12)</param>
        /// <param name="player">the player data to write</param>
        protected void WritePlayerData(int index, Player player)
        {
            ((TextBox)thisContent.FindControl("tbName" + index.ToString())).Text = player.Name;
            ((TextBox)thisContent.FindControl("tbAB" + index.ToString())).Text = player.ABs.ToString(); ;
            ((TextBox)thisContent.FindControl("tbBB" + index.ToString())).Text = player.Walks.ToString();
            ((TextBox)thisContent.FindControl("tb1B" + index.ToString())).Text = player.Singles.ToString();
            ((TextBox)thisContent.FindControl("tb2B" + index.ToString())).Text = player.Doubles.ToString();
            ((TextBox)thisContent.FindControl("tb3B" + index.ToString())).Text = player.Triples.ToString();
            ((TextBox)thisContent.FindControl("tbHR" + index.ToString())).Text = player.Homers.ToString();
            ((TextBox)thisContent.FindControl("tbSB" + index.ToString())).Text = player.Steals.ToString();
            ((TextBox)thisContent.FindControl("tbCS" + index.ToString())).Text = player.CaughtStealing.ToString();
        }

        /// <summary>
        /// Read the player data at the specified index
        /// </summary>
        /// <param name="index">the order in the line-up (1-12)</param>
        /// <returns>the player data as read</returns>
        protected Player ReadPlayerData(int index)
        {
            Player player = new Player();
            player.Name = ((TextBox)thisContent.FindControl("tbName" + index.ToString())).Text;
            if (!Int32.TryParse(((TextBox)thisContent.FindControl("tbAB" + index.ToString())).Text, out player.ABs))
                player.ABs = 0;
            if (!Int32.TryParse(((TextBox)thisContent.FindControl("tbBB" + index.ToString())).Text, out player.Walks))
                player.Walks = 0;
            if (!Int32.TryParse(((TextBox)thisContent.FindControl("tb1B" + index.ToString())).Text, out player.Singles))
                player.Singles = 0;
            if (!Int32.TryParse(((TextBox)thisContent.FindControl("tb2B" + index.ToString())).Text, out player.Doubles))
                player.Doubles = 0;
            if (!Int32.TryParse(((TextBox)thisContent.FindControl("tb3B" + index.ToString())).Text, out player.Triples))
                player.Triples = 0;
            if (!Int32.TryParse(((TextBox)thisContent.FindControl("tbHR" + index.ToString())).Text, out player.Homers))
                player.Homers = 0;
            if (!Int32.TryParse(((TextBox)thisContent.FindControl("tbSB" + index.ToString())).Text, out player.Steals))
                player.Steals = 0;
            if (!Int32.TryParse(((TextBox)thisContent.FindControl("tbCS" + index.ToString())).Text, out player.CaughtStealing))
                player.CaughtStealing = 0;

            return player;
        }

        /// <summary>
        /// Based on the team, fill up the available player listbox
        /// </summary>
        /// <param name="team">the team (or Any or None)</param>
        protected void LoadPlayerListbox(string team)
        {
            /*this.lbLoadPlayers.Items.Clear(); //Clear player list

            if (team == NoTeam) //List should remain empty
                return;

            OdbcConnection playerDB = new OdbcConnection(this.ConnectString);
            playerDB.Open();

            //First load our 2008 lineups
            if (team != this.AnyTeam)
            {
                string SQL1 = "SELECT ID, PlayerStats.LastName, FirstName, AtBats, BattingOrder FROM PlayerStats, Lineups WHERE ID = PlayerID AND Lineups.Team = '" + team + "' ORDER BY BattingOrder";
                int order = 1;
                OdbcCommand cmd1 = new OdbcCommand(SQL1, playerDB);
                OdbcDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    int id = reader1.GetInt32(0);
                    string lastName1 = reader1.GetString(1);
                    string firstName1 = reader1.GetString(2);
                    int atBats1 = reader1.GetInt32(3);
                    string displayName1 = order.ToString() + ") " + lastName1 + ", " + firstName1;
                    if (atBats1 >= 300)
                        displayName1 += "*";
                   this.lbLoadPlayers.Items.Add(new ListItem(displayName1, id.ToString()));
                   order++;
                }
                this.lbLoadPlayers.Items.Add(new ListItem("-------------------------", ""));
                reader1.Close();
                cmd1.Dispose();
            }


            //Load the remaining players alphabetically
            List<ListItem> playerList = new List<ListItem>();

            //Load players from the team from the database into a temporary list
            
            string SQL = "SELECT ID, LastName, FirstName, AtBats FROM PlayerStats";
            if (team != this.AnyTeam)
                SQL += " WHERE Team = '" + team + "'";
            OdbcCommand cmd = new OdbcCommand(SQL, playerDB);
            OdbcDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string lastName = reader.GetString(1);
                string firstName = reader.GetString(2);
                int atBats = reader.GetInt32(3);
                string displayName = lastName + ", " + firstName;
                if (atBats >= 300)
                    displayName += "*";

                playerList.Add(new ListItem(displayName, id.ToString()));              
            }       
            reader.Close();
            cmd.Dispose();
            playerDB.Close();

            //Sort the items in our list
            Comparison<ListItem> comparer = new Comparison<ListItem>(this.CompareListItems);
            playerList.Sort(comparer);

            //Write them into our actual listbox
            foreach (ListItem li in playerList)
                this.lbLoadPlayers.Items.Add(li);
            */
        }

        /// <summary>
        /// Helper function used to compare to list-items
        /// (just alphabetically)
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns></returns>
        int CompareListItems(ListItem item1, ListItem item2)
        {
            return String.Compare(item1.Text, item2.Text);
        }

        /// <summary>
        /// Load the list of teams
        /// </summary>
        protected void LoadTeamList()
        {
            /*this.ddlTeams.Items.Clear();
            this.ddlTeams.Items.Add(NoTeam);
            this.ddlTeams.Items.Add(AnyTeam);
            
            //Get the remaining team options from the database
            List<string> teamList = new List<string>();
            OdbcConnection playerDB = new OdbcConnection(this.ConnectString);
            playerDB.Open();
            string SQL = "SELECT DISTINCT Team FROM PlayerStats";
            OdbcCommand cmd = new OdbcCommand(SQL, playerDB);
            OdbcDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                teamList.Add(reader.GetString(0));
                
            }
            reader.Close();
            cmd.Dispose();
            playerDB.Close();

            teamList.Sort(); //Sort the team list

            //Actually write out the list back into the combo-box
            foreach (string team in teamList)
                this.ddlTeams.Items.Add(team); */
        }

        /// <summary>
        /// User selects a different team
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.LoadPlayerListbox(ddlTeams.SelectedItem.Text);
        }

        /// <summary>
        /// Players chooses to add a selection of players to the lineup below
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLoadPlayers_Click(object sender, EventArgs e)
        {
            //Open the database
            /*OdbcConnection playerDB = new OdbcConnection(this.ConnectString);
            playerDB.Open();
            for (int i=0; i < this.lbLoadPlayers.Items.Count; i++)
            {
                if (this.lbLoadPlayers.Items[i].Selected)
                {   //Player to load, selected by user

                    //Find next available slot to put him in
                    int availableSlot = 1;
                    while (!IsPlayerSlotEmpty(availableSlot))
                    {
                        availableSlot++;
                        //No slots left available, no use loading anymore
                        if (availableSlot > 12)
                        {
                            playerDB.Close();
                            return;
                        }
                    }

                    //Actually load the player
                    Player player = this.LoadPlayer(playerDB, this.lbLoadPlayers.Items[i].Value);
                    if (player != null)
                        this.WritePlayerData(availableSlot, player);

                }
            }
            playerDB.Close(); */
        }

        /// <summary>
        /// Determine if a play slot is empty or not
        /// </summary>
        /// <param name="index">lineup spot (1-12)</param>
        /// <returns>true if it is empty, false otherwise</returns>
        protected bool IsPlayerSlotEmpty(int index)
        {
            Player player = this.ReadPlayerData(index);
            if (player.Name.Trim().Length != 0)
                return false;
            if (player.Total_ABs > 0 || player.Walks > 0 || player.Singles > 0 || player.Doubles > 0 || player.Triples > 0 ||
                player.Homers > 0 || player.Steals > 0 || player.CaughtStealing > 0)
                return false;
            return true;
        }

        /// <summary>
        /// Load a player from the database based on his ID
        /// </summary>
        /// <param name="playerDB">the open DB connection</param>
        /// <param name="id">the ID of the player in the database</param>
        /// <returns>the player data from the database</returns>
        protected Player LoadPlayer(OdbcConnection playerDB, string id)
        {
            Player player = null;
            if (id.Length == 0)
                return player;

            string SQL = "SELECT LastName, FirstName, AtBats, Hits, Doubles, Triples, Homeruns, Walks, StolenBases, CaughtStealing, HBP FROM PlayerStats WHERE ID = '" + id + "'";

            OdbcCommand cmd = new OdbcCommand(SQL, playerDB);
            OdbcDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                player = new Player();
                string lastName = reader.GetString(0);
                string firstName = reader.GetString(1);
                int ABs = reader.GetInt32(2);
                int hits = reader.GetInt32(3);
                int doubles = reader.GetInt32(4);
                int triples = reader.GetInt32(5);
                int homers = reader.GetInt32(6);
                int bbs = reader.GetInt32(7);
                int sb = reader.GetInt32(8);
                int cs = reader.GetInt32(9);
                int hbp = reader.GetInt32(10);
                player.ABs = ABs;
                player.Singles = hits - doubles - triples - homers;
                player.Doubles = doubles;
                player.Triples = triples;
                player.Homers = homers;
                player.Walks = bbs + hbp;
                player.Steals = sb;
                player.CaughtStealing = cs;
                player.Name = firstName + " " + lastName;
            }
            reader.Close();
            cmd.Dispose();
            return player;
        }

        /// <summary>
        /// User presses one of the "clear" buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            //Discover which one was pressed
            Button btn = (Button)sender;
            int index = Char.IsDigit(btn.ID[btn.ID.Length - 2]) ?
                Convert.ToInt32(btn.ID.Substring(btn.ID.Length - 2))
                : Convert.ToInt32(btn.ID.Substring(btn.ID.Length - 1));
            
            //Clear the appropriate player slot            
            ((TextBox)thisContent.FindControl("tbName" + index.ToString())).Text = "";
            ((TextBox)thisContent.FindControl("tbAB" + index.ToString())).Text = "";
            ((TextBox)thisContent.FindControl("tbBB" + index.ToString())).Text = "";
            ((TextBox)thisContent.FindControl("tb1B" + index.ToString())).Text = "";
            ((TextBox)thisContent.FindControl("tb2B" + index.ToString())).Text = "";
            ((TextBox)thisContent.FindControl("tb3B" + index.ToString())).Text = "";
            ((TextBox)thisContent.FindControl("tbHR" + index.ToString())).Text = "";
            ((TextBox)thisContent.FindControl("tbSB" + index.ToString())).Text = "";
            ((TextBox)thisContent.FindControl("tbCS" + index.ToString())).Text = "";
        }

        /// <summary>
        /// User chooses to "Upload" a lineup file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLoadLineup_Click(object sender, EventArgs e)
        {
            if (this.FileUpload1.FileContent.Length > 10000)
            {   //First make sure we don't bother if the lineup file is too large
                this.lblErrors.Text = "Invalid lineup file.";
                this.FileUpload1.FileContent.Close();
                return;
            } 
            
            //Convert bytes to string
            byte[] buffer = new byte[this.FileUpload1.FileContent.Length];
            this.FileUpload1.FileContent.Read(buffer, 0, buffer.Length);
            String inputData = "";
            foreach (byte data in buffer)
            {
                inputData += (char)data;
            }

            if (!inputData.StartsWith(this.FileHeader))
                this.lblErrors.Text = "Invalid lineup file.";
            else
            {
                this.lblErrors.Text = "";

                //Separate by lines and tabs
                String[] lines = inputData.Replace("\r\n", "\n").Split('\n');
                for (int nLine = 1; nLine < lines.Length; nLine++)
                {
                    if (lines[nLine] == null || lines[nLine].Length == 0)
                        continue;
                    String[] cols = lines[nLine].Split('\t');
                    for (int nCol = 0; nCol < cols.Length; nCol++)
                    {
                        switch (nCol)
                        {
                            case 0: 
                                ((TextBox)thisContent.FindControl("tbName" + nLine.ToString())).Text = cols[nCol];
                                break;
                            case 1:
                                ((TextBox)thisContent.FindControl("tbAB" + nLine.ToString())).Text = cols[nCol];
                                break;
                            case 2:
                                ((TextBox)thisContent.FindControl("tbBB" + nLine.ToString())).Text = cols[nCol];
                                break;
                            case 3:
                                ((TextBox)thisContent.FindControl("tb1B" + nLine.ToString())).Text = cols[nCol];
                                break;
                            case 4:
                                ((TextBox)thisContent.FindControl("tb2B" + nLine.ToString())).Text = cols[nCol];
                                break;
                            case 5:
                                ((TextBox)thisContent.FindControl("tb3B" + nLine.ToString())).Text = cols[nCol];
                                break;
                            case 6:
                                ((TextBox)thisContent.FindControl("tbHR" + nLine.ToString())).Text = cols[nCol];
                                break;
                            case 7:
                                ((TextBox)thisContent.FindControl("tbSB" + nLine.ToString())).Text = cols[nCol];
                                break;
                            case 8:
                                ((TextBox)thisContent.FindControl("tbCS" + nLine.ToString())).Text = cols[nCol];
                                break;
                        }
                    }
                }
            }
            this.FileUpload1.FileContent.Close();  //Close the stream
        }

        /// <summary>
        /// User chooses to save the lineup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveLineup_Click(object sender, EventArgs e)
        {
            String toSave = this.FileHeader + "\n";
            for (int index = 1; index <= 12; index++)
            {   //Add each of the players's stats
                toSave += ((TextBox)thisContent.FindControl("tbName" + index.ToString())).Text;
                toSave += "\t" + ((TextBox)thisContent.FindControl("tbAB" + index.ToString())).Text;
                toSave += "\t" + ((TextBox)thisContent.FindControl("tbBB" + index.ToString())).Text;
                toSave += "\t" + ((TextBox)thisContent.FindControl("tb1B" + index.ToString())).Text;
                toSave += "\t" + ((TextBox)thisContent.FindControl("tb2B" + index.ToString())).Text;
                toSave += "\t" + ((TextBox)thisContent.FindControl("tb3B" + index.ToString())).Text;
                toSave += "\t" + ((TextBox)thisContent.FindControl("tbHR" + index.ToString())).Text;
                toSave += "\t" + ((TextBox)thisContent.FindControl("tbSB" + index.ToString())).Text;
                toSave += "\t" + ((TextBox)thisContent.FindControl("tbCS" + index.ToString())).Text;
                if (index != 12)
                    toSave += "\n";
            }

            //Convert string to bytes
            byte[] btFile = new byte[toSave.Length];
            for (int i = 0; i < toSave.Length; i++)
                btFile[i] = (byte) toSave[i];

            String dateTimeString = DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString() + 
                DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            
            //Send it back to the user
            Response.AddHeader("Content-disposition", "attachment; filename=LineupSimulator_" + dateTimeString + ".txt");
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(btFile);
            Response.End();
        }

    }
}
