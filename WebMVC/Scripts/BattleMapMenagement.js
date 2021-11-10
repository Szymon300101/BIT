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
        };

        source.onerror = function (event) {
            if (event.eventPhase == EventSource.CLOSED) {
                console.log("Connection Closed.");
            }
        };

        source.onmessage = function (event) {
            console.log(event.data);
            if (event.data == "True") {
                location.reload();
            }
        };
    }
}