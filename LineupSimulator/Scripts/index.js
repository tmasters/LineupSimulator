// index.js
//   copyright Troy Masters 2014
//   Controllers the interaction for the "Full Simulator" page
//
var curMoving = -1; //Index of player currently selected for "MOVE"
var NUM_SPOTS = 9;
var playerResults = null; //Contains the last PlayerResults we received from simulation

//Click on "move" or "here"
function moveclick(index) {
    if (curMoving == -1) { //Clicked Move
        curMoving = index;
        for (var i = 0; i < NUM_SPOTS; i++) {
            if (index != i) {
                $("#move" + i).removeClass('btn-primary');
                $("#move" + i).addClass('btn-success');
                $("#move" + i).html('Here');
            } else {
                $("#move" + i).addClass('active');
            }
        }
    } else { //Place "Here"
        var playerMoving = getStats(curMoving + 1);
        var playerAtSpot = getStats(index+1);
        setStats(curMoving + 1, playerAtSpot.Name, playerAtSpot.AB, playerAtSpot.H, playerAtSpot.Double, playerAtSpot.Triple, playerAtSpot.HR, playerAtSpot.BB, playerAtSpot.SB, playerAtSpot.CS);
        setStats(index + 1, playerMoving.Name, playerMoving.AB, playerMoving.H, playerMoving.Double, playerMoving.Triple, playerMoving.HR, playerMoving.BB, playerMoving.SB, playerMoving.CS);
        for (var i = 0; i < NUM_SPOTS; i++) {
            $("#move" + i).removeClass('active');
            $("#move" + i).addClass('btn-primary');
            $("#move" + i).removeClass('btn-success');
            $("#move" + i).html('Move');
        }
        curMoving = -1;
    }
}

//Clear out a particular row
function removePlayer(index) {
    $("#name" + (index).toString()).val('');
    $("#ab" + (index).toString()).val('');
    $("#bb" + (index).toString()).val('');
    $("#h" + (index).toString()).val('');
    $("#2b" + (index).toString()).val('');
    $("#3b" + (index).toString()).val('');
    $("#hr" + (index).toString()).val('');
    $("#sb" + (index).toString()).val('');
    $("#cs" + (index).toString()).val('');
}

//Set stats for a particular position
function setStats(pos, name, ab, h, double, triple, hr, bb, sb, cs) {
    $("#name" + (pos-1).toString()).val(name);
    $("#ab" + (pos - 1).toString()).val(ab);
    $("#bb" + (pos - 1).toString()).val(bb);
    $("#h" + (pos - 1).toString()).val(h);
    $("#2b" + (pos - 1).toString()).val(double);
    $("#3b" + (pos - 1).toString()).val(triple);
    $("#hr" + (pos - 1).toString()).val(hr);
    $("#sb" + (pos - 1).toString()).val(sb);
    $("#cs" + (pos - 1).toString()).val(cs);
}

//Get stats for a particular position
function getStats(pos) {
    var player = {};
    player.Name = $("#name" + (pos - 1).toString()).val();
    player.AB = parseInt($("#ab" + (pos - 1).toString()).val());
    player.BB = parseInt($("#bb" + (pos - 1).toString()).val());
    player.H = parseInt($("#h" + (pos - 1).toString()).val());
    player.Double = parseInt($("#2b" + (pos - 1).toString()).val());
    player.Triple = parseInt($("#3b" + (pos - 1).toString()).val());
    player.HR = parseInt($("#hr" + (pos - 1).toString()).val());
    player.SB = parseInt($("#sb" + (pos - 1).toString()).val());
    player.CS = parseInt($("#cs" + (pos - 1).toString()).val());
    return player;
}

//Function to set "default" stats to 2014 LAA
function setInitialStats() {
    setStats(1, "Kole Calhoun", 483, 133, 31, 3, 16, 38, 5, 3);
    setStats(2, "Mike Trout", 593, 172, 39, 9, 35, 82, 16, 2);
    setStats(3, "Josh Hamilton", 338, 89, 21, 0, 10, 32, 3, 3);
    setStats(4, "Albert Pujols", 623, 170, 37, 1, 28, 48, 5, 1);
    setStats(5, "Howie Kendrick", 607, 177, 32, 5, 7, 48, 14, 5);
    setStats(6, "Erick Aybar", 580, 163, 29, 4, 7, 35, 16, 9);
    setStats(7, "David Freese", 454, 118, 24, 1, 10, 37, 1, 3);
    setStats(8, "Chris Iannetta", 300, 76, 22, 0, 7, 53, 3, 0);
    setStats(9, "Collin Cowgill", 255, 65, 10, 1, 5, 25, 4, 0);
}

