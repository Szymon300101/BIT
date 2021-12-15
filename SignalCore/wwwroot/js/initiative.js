let initiative = new Array();

var connection = new signalR.HubConnectionBuilder().withUrl("hub").build();

ajaxGetData() // + loadFullInitiative()




connection.on("GotDataPath", function (path) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `DataPath: ${path}`;
});

connection.on("FieldChanged", function (name, id, value) {
    document.getElementById(`init-${name}-${id}`).value = value;
    console.log(`init-${name}-${id} ` + value)
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
        let Id = initiative[i].id;
        let row = document.createElement("tr");
        row.id = `init-row-${Id}`;

        let init = document.createElement("td")
            let init_input = document.createElement("input")
            init_input.id = `init-initiative-${Id}`;
            init_input.value = initiative[i].initiative;
            init_input.style.width = '30px';
            init_input.addEventListener("change", changeField, false);
            init_input.fieldType = 'initiative';
            init_input.dbId = Id;
        init.appendChild(init_input);

        let name = document.createElement("td")
        name.id = `init-name-${Id}`;
        name.textContent = initiative[i].name;

        let hp = document.createElement("td")
        hp.id = `init-hp-${Id}`;
        hp.textContent = initiative[i].hp;

        let bonus = document.createElement("td")
        bonus.id = `init-bonus-${Id}`;
        bonus.textContent = initiative[i].initiativeBonus;

        let ac = document.createElement("td")
        ac.id = `init-ac-${Id}`;
        ac.textContent = initiative[i].ac;

        
        row.appendChild(init);
        row.appendChild(name);
        row.appendChild(hp);
        row.appendChild(bonus);
        row.appendChild(ac);

        //document.getElementById("initiative-table").appendChild(row);
    }
}

function changeField(evt) {
    let type = evt.currentTarget.fieldType;
    let dbId = evt.currentTarget.dbId;
    let value = evt.currentTarget.value;
    connection.invoke("ChangeField", type, dbId.toString(), value.toString()).catch(function (err) {
        return console.error(err.toString());
    });
} 