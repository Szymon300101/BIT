window.onload = load;

function load() {
    loadSyncBreaks();
    connectToSync();
}




function requestSaveImg() {
    var data = document.getElementById("Upload-File-Button").files[0];

    var formData = new FormData();
    formData.append("fileUploadForm", data);
    $.ajax({
        url: `/BattleMap/SaveImg`,
        cache: false,
        type: "POST",
        data: formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (response) {
            console.log(response);
            //location.reload();
            document.getElementById("FilePath-Field").value = response.path;
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

function dmgModalSetup(id) {
    document.getElementById("DmgCreatureId").value = id;
}

var source;
function connectToSync() {

    if (window.EventSource == undefined) {
        // If not supported  
        console.log("Your browser doesn't support Server Sent Events.");
        return;
    } else {
        source =new EventSource('../BattleMap/SyncBM');

        source.onopen = function (event) {
            console.log("Connection Opened.");
            openedSyncConnection();
        };

        source.onerror = function (event) {
            if (event.eventPhase == EventSource.CLOSED) {
                console.log("Connection Closed.");
                breakSyncConnection();
            }
        };

        source.onmessage = function (event) {
            console.log(event.data);
            if (event.data == "True") {
                breakSyncConnection();
                requestGetData();
                connectToSync()
                //location.reload();
            }
        };
    }
}

function requestGetData(){
    $.ajax({
        url: `/BattleMap/GetData`,
        cache: false,
        type: "POST",
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (response) {
            console.log(response);
            updatePage(response.dataModel);
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

async function updatePage(model) {
    let creatures = document.getElementById("creatures-div");
    let r_size = document.getElementById("r_size").innerHTML;

    creatures.innerHTML = "";

    for (var i = 0; i < model.FullInitiative.length; i++) {
        let element = document.createElement("div");
        element.style.position = "absolute";
        element.style.left = `${model.FullInitiative[i].PositionX * r_size}px`;
        element.style.bottom = `${model.FullInitiative[i].PositionY * r_size}px`;
        element.style.width = `${r_size}px`;
        element.style.height = `${r_size}px`;
        if (model.StateData.Turn == i) {
            element.style.border = "2px solid #e67300;";
        }
        if (model.FullInitiative[i].HP == 0) {
            element.style.border = "5px solid red;";
        }

        element.classList = "board-cell";
        element.setAttribute("pos_x", model.FullInitiative[i].PositionX);
        element.setAttribute("pos_y", model.FullInitiative[i].PositionY);

        let img = document.createElement("img")
        
        img.style = "width:100%; height:100%"
        img.datatoggle="tooltip"
        img.dataplacement="top"
        img.title = model.FullInitiative[i].Name;
        requestGetImg(model.FullInitiative[i].ImagePath, model.FullInitiative[i].CreatureType, img);
        element.appendChild(img);

        creatures.appendChild(element);
    }
}

function requestGetImg(path, type, img) {
    $.ajax({
        url: `/BattleMap/GetImgSrc`,
        cache: false,
        type: "POST",
        dataType: 'json',
        data: {
            "path": path,
            "type": type
        },
        success: function (response) {
            console.log(response);
            img.src = response.src;
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}