<%@ Page Title="" Language="C#" MasterPageFile="~/BaseballCalculator.Master" AutoEventWireup="true" CodeBehind="FAQ.aspx.cs" Inherits="RunCalculator.FAQ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="margin: 8px;">
<b>Q: How does the simulator work?</b><br />
<br />
Essentially, the simulator takes the statistics you enter for 
the first 9 players in the line-up and uses them to determine the 
likelihood of various events occurring.  For instance, suppose the ABs 
(including walks) entered for a player is 5, and the 1Bs entered is 2: 
the simulator will suppose that this player will hit a single  40%
(2/5 * 100%) of the time he goes to the plate.  Using this, along with
the other statistical entries for this player (and the 8 other players 
in the line-up), it will then "simulate" 200,000 games.  Note that the simulator
is <b>not</b> using a mathematical formula to estimate the runs scored; rather, it 
actually plays out each of the games according to MLB rules, and stores 
the resulting statistics from each of those games. 
<br />
<br />
<b>Q: What are some common uses for the simulator?</b><br />
<br /> 
The most popular use is to analyze possible trades.  If
you are curious how offensive production will be affected
if Player A is acquired for Player B, you can compare 
the Avg Runs Per Game of the curent line-up against a line-up that
has Player A inserted for Player B.<br /><br />
Another popular use is to compare MVP candidates.  You can compare
line-ups with Candidate A entered vs. Candidate B, and see which
yields more production.  Often times Candidate A might be lauded for
his high RBI number -- with the simulator, you can see how many
RBI Candidate B would have if he batted in Candidate A's line-up.
<br /><br />
Additionally, and the original reason for its creation, was to compare
the order of line-ups.  Back in 2007, Tony LaRussa decided he wanted
to bat the pitcher 8th in the line-up instead of 9th.  The simulator
was programmed to contrast the expected runs in each scenario. <br /><br />
More abstractly, the simulator can be used to examine the effect of
certain statistics on overall production.  For example, is a .250 hitter 
with 50 homeruns more valuable than a .315 hitter with 15 homeruns?  
<br />
<br />
<b>Q: What is the purpose of the three "bench" spots (10-12)?</b>
<br />
<br />
These slots are ignored by the simulator.  They are simply there
for convenience, to allow you to quickly swap players in and out
of the line-up.
<br />
<br />
<b>Q: I recreated the actual line-up of a 2008 team.  Why does 
the simulator say they should have scored more runs than they 
actually did?</b>
<br />
<br />
The simulator runs every game with the line-up entered. Since
players tend to get injured or are just rested throughout the
course of an actual season, this "ideal" line-up is never maintained
throughout the entire 162 games.  The adjustment factor depends on a variety
of complex factors, such as amount of games missed by each player,
and the production of their replacements...statistics that most
users are not interested in entering.  
<br />
<br />
Question not answered?  Please feel free to send an email to contact @ lineupsimulator.com
</div>
</asp:Content>
