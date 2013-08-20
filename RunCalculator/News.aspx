<%@ Page Title="" Language="C#" MasterPageFile="~/BaseballCalculator.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="RunCalculator.News" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 8px;">
    <asp:Label ID="Label3" runat="server" Text="Added Save/Load Ability" Font-Bold="True" 
        Font-Size="Large" ForeColor="Red"></asp:Label><br />
    February 25th, 2011 -- I haven't updated this a while, but I noticed quite a few people
    are still using the simulator.  I've removed the 2008 stats since they are no longer of much
    use, and added in the ability to load and save txt files representing the lineups.
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Simulator Update" Font-Bold="True" 
        Font-Size="Large" ForeColor="Red"></asp:Label><br />
     December 7, 2008 -- added the most common lineups from 2008 for each team.
     <br />
     <br />
    <asp:Label ID="Label1" runat="server" Text="Lineup Simulator Beta Launch" Font-Bold="True" 
        Font-Size="X-Large" ForeColor="Red"></asp:Label><br />
     November 22, 2008 -- the Lineup Simulator is available to general public for testing.
    </div>
</asp:Content>
