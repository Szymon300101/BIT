"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("bmHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("GotDataPath", function (path) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `DataPath: ${path}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    connection.invoke("GetSomeData").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});