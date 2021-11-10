window.onload = load;

function load() {
    loadSyncBreaks();

    $('#myTabs a').click(function (e) {
        e.preventDefault()
        $(this).tab('show')
    })
    connectToSync();

}



function highlightButton(id) {
    var button = document.getElementById(`${id}-Submit-Button`);
    button.classList = "btn btn-danger";
}


function requestSaveImg() {
    var data = document.getElementById("Upload-File-Button").files[0];

    var formData = new FormData();
    formData.append("fileUploadForm", data);
    $.ajax({
        url: `/Creature/SaveImg`,
        cache: false,
        type: "POST",
        data: formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (response) {
            console.log(response);
            document.getElementById("FilePath-Field").value = response.path;
            console.log(document.getElementById("FilePath-Field").value);
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

//function fillZero() {
//    if (document.getElementById("Initiative-Input").value == "")
//        document.getElementById("Initiative-Input").value = 0;
//    if (document.getElementById("HP-Input").value == "")
//        document.getElementById("HP-Input").value = 0;
//    if (document.getElementById("InitiativeBonus-Input").value == "")
//        document.getElementById("InitiativeBonus-Input").value = 0;
//    if (document.getElementById("AC-Input").value == "")
//        document.getElementById("AC-Input").value = 0;
//}

var source;

function connectToSync() {

    if (window.EventSource == undefined) {
        // If not supported  
        console.log("Your browser doesn't support Server Sent Events.");
        return;
    } else {
        source = new EventSource('../Initiative/SyncInit');

        source.onopen = function (event) {
            console.log("Connection Opened.");
            document.getElementById('targetDiv').innerHTML += '<br>';
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