
 //var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();
//let raceTimer = 0;
//let raceInterval;


////Disable the send button until connection is established.
//if (document.getElementById("start-button") !== null) {
//    document.getElementById("start-button").style.display = "none";
//}
//if (document.getElementById("end") !== null) {
//    document.addEventListener("DOMContentLoaded", function () {
//        fetch('/api/Count/count')
//            .then(response => response.json())
//            .then(data => {
//                const end = document.getElementById("end");
//                const ps = document.createElement("p");
//                ps.id = "count";
//                ps.textContent = `${data}/2`;
//                end.appendChild(ps);
//            });
//    });
//}


//connection.on("WaitingForOthers", (remaining) => {
//    document.getElementById("timer1").textContent = `Waiting for ${remaining} player(s)...`;
//});

//connection.on("RaceStarted", () => {
//    document.getElementById("start-button").disabled = true;
//    document.getElementById("timer1").textContent = 0;
//    document.getElementById("timer").style.opacity = 1;
//});


//connection.on("UpdateRaceTimer", (elapsedSeconds) => {
//    document.getElementById("timer1").textContent = elapsedSeconds;
//});

//connection.on("RaceEnded", (finalTime) => {
//    if (document.getElementById("start-button") !== null) {
//        document.getElementById("start-button").disabled = false;
//    }
//    alert(`Race finished! Final time: ${finalTime} seconds`);
//});

//connection.start().then(function () {
//    if (document.getElementById("start-button") !== null) {
//        document.getElementById("start-button").style.display = "none";
//    }
//}).catch(function (err) {
//    return console.error(err.toString());
//});
//function joinGroup(groupName) {
//    connection.invoke("AddToGroup", groupName).catch(err => console.error(err.toString()));
//   }
//connection.on("Join", function (message, count) {

//    var ps = document.getElementById("count");
//    ps.textContent = `${count}/2`;
//    console.log(message);
//}
//);
//function leaveFromGroup(groupName) {
//    connection.invoke("RemoveFromGroup", groupName).catch(err => console.error(err.toString()));
//}
//connection.on("Leave", function (message, count) {

//    var ps = document.getElementById("count");
//    ps.textContent = `${count}/2`;
//    console.log(message);
//}
//);


//function startGame(groupName) {
//    /*    window.open('/Index', '_self');*/
//    document.getElementById("race").style.display = "none";
//    var game = document.createElement("canvas");
//    var saf = document.createElement("button");
//    var car;
//    saf.id = "saf";
//    saf.style.width = "50px";
//    saf.style.height = "50px";
//    let isDrawing = false;
//    let prevMouseX = 0;
//    let prevMouseY = 0;
//    game.id = "game";
//    game.width = window.innerWidth;
//    game.height = window.innerHeight;
//    game.style.width = "100%";
//    game.style.height = "100vh";
//    game.style.border = "1px solid black";
//    document.getElementById("groups").appendChild(saf);
//    document.getElementById("groups").appendChild(game);
//    const ctx = game.getContext("2d");
//    ctx.strokeStyle = "red";
//    saf.onclick = function () {
//        car = ctx.getImageData(0, 0, game.width,game.height);
//        ctx.clearRect(0, 0, game.width, game.height); // Clear the canvas
      
//    };
//    game.addEventListener("mousedown", (e) => {
//        if (e.button === 2) {
//            ctx.clearRect(0, 0, game.width, game.height); // Clear the canvas
//        }
//        isDrawing = true;
//        const rect = game.getBoundingClientRect();
//        const scaleX = game.width / rect.width;
//        const scaleY = game.height / rect.height;
//        prevMouseX = (e.clientX - rect.left) * scaleX;
//        prevMouseY = (e.clientY - rect.top) * scaleY;
//        ctx.lineCap = "round";
//        ctx.lineWidth = 5;
//        ctx.beginPath();
//        ctx.moveTo(prevMouseX, prevMouseY);
//    });
//    game.addEventListener("mouseup", () => {
//        isDrawing = false;
//    });
//    game.addEventListener("mousemove", (e) => {
//        if (isDrawing) {
//            const rect = game.getBoundingClientRect();
//            const scaleX = game.width / rect.width;
//            const scaleY = game.height / rect.height;
//            const x = (e.clientX - rect.left) * scaleX;
//            const y = (e.clientY - rect.top) * scaleY;

