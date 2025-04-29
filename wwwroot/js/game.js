"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();
let raceTimer = 0;
let raceInterval;

//Disable the send button until connection is established.
document.getElementById("start-button").style.display = "none";

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
    document.getElementById("start-button").disabled = false;
    alert(`Race finished! Final time: ${finalTime} seconds`);
});

connection.start().then(function () {
    document.getElementById("start-button").style.display = "block";
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("start-button").addEventListener("click", () => {
    document.getElementById("timer").style.opacity = 1;
    document.getElementById("start-button").style.display = "none";
    connection.invoke("PlayerReady").catch(err => console.error(err));
});
