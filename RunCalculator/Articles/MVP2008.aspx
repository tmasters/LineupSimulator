<%@ Page Title="" Language="C#" MasterPageFile="~/BaseballCalculator.Master" AutoEventWireup="true" CodeBehind="MVP2008.aspx.cs" Inherits="RunCalculator.MVP2008" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1 {
            width: 114px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="margin: 8px;">
<div style="font-family: 'Times New Roman'; font-size: x-large; font-weight: bold;">Nine Pujols vs. Nine Howards<br />
    </div>
12/01/2008 -- Troy Masters
<p>Let’s face it – the MVP award in baseball is largely based on 
offense. Nothing says this clearer than the top two candidates in 
the NL MVP race -- Albert Pujols and Ryan Howard -- who both play 
perhaps the least valuable defensive position in baseball, first 
base (though to be fair, Pujols plays it quite well).</p>
<img src="PujolsSim.GIF" />
<p>Fortunately with the <a href="../Default.aspx">simulator</a> we can get a pretty good estimate of a player’s offensive value by creating lineups consisting entirely of that single player. I decided to make a lineup consisting of Albert Pujols batting in slots 1-9, and see if it would out-produce a lineup of all Ryan Howards. To further satisfy my curiosity, I decided to take the other NL MVP candidates and see how many runs they would produce in their own homogenous lineups. The simulations I ran are based on their 2008 stats (without adjustment for ballparks):</p>
<center>
<table style="text-align: left;" border=1>
<tr><td class="style1">Player</td><td>Runs Per Game</td></tr>
<tr><td class="style1">M. Ramirez</td><td>13.14*</td></tr>
<tr><td class="style1">A. Pujols</td><td>10.48</td></tr>
<tr><td class="style1">L. Berkman</td><td>8.09</td></tr>
<tr><td class="style1">H. Ramirez</td><td>7.13</td></tr>
<tr><td class="style1">C. Utley</td><td>6.76</td></tr>
<tr><td class="style1">P. Burrell</td><td>6.04</td></tr>
<tr><td class="style1">R. Braun</td><td>5.94</td></tr>
<tr><td class="style1">R. Howard</td><td>5.80</td></tr>
<tr><td class="style1">J .Rollins</td><td>5.62</td></tr>
</table></center>
<h5>*Manny Ramirez’s number based only on the 53 games with the Dodger’s. If you take his entire season, that number goes down to a more down-to-earth 8.67 runs per games.</h5>
<p>Looking at the results above, it’s a darn good thing that Pujols was awarded the MVP over Howard. Pretty much every sabermetrically-inclined writer and/or fan was saying a similar thing, but I couldn’t quite believe just how pronounced the difference was between Howard and Pujols. Manny caught fire (or started trying) for the last two months of the season, yet even with that pulling up his number, he still finished a distant second to Pujols overall on the season. Simply put, Pujols 
    <b>10.48</b> runs per game is amazing, especially when we’ll take a look at the AL MVP race below.</p>
<p>On the other hand, the fact that Howard was even in the race is something of a joke. The Phillies lineup averaged 
    <b>4.93</b> runs per games over the season, less than a run under that of Howard himself – which means that the Philadelphia lineup, even with the likes of Pedro Feliz and Carlos Ruiz and a pitcher, was almost as dangerous as a lineup consisting entirely of Ryan Howards. Clearly, there was somebody else more valuable in that lineup than Howard, so I ran a few more Phillies (as you can see above) through the simulator. What we find is that Utley was almost a full run above Howard, that Burrell upstaged him, and that Rollins – in what many considered a terrible year for him – almost matched his number. Given that Howard doesn’t bring much defensive value, whereas Rollins plays shortstop, one could make the argument that 
    <b>three</b> offensive players were more valuable than Howard in his own lineup.</p>
<p>So what brought Howard’s number down? This has been talked about a good amount elsewhere, but it boils down to this – Howard only hit homeruns<sup>a</sup>. Whereas in his career he has posted a respectable .380 on-base percentage, this year his OBP was under .340.</p>
<p>Even his slugging (.543) was much lower than you’d expect from somebody who hit 48 homeruns, because he only hit 26 doubles, and just didn’t get hits very often. What this means is a lineup consisting of all Ryan Howards hits a lot of solo homeruns. Compare this to someone like Berkman’s .420 OBP and .567 SLG, and it should be obvious that Howard just wasn’t in that super-elite class this year.</p>
<p>Of course, one could make the case that a lineup consisting of only one hitter will favor those with a high OBP more than it should, and undervalues the homerun hitter<sup>b</sup>. So, let’s take a closer look at Howard’s 146 RBI, which is often cited in his MVP case. The following order is what I used for the typical Phillies lineup in 2008 (for the record, Werth was a much better hitter than Jenkins):</p>
<ol>
<li>J. Rollins</li>
<li>S. Victorino</li>
<li>C. Utley</li>
<li>R. Howard</li>
<li>P. Burrell</li>
<li>J. Werth</li>
<li>P. Feliz</li>
<li>C. Ruiz</li>
<li>A. Eaton</li>
</ol>
<p>This simulated lineup yields <b>4.91</b> runs per games, and 134 RBI for Howard. Given that the 4.91 is close to the 4.93 the Phillies actually scored in the season, the fact that Howard’s simulated RBI total is less than his actual total (146) can perhaps be attributed to some sort of clutch (or luck) factor. So he does have that going for him. But if we plug Pujols into Howard’s slot in the lineup, that runs per game jumps up to 
    <b>5.29</b> for the dominating Phillies, and Pujols is suddenly knocking in 130 RBI in his 148 games instead of his 116 that he did with the Cardinals. Put a different way, within the context of the Phillies lineup, Pujols would generate 
    <i><b>62 more runs</b></i> over the course of the season than his closest MVP rival. Congratulations baseball writers of America, you got this one right.</p>
<p>Now, onto the American League.  As with the NL candidates, I simulated a line-up with the same player batting in slots 1-9, and then looked at the resulting runs per game.</p>
<center>
<table style="text-align: left;" border=1>
<tr><td class="style1">Player</td><td>Runs Per Game</td></tr>
<tr><td class="style1">A. Rodriguez</td><td>7.51</td></tr>
<tr><td class="style1">C. Quentin</td><td>7.24</td></tr>
<tr><td class="style1">K. Youkilis</td><td>7.01</td></tr>
<tr><td class="style1">J. Hamilton</td><td>6.39</td></tr>
<tr><td class="style1">D. Pedroia</td><td>6.32</td></tr>
<tr><td class="style1">J. Mauer</td><td>6.30</td></tr>
<tr><td class="style1">J. Morneau</td><td>5.91</td></tr>
</table>
</center>
<p>As you can see, there was nobody that was heads and tails ahead of the pack, though A-Rod posting a 7.51 RPG average over the course of the season is pretty impressive. 
    He tends to get overlooked every season, and this one is no exception. For him to finish 8th in MVP voting is a joke.</p>
<p>Other than that, however, Pedroia’s selection as the MVP is not too much of an outrage. Carlos Quentin missed too many games over the season. Kevin Youkilis might have been a better hitter in his the Red Sox lineup, but once again, a good-hitting first basemen is not nearly as rare as a good-hitting second baseman (all the more reason that Utley should have finished higher in the NL MVP voting).</p>
<p>Speaking of first baseman, Morneau’s second-place finish was ridiculous, especially given that he eclipsed his own teammate – Mauer – when Mauer produced more runs at a much more difficult defensive position.</p>
<p>For the sake of completeness, I did a similar activity to the one I did with the NL MVP, which is to plug in the “better” hitter into another team&#39;s lineup. For the Red Sox, I took the following as the everyday lineup.</p>
<ol>
<li>J. Ellsbury</li>
<li>D. Pedroia</li>
<li>D. Ortiz</li>
<li>M. Ramirez</li>
<li>M. Lowell</li>
<li>J. Drew</li>
<li>K. Youkilis</li>
<li>J. Varitek</li>
<li>J. Lugo</li>
</ol>
<p>When I ran the simulation, this lineup averaged 5.46 runs per game. For 2008, the Red Sox actually averaged 
    <b>5.21</b> runs per game. The adjustment factor, which takes into account things like injuries and the fact that Manny was gone for a third of the season, is therefore .956 (5.21 / 5.46). Subbing A-Rod into Pedroia’s spot in the lineup, and then applying the adjustment factor, results in a 
    <b>5.35</b> runs per game average (compared to the 5.21 of before). Doing the same thing, but subbing A-Rod in for Youkilis instead, results in an average of 
    <b>5.28</b> runs per game – once again affirming the order we see in our original table (that A-Rod is of more value offensively than Youkilis, who is in turn more valuable offensively than Pedroia). And so, within the context of the Red Sox lineup, A-Rod would’ve generated 
    <b>21 more runs</b> over the course of the season than the actual AL MVP. Not nearly the disparity between Howard and Pujols, but still – A-Rod has every right to feel slighted by the voters.</p>
<hr />
<h5><sup>a</sup>Interesting note: if Howard had the same number of singles, doubles, triples, walks, and at-bats as he actually had, and only his number of homeruns changed, he would’ve needed 93 homeruns to match the production of Pujols! I’m pretty sure he still would’ve been voted the MVP if he topped 70 though…</h5>
<h5><sup>b</sup>The simplest example being this: suppose Will Walker walks 90% of the time, and gets out the other 10%. Now suppose Harry Homer hits a homerun 5% of the time, and gets out 95 percent of the time. If we simulate each in his own lineup, Will Walker will score quite a few more runs per game. But if we stick them in a line-up with other players who all never get on base, Harry Homer’s lineup will score more than Will’s.</h5>
</div>
</asp:Content>