//            ctx.lineTo(x, y);
//            ctx.stroke();
//            const stroke = {
//                x: x,
//                y: y,
//                lineWidth: ctx.lineWidth,
//                color: ctx.strokeStyle,
//                linecap: ctx.lineCap
//            };
//            connection.invoke("DrawGame", stroke, groupName);
//        }
//    });
//    window.addEventListener(
//        "keydown",
//         (event) => {
//            if (event.defaultPrevented) {
//                return;
//            }
//            const moveAmount = 10;
//            switch (event.key) {
//                case "ArrowDown":
//                    ctx.clearRect(0, 0, game.width, game.height); // Clear the canvas
//                    ctx.putImageData(car, 0, moveAmount);
//                    car = ctx.getImageData(0, 0, game.width, game.height);
//                    console.log("down");
//                    break;
//                case "ArrowUp":
//                    ctx.clearRect(0, 0, game.width, game.height); // Clear the canvas
//                    if (car.y )
//                    ctx.putImageData(car, 0, -moveAmount);
//                    car = ctx.getImageData(0, 0, game.width, game.height);
//                    console.log("up");
//                    break;
//                case "ArrowLeft":
//                    ctx.clearRect(0, 0, game.width, game.height); // Clear the canvas

//                    ctx.putImageData(car, -moveAmount, 0);
//                    car = ctx.getImageData(0, 0, game.width, game.height);
//                    console.log("left");
//                    break;
//                case "ArrowRight":
//                    ctx.clearRect(0, 0, game.width, game.height); // Clear the canvas
//                    ctx.putImageData(car, moveAmount, 0);
//                    car = ctx.getImageData(0, 0, game.width, game.height);
//                    console.log("right");
//                    break;
//                case "Space":
//                    ctx.clearRect(0, 0, game.width, game.height); // Clear the canvas
//                    ctx.putImageData(car, 0, -moveAmount);
//                    car = ctx.getImageData(0, 0, game.width, game.height);
//                    ctx.clearRect(0, 0, game.width, game.height);
//                    ctx.putImageData(car, 0, moveAmount);
//                    car = ctx.getImageData(0, 0, game.width, game.height);
//                    console.log("hello");
//                    break;
//                case " ":
//                    // Do something for "space" key press.
//                    break;
//                case "Escape":
//                    // Do something for "esc" key press.
//                    break;
//                default:
//                    return; // Quit when this doesn't handle the key event.
//            }

//            // Cancel the default action to avoid it being handled twice
//            event.preventDefault();
//        },
//        true,
//    );
//    connection.on("Dra", function (cctx) {
//        ctx.lineWidth = cctx.lineWidth;
//        ctx.strokeStyle = cctx.strokeStyle;
//        ctx.lineCap = cctx.lineCap;
//        ctx.lineTo(cctx.x, cctx.y);
//        ctx(stroke);
//    });
//}



//connection.on("Send", (groupName) => {
//    console.log(`Joined group: ${groupName}`);
//    document.getElementById("timer1").textContent = `You are in group ${groupName}`; ")

//if (document.getElementById("start-button") !== null) {
//    document.getElementById("start-button").addEventListener("click", () => {
//        connection.invoke("AddToGroup", "RaceGroup").catch(err => console.error(err));
//        var h1 = document.createElement("h1");
//        document.getElementById("race").appendChild(h1);
//        h1.textContent = RaceGroup.count;
//        //document.getElementById("timer").style.opacity = 1;
//        //document.getElementById("start-button").style.display = "none";
//        //connection.invoke("PlayerReady").catch(err => console.error(err));
//    });
//}