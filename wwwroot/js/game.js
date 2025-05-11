"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();
let raceTimer = 0;
let raceInterval;

//Disable the send button until connection is established.
if (document.getElementById("start-button") !== null) {
    document.getElementById("start-button").style.display = "none";
}
if (document.getElementById("end") !== null) {
    document.addEventListener("DOMContentLoaded", function () {
        fetch('/api/Count/count')
            .then(response => response.json())
            .then(data => {
                const end = document.getElementById("end");
                const ps = document.createElement("p");
                ps.id = "count";
                ps.textContent = `${data}/2`;
                end.appendChild(ps);
            });
    });
}


connection.on("WaitingForOthers", (remaining) => {
    document.getElementById("timer1").textContent = `Waiting for ${remaining} player(s)...`;
});

connection.on("RaceStarted", () => {
    document.getElementById("start-button").disabled = true;
    document.getElementById("timer1").textContent = 0;
    document.getElementById("timer").style.opacity = 1;
});


connection.on("UpdateRaceTimer", (elapsedSeconds) => {
    document.getElementById("timer1").textContent = elapsedSeconds;
});

connection.on("RaceEnded", (finalTime) => {
    if (document.getElementById("start-button") !== null) {
        document.getElementById("start-button").disabled = false;
    }
    alert(`Race finished! Final time: ${finalTime} seconds`);
});

connection.start().then(function () {
    if (document.getElementById("start-button") !== null) {
        document.getElementById("start-button").style.display = "none";
    }
}).catch(function (err) {
    return console.error(err.toString());
});
function joinGroup(groupName) {
    connection.invoke("AddToGroup", groupName).catch(err => console.error(err.toString()));
   }
connection.on("Join", function (message,count) {
    var ps = document.getElementById("count");
    ps.textContent = `${count}/2`;
    console.log(message);
}
);
function leaveFromGroup(groupName) {
    connection.invoke("RemoveFromGroup", groupName).catch(err => console.error(err.toString()));
}
connection.on("Leave", function (message, count) {
    var ps = document.getElementById("count");
    ps.textContent = `${count}/2`;
    console.log(message);
}
);
connection.on("StartG", function () {
    console.log("Game started");

});

//connection.on("Send", (groupName) => {
//    console.log(`Joined group: ${groupName}`);
//    document.getElementById("timer1").textContent = `You are in group ${groupName}`; ")

if (document.getElementById("start-button") !== null) {
    document.getElementById("start-button").addEventListener("click", () => {
        connection.invoke("AddToGroup", "RaceGroup").catch(err => console.error(err));
        var h1 = document.createElement("h1");
        document.getElementById("race").appendChild(h1);
        h1.textContent = RaceGroup.count;
        //document.getElementById("timer").style.opacity = 1;
        //document.getElementById("start-button").style.display = "none";
        //connection.invoke("PlayerReady").catch(err => console.error(err));
    });
}