//displayPlayerResults - create the player result table
function displayPlayerResults() {
    var numGames = parseInt($("#playerResultGames").val());

    //Set up full player table
    var html = '';
    for (var i = 0; i < playerResults.length; i++) {
        var player = playerResults[i];
        //Assume errors combine in quadrature, adjust from 162 to specified number of games
        var medianRuns = player.MedianRuns * numGames / 162.0;
        var minRuns = medianRuns - Math.sqrt((Math.pow((player.MedianRuns - player.MinRuns), 2.0) / 162.0) * numGames);
        var maxRuns = medianRuns + Math.sqrt((Math.pow((player.MaxRuns - player.MedianRuns), 2.0) / 162.0) * numGames);
        var medianRBI = player.MedianRBI * numGames / 162.0;
        var minRBI = medianRBI - Math.sqrt((Math.pow((player.MedianRBI - player.MinRBI), 2.0) / 162.0) * numGames);
        var maxRBI = medianRBI + Math.sqrt((Math.pow((player.MaxRBI - player.MedianRBI), 2.0) / 162.0) * numGames);

        //Create HTML for this player
        html += "<tr>";
        html += '<td>' + (i + 1).toString() + '</td>';
        html += '<td>' + player.Name + '</td>';
        html += '<td>' + Math.round(medianRuns) + '</td>';
        html += '<td>' + Math.round(minRuns) + ' - ' + Math.round(maxRuns) + '</td>';
        html += '<td>' + Math.round(medianRBI) + '</td>';
        html += '<td>' + Math.round(minRBI) + ' - ' + Math.round(maxRBI) + '</td>';
        html += "</tr>";
    }
    $('#resultsfull tbody:first').html(html);
}

//Make AJAX call to perform simulations
function performSimulation() {

    $("#indicator").show();
    $("#simbutton").hide();
    $("#errorDiv").hide();

    var players = [];
    for (var i = 0; i < 9; i++) {
        players.push(getStats(i + 1));
    }
    var data = "{ players: " + JSON.stringify(players) + " }";

    $.ajax({
        dataType: "json",
        type: "POST",
        url: "Home/SimulateFull",
        contentType: "application/json; charset=udf-8",
        data: data,
        success: function (data) {

            $("#indicator").hide();
            $("#simbutton").show();

            //See if this worked without an error
            if (data.Errors == null || data.Errors.length == 0) {
                $("#errorDiv").hide();
                //Set top runs
                $("#medianRuns").html(data.Result.MedianRuns);
                $("#minRuns").html(data.Result.MinRuns);
                $("#maxRuns").html(data.Result.MaxRuns);
                $("#medianRunsGame").html((data.Result.MedianRuns / 162.0).toFixed(2));
                $("#minRunsGame").html((data.Result.MinRuns / 162.0).toFixed(2));
                $("#maxRunsGame").html((data.Result.MaxRuns / 162.0).toFixed(2));

                playerResults = data.Result.PlayerResults;
                displayPlayerResults();

                //Show appropriate grid
                $('#divSimulate').hide();
                $('#divResults').show();
            } else {
                var html = '<ul>';
                for (var i = 0; i < data.Errors.length; i++) {
                    html += "<li>" + data.Errors[i] + "</li>";
                }
                html += "</ul>";
                $("#errorDiv").html(html);
                $("#errorDiv").show();
            }
        }
    });
}

//On document finished loading
$(document).ready(function () {
    //Set up full table
    var html = '';
    for (var i = 0; i < NUM_SPOTS; i++) {
        html += "<tr>";
        html += '<td>' + (i + 1).toString() + '</td>';
        html += '<td><button id="move' + i + '" type="button" class="btn btn-primary" onclick="moveclick(' + i + ');" style="width:60px">Move</button></td>';
        html += '<td><input id="name' + i + '" type="text" style="width:250px; margin-bottom: 0px;" class="form-control"/></td>';
        html += '<td><input id="ab' + i + '" type="text" class="smallinput form-control" style="margin-bottom: 0px;"/></td>';
        html += '<td><input id="h' + i + '" type="text" class="smallinput form-control" style="margin-bottom: 0px;"/></td>';
        html += '<td><input id="2b' + i + '" type="text" class="smallinput form-control" style="margin-bottom: 0px;"/></td>';
        html += '<td><input id="3b' + i + '" type="text" class="smallinput form-control" style="margin-bottom: 0px;"/></td>';
        html += '<td><input id="hr' + i + '" type="text" class="smallinput form-control" style="margin-bottom: 0px;"/></td>';
        html += '<td><input id="bb' + i + '" type="text" class="smallinput form-control" style="margin-bottom: 0px;"/></td>';
        html += '<td><input id="sb' + i + '" type="text" class="smallinput form-control" style="margin-bottom: 0px;"/></td>';
        html += '<td><input id="cs' + i + '" type="text" class="smallinput form-control" style="margin-bottom: 0px;"/></td>';
        html += '<td><span class="glyphicon glyphicon-trash" style="cursor:pointer; margin-top:7px;" onclick="removePlayer(' + i + ');"></span></td>';
        html += "</tr>";
    }
    $('#lineupgridfull tbody:first').html(html);

    //Wire up simulate button
    $('#simbutton').click(function () {
        performSimulation();
    });

    //Wire up back button
    $('#backbutton').click(function () {
        $('#divSimulate').show();
        $('#divResults').hide();
    });

    //Wire up update player results button
    $("#btnUpdatePlayerResults").click(displayPlayerResults);

    //Set initial stats for Angels
    setInitialStats();
});