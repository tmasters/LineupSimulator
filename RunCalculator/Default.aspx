<%@ Page Title="" Language="C#" MasterPageFile="~/BaseballCalculator.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RunCalculator._Default" %>
<asp:content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type ="text/javascript">
        function OnSimulateClient() {
            document.getElementById('simulateDiv').style.display = 'none';
            document.getElementById('waitDiv').style.display = 'inline';
        }

        function OnDoneSimulate() {
            document.getElementById('simulateDiv').style.display = 'inline';
            document.getElementById('waitDiv').style.display = 'none';
        }
    </script>
    <table cellspacing="5px">
    <tr><td style="width: 513px">
    </td><td></td></tr>
    <tr>
    <td>
    <!--------Lineup Area -------------------->
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Label ID="lblErrors" runat="server" ForeColor="Red" />
    <div style="font-size: small"> Enter or select stats for at least 9 players in the line-up below.
    OR you can use the upload option on the right to load a previously saved line-up.
    You can move a player within the line-up by selecting the position from the
    "Order" drop down list. </div>
    <table>
    <tr><td>Order</td><td>Name</td><td>AB</td><td>BB</td><td>1B</td><td>2B</td><td>3B</td><td>HR</td><td>SB</td><td>CS</td></tr>
    <tr>
        <td><asp:DropDownList ID="ddlOrder1" runat="server" 
                onselectedindexchanged="ddlOrder_SelectedIndexChanged"  
                Width="43px"/></td>
        <td><asp:TextBox ID="tbName1" runat="server" Width="135px" /></td>
        <td><asp:TextBox ID="tbAB1" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbBB1" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb1B1" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb2B1" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb3B1" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbHR1" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbSB1" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbCS1" runat="server" Width="30px" /></td>
        <td><asp:Button ID="btnClear1" runat="server" Text="Clear" Height="20px" 
                onclick="btnClear_Click"/></td>
    </tr>
    <tr>
        <td><asp:DropDownList ID="ddlOrder2" runat="server" 
                onselectedindexchanged="ddlOrder_SelectedIndexChanged"  
                Width="43px"/></td>
        <td><asp:TextBox ID="tbName2" runat="server" Width="135px" /></td>
        <td><asp:TextBox ID="tbAB2" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbBB2" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb1B2" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb2B2" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb3B2" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbHR2" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbSB2" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbCS2" runat="server" Width="30px" /></td>
        <td><asp:Button ID="btnClear2" runat="server" Text="Clear" Height="20px" 
                onclick="btnClear_Click"/></td>        
    </tr>
    <tr>
        <td><asp:DropDownList ID="ddlOrder3" runat="server" 
                onselectedindexchanged="ddlOrder_SelectedIndexChanged"  
                Width="43px"/></td>
        <td><asp:TextBox ID="tbName3" runat="server" Width="135px" /></td>
        <td><asp:TextBox ID="tbAB3" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbBB3" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb1B3" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb2B3" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb3B3" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbHR3" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbSB3" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbCS3" runat="server" Width="30px" /></td>
        <td><asp:Button ID="btnClear3" runat="server" Text="Clear" Height="20px" 
                onclick="btnClear_Click"/></td>   
    </tr>
    <tr>
        <td><asp:DropDownList ID="ddlOrder4" runat="server" 
                onselectedindexchanged="ddlOrder_SelectedIndexChanged"  
                Width="43px"/></td>
        <td><asp:TextBox ID="tbName4" runat="server" Width="135px" /></td>
        <td><asp:TextBox ID="tbAB4" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbBB4" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb1B4" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb2B4" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb3B4" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbHR4" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbSB4" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbCS4" runat="server" Width="30px" /></td>
        <td><asp:Button ID="btnClear4" runat="server" Text="Clear" Height="20px" 
                onclick="btnClear_Click"/></td>
    </tr>
    <tr>
        <td><asp:DropDownList ID="ddlOrder5" runat="server" 
                onselectedindexchanged="ddlOrder_SelectedIndexChanged"  
                Width="43px"/></td>
        <td><asp:TextBox ID="tbName5" runat="server" Width="135px" /></td>
        <td><asp:TextBox ID="tbAB5" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbBB5" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb1B5" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb2B5" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb3B5" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbHR5" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbSB5" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbCS5" runat="server" Width="30px" /></td>
        <td><asp:Button ID="btnClear5" runat="server" Text="Clear" Height="20px" 
                onclick="btnClear_Click"/></td>
    </tr>
    <tr>
        <td><asp:DropDownList ID="ddlOrder6" runat="server" 
                onselectedindexchanged="ddlOrder_SelectedIndexChanged"  
                Width="43px"/></td>
        <td><asp:TextBox ID="tbName6" runat="server" Width="135px" /></td>
        <td><asp:TextBox ID="tbAB6" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbBB6" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb1B6" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb2B6" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb3B6" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbHR6" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbSB6" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbCS6" runat="server" Width="30px" /></td>
        <td><asp:Button ID="btnClear6" runat="server" Text="Clear" Height="20px" 
                onclick="btnClear_Click"/></td>
    </tr>
    <tr>
        <td><asp:DropDownList ID="ddlOrder7" runat="server" 
                onselectedindexchanged="ddlOrder_SelectedIndexChanged"  
                Width="43px"/></td>
        <td><asp:TextBox ID="tbName7" runat="server" Width="135px" /></td>
        <td><asp:TextBox ID="tbAB7" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbBB7" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb1B7" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb2B7" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb3B7" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbHR7" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbSB7" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbCS7" runat="server" Width="30px" /></td>
        <td><asp:Button ID="btnClear7" runat="server" Text="Clear" Height="20px" 
                onclick="btnClear_Click"/></td>
    </tr>
    <tr>
        <td><asp:DropDownList ID="ddlOrder8" runat="server" 
                onselectedindexchanged="ddlOrder_SelectedIndexChanged"  
                Width="43px"/></td>
        <td><asp:TextBox ID="tbName8" runat="server" Width="135px" /></td>
        <td><asp:TextBox ID="tbAB8" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbBB8" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb1B8" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb2B8" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb3B8" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbHR8" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbSB8" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbCS8" runat="server" Width="30px" /></td>
        <td><asp:Button ID="btnClear8" runat="server" Text="Clear" Height="20px" 
                onclick="btnClear_Click"/></td>
    </tr>
    <tr>
        <td><asp:DropDownList ID="ddlOrder9" runat="server" 
                onselectedindexchanged="ddlOrder_SelectedIndexChanged"  
                Width="43px"/></td>
        <td><asp:TextBox ID="tbName9" runat="server" Width="135px" /></td>
        <td><asp:TextBox ID="tbAB9" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbBB9" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb1B9" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb2B9" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb3B9" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbHR9" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbSB9" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbCS9" runat="server" Width="30px" /></td>
        <td><asp:Button ID="btnClear9" runat="server" Text="Clear" Height="20px" 
                onclick="btnClear_Click"/></td>
    </tr>
    <tr><td colspan=10><hr /></td></tr>
    <tr>
        <td><asp:DropDownList ID="ddlOrder10" runat="server" 
                onselectedindexchanged="ddlOrder_SelectedIndexChanged"  
                Width="43px"/></td>
        <td><asp:TextBox ID="tbName10" runat="server" Width="135px" /></td>
        <td><asp:TextBox ID="tbAB10" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbBB10" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb1B10" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb2B10" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb3B10" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbHR10" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbSB10" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbCS10" runat="server" Width="30px" /></td>
        <td><asp:Button ID="btnClear10" runat="server" Text="Clear" Height="20px" 
                onclick="btnClear_Click"/></td>
    </tr>
    <tr>
        <td><asp:DropDownList ID="ddlOrder11" runat="server" 
                onselectedindexchanged="ddlOrder_SelectedIndexChanged"  
                Width="43px"/></td>
        <td><asp:TextBox ID="tbName11" runat="server" Width="135px" /></td>
        <td><asp:TextBox ID="tbAB11" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbBB11" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb1B11" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb2B11" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb3B11" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbHR11" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbSB11" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbCS11" runat="server" Width="30px" /></td>
        <td><asp:Button ID="btnClear11" runat="server" Text="Clear" Height="20px" 
                onclick="btnClear_Click"/></td>
    </tr>
    <tr>
        <td><asp:DropDownList ID="ddlOrder12" runat="server" 
                onselectedindexchanged="ddlOrder_SelectedIndexChanged"  
                Width="43px"/></td>
        <td><asp:TextBox ID="tbName12" runat="server" Width="135px" /></td>
        <td><asp:TextBox ID="tbAB12" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbBB12" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb1B12" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb2B12" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tb3B12" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbHR12" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbSB12" runat="server" Width="30px" /></td>
        <td><asp:TextBox ID="tbCS12" runat="server" Width="30px" /></td>
        <td><asp:Button ID="btnClear12" runat="server" Text="Clear" Height="20px" 
                onclick="btnClear_Click"/></td>
    </tr>
    </table>

    For player stats, assume players play 
        <asp:TextBox ID="tbGamesPerSeason" 
            runat="server" Text="150" Width="35px" MaxLength="3" /> games per season. <br />
    <div id="simulateDiv"><asp:Button ID="btnSimulate" runat="server" onclick="btnSimulate_Click" 
        Text="Simulate" Font-Bold="True" Font-Size="Large" 
            onclientclick="OnSimulateClient();" /></div>
     
    <div id="waitDiv" style="display:none"><img src="indicator.gif" />Simulating...</div>
    </td>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <!---------------Right side--------------->
    <td valign=top> 
    <!------Load or Save lineup section------->
    <asp:Panel ID="pnlLoadSave" runat="server" Visible="true">
    <fieldset>
    <legend>Load/Save Line-up</legend>
    <div style="font-size: small">Click the "Save Current Line-up" button and choose "Save" in the pop-up window to store this line-up as a file on your local machine. 
    <br /><asp:Button ID="btnSaveLineup" runat="server" Text="Save Current Line-up" 
            onclick="btnSaveLineup_Click" /> </div><br />
    <div style="font-size: small">Or Browse for a previously saved line-up and click "Upload" to load it as your current line-up.</div>       
    <asp:Button ID="btnLoadLineup" runat="server" Text="Upload: " 
            onclick="btnLoadLineup_Click" /> <asp:FileUpload ID="FileUpload1" runat="server" /><br />
    </fieldset>
    <br />
   </asp:Panel>
   
   <!---------------Stats Area ------------------->
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnSimulate" />
    </Triggers>
    <ContentTemplate>
    <fieldset>
    <legend>Results:</legend>
    <asp:Label ID="lblNoResults" runat="server" Text="Hit the Simulate button for results." />
    <asp:Panel ID="pnlStats" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Avg Runs Per Game:" />
    <asp:Label ID="lblTotalRuns" runat="server" Text="" /><br /><br />
    <asp:Label ID="Label2" runat="server" Text="Runs For 162 Games:" />
    <asp:Label ID="lblSeasonRuns" runat="server" Text="" /><br /><br />
    
    <table><tr><td>Order</td><td>Name</td><td>G</td><td>RBI</td><td>R</td></tr>
    <tr>
        <td>1</td>
        <td><asp:Label ID="lblName1" runat="server"/></td>
        <td><asp:Label ID="lblGames1" runat="server"/></td>
        <td><asp:Label ID="lblRBI1" runat="server"/></td>
        <td><asp:Label ID="lblRuns1" runat="server"/></td>
    </tr>
    <tr>
        <td>2</td>
        <td><asp:Label ID="lblName2" runat="server"/></td>
        <td><asp:Label ID="lblGames2" runat="server"/></td>
        <td><asp:Label ID="lblRBI2" runat="server"/></td>
        <td><asp:Label ID="lblRuns2" runat="server"/></td>
    </tr>
    <tr>
        <td>3</td>
        <td><asp:Label ID="lblName3" runat="server"/></td>
        <td><asp:Label ID="lblGames3" runat="server"/></td>
        <td><asp:Label ID="lblRBI3" runat="server"/></td>
        <td><asp:Label ID="lblRuns3" runat="server"/></td>
    </tr>    
    <tr>
        <td>4</td>
        <td><asp:Label ID="lblName4" runat="server"/></td>
        <td><asp:Label ID="lblGames4" runat="server"/></td>
        <td><asp:Label ID="lblRBI4" runat="server"/></td>
        <td><asp:Label ID="lblRuns4" runat="server"/></td>
    </tr>
    <tr>
        <td>5</td>
        <td><asp:Label ID="lblName5" runat="server"/></td>
        <td><asp:Label ID="lblGames5" runat="server"/></td>
        <td><asp:Label ID="lblRBI5" runat="server"/></td>
        <td><asp:Label ID="lblRuns5" runat="server"/></td>
    </tr>
    <tr>
        <td>6</td>
        <td><asp:Label ID="lblName6" runat="server"/></td>
        <td><asp:Label ID="lblGames6" runat="server"/></td>
        <td><asp:Label ID="lblRBI6" runat="server"/></td>
        <td><asp:Label ID="lblRuns6" runat="server"/></td>
    </tr>    
    <tr>
        <td>7</td>
        <td><asp:Label ID="lblName7" runat="server"/></td>
        <td><asp:Label ID="lblGames7" runat="server"/></td>
        <td><asp:Label ID="lblRBI7" runat="server"/></td>
        <td><asp:Label ID="lblRuns7" runat="server"/></td>
    </tr>    
    <tr>
        <td>8</td>
        <td><asp:Label ID="lblName8" runat="server"/></td>
        <td><asp:Label ID="lblGames8" runat="server"/></td>
        <td><asp:Label ID="lblRBI8" runat="server"/></td>
        <td><asp:Label ID="lblRuns8" runat="server"/></td>
    </tr>
        <tr>
        <td>9</td>
        <td><asp:Label ID="lblName9" runat="server"/></td>
        <td><asp:Label ID="lblGames9" runat="server"/></td>
        <td><asp:Label ID="lblRBI9" runat="server"/></td>
        <td><asp:Label ID="lblRuns9" runat="server"/></td>
    </tr>
    
    </table>
    </asp:Panel>
    </fieldset>
    </td>
    </ContentTemplate>  
    </asp:UpdatePanel>
    
    
    </tr>
    </table>
    </div>
   
</asp:content>

