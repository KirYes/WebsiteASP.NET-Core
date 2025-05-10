"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
if (document.getElementById("sendButton") !== null) {
    document.getElementById("sendButton").disabled = true;
}
document.addEventListener("DOMContentLoaded", function () {
    fetch('/api/Chat/history')
        .then(response => response.json())
        .then(data => {
            const messagesList = document.getElementById("messagesList");
            data.forEach(msg => {
                const li = document.createElement("li");
                li.textContent = `${msg.user}: ${msg.text}`;
                const count = messagesList.children.length; 

               
                if (count % 2 === 0) {
                    li.classList.add("even-message");
                    li.style.animationDelay = `${count * 0.1}s`;
                } else {
                    li.classList.add("odd-message");
                    li.style.animationDelay = `${count * 0.1}s`;
                }
                messagesList.appendChild(li);
            });
        });
});

function appendMessage(text){
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = text;
}
connection.on("ReceiveMessage", function (user, message) {
    appendMessage(`${user}: ${message}`);
});
connection.on("Answer", function (user, message) {
    appendMessage(`GPT Answer: ${message}`);
});
connection.on("Question", function (user, message) {
    appendMessage(`${user}'s question to GPT: ${message}`);
});

connection.start().then(function () {
    if (document.getElementById("sendButton") !== null) {
        document.getElementById("sendButton").disabled = false;
    }
}).catch(function (err) {
    return console.error(err.toString());
});

if (document.getElementById("sendButton") !== null) {
    document.getElementById("sendButton").addEventListener("click", function (event) {
        var user = document.getElementById("userInput").value;
        var message = document.getElementById("messageInput").value;
        connection.invoke("SendMessage", user, message).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });
}

