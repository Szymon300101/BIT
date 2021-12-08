let initiative = new Array();

var connection = new signalR.HubConnectionBuilder().withUrl("testHub").build();

ajaxGetData() // + loadFullInitiative()




connection.on("GotDataPath", function (path) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `DataPath: ${path}`;
});

connection.start().then(function () {

}).catch(function (err) {
    return console.error(err.toString());
});

/*document.getElementById("sendButton").addEventListener("click", function (event) {
    connection.invoke("GetSomeData").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});*/


function ajaxGetData() {
    $.ajax({
        url: `/Home/GetInitaitive`,
        cache: false,
        type: "GET",
        success: function (response) {
            console.log(response);
            if (response.error == null) {
                initiative = response.initiative;
                loadFullInitiative();
            }
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

function loadFullInitiative() {
    for (var i = 0; i < initiative.length; i++) {
        let row = document.createElement("tr");
        row.id = `init-row-${i}`;

        let init = document.createElement("td")
        init.id = `init-init-${i}`;
        init.textContent = initiative[i].initiative;

        let name = document.createElement("td")
        name.id = `init-name-${i}`;
        name.textContent = initiative[i].name;

        let hp = document.createElement("td")
        hp.id = `init-hp-${i}`;
        hp.textContent = initiative[i].hp;

        let bonus = document.createElement("td")
        bonus.id = `init-bonus-${i}`;
        bonus.textContent = initiative[i].initiativeBonus;

        let ac = document.createElement("td")
        ac.id = `init-ac-${i}`;
        ac.textContent = initiative[i].ac;

        
        row.appendChild(init);
        row.appendChild(name);
        row.appendChild(hp);
        row.appendChild(bonus);
        row.appendChild(ac);

        document.getElementById("initiative-table").appendChild(row);
    }
